﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Rico.Abstractions.Date;
using Rico.Abstractions.SnowflakeId;
using Rico.Abstractions.ValueObjects;
using System.Reflection;

namespace Rico.Database;

public abstract class BaseDbContext(DbContextOptions options, Assembly[] assembliesToScan) : DbContext(options)
{
    private readonly Assembly[] _assembliesToScan = assembliesToScan;

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<UtcDateTime>().HaveConversion<UtcDateTimeValueConverter>();

        var types = _assembliesToScan.SelectMany(a => a.GetTypes());

        foreach (var type in types)
        {
            if (IsSnowflakeEntityId(type))
            {
                var converterType = typeof(SnowflakeEntityIdValueConverter<>).MakeGenericType(type);

                configurationBuilder
                    .Properties(type)
                    .HaveConversion(converterType)
                    .HaveMaxLength(SnowflakeEntityId.MaxLength)
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

                var internalValue = genericTypeArgument.IsValueType ? Activator.CreateInstance(genericTypeArgument) : null;

                var propertyBuilder = configurationBuilder.Properties(type);

                var converterType = typeof(ValueObjectValueConverter<,>).MakeGenericType([type, genericTypeArgument]);

                var constructor = type
                    .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
                    .Single(
                        ctor => ctor.GetParameters().Length == 1 &&
                        ctor.GetParameters()[0].ParameterType == genericTypeArgument);

                var valueObject = constructor.Invoke([internalValue]) as BaseValueObject ?? throw new ArgumentNullException("Value object cannot be null.");

                propertyBuilder.HaveConversion(converterType);

                propertyBuilder.HaveMaxLength(valueObject.MaxLength);

                propertyBuilder.AreUnicode(valueObject.Unicode);

                propertyBuilder.HavePrecision(valueObject.Precision, valueObject.Scale);
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                property.ValueGenerated = ValueGenerated.Never;
            }
        }

        foreach (var assemblyToScan in _assembliesToScan)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assemblyToScan);
        }
    }

    private static bool IsSnowflakeEntityId(Type type)
    {
        return typeof(SnowflakeEntityId).IsAssignableFrom(type) && !type.IsAbstract;
    }

    private static bool IsValueObject(Type type)
    {
        return typeof(BaseValueObject).IsAssignableFrom(type) && !type.IsAbstract;
    }
}
