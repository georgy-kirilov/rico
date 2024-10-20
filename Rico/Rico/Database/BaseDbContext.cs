using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Rico.Ulids;
using Rico.ValueObjects;

namespace Rico.Database;

public abstract class BaseDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        var types = GetType().Assembly.GetTypes();

        foreach (var type in types)
        {
            if (IsAssignableToGenericType(type, typeof(UlidEntityId)))
            {
                var converterType = typeof(UlidEntityIdValueConverter<>).MakeGenericType(type);

                configurationBuilder
                    .Properties(type)
                    .HaveConversion(converterType)
                    .HaveMaxLength(26)
                    .AreFixedLength()
                    .AreUnicode(false);

                continue;
            }

            if (IsValueObject(type))
            {
                Type? baseType = type.BaseType;

                ArgumentNullException.ThrowIfNull(nameof(type.BaseType));

                if (!baseType!.IsGenericType)
                {
                    baseType = baseType.BaseType;
                }

                Type[] genericArguments = baseType!.GetGenericArguments();

                Type genericTypeArgument = genericArguments[0];

                var propertyBuilder = configurationBuilder.Properties(type);

                var converterType = typeof(ValueObjectValueConverter<,>).MakeGenericType([type, genericTypeArgument]);

                propertyBuilder.HaveConversion(converterType);

                var valueObject = Activator.CreateInstance(type, nonPublic: true);

                var length = type
                    .GetProperty("Length", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy)!
                    .GetValue(valueObject)
                    as Length;

                if (length!.Value != Length.None.Value)
                {
                    propertyBuilder.HaveMaxLength(length.Value);
                }

                if (length.IsExact)
                {
                    propertyBuilder.AreFixedLength();
                }

                var unicode = type
                    .GetProperty("Unicode", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy)!
                    .GetValue(valueObject)
                    as Unicode;

                if (unicode!.IsAllowed)
                {
                    propertyBuilder.AreUnicode();
                }

                var precision = type
                    .GetProperty("Precision", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy)!
                    .GetValue(valueObject)
                    as Precision;

                if (precision != Precision.None)
                {
                    propertyBuilder.HavePrecision(precision!.PrecisionValue, precision.Scale);
                }
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            modelBuilder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name);

            foreach (var property in entityType.GetProperties())
            {
                property.ValueGenerated = ValueGenerated.Never;
            }

            foreach (var foreignKey in entityType.GetForeignKeys())
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseDbContext).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    private static bool IsValueObject(Type type)
    {
        return IsAssignableToGenericType(type, typeof(ValueObject<>));
    }

    public static bool IsAssignableToGenericType(Type givenType, Type genericType)
    {
        if (givenType == null || genericType == null)
            return false;

        if (genericType.IsAssignableFrom(givenType))
            return true;

        // Check for interfaces and base types
        if (genericType.IsGenericTypeDefinition)
        {
            var interfaceTypes = givenType.GetInterfaces();
            foreach (var it in interfaceTypes)
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
            }

            // Check the base type
            Type baseType = givenType.BaseType;
            while (baseType != null)
            {
                if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
                baseType = baseType.BaseType;
            }
        }

        return false;
    }
}
