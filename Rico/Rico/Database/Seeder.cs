using System.Reflection;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace Rico.Database;

public sealed record SeedingModel<TEntity, TId>(TEntity[] Entities) where TEntity : Entity<TId> where TId : class;

public sealed class Seeder<TEntity, TId> where TEntity : Entity<TId> where TId : class
{
    public async Task Seed(BaseDbContext dbContext)
    {
        var json = await File.ReadAllTextAsync($"./Assets/{typeof(TEntity).Name}.json");
        var model = JsonSerializer.Deserialize<SeedingModel<TEntity, TId>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        foreach (var entity in model!.Entities)
        {
            var obj = await dbContext.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == entity.Id);

            if (obj is null)
            {
                obj = Activator.CreateInstance<TEntity>();

                var props = typeof(TEntity).GetProperties().Where(p => p.SetMethod is not null);

                foreach (var prop in props)
                {
                    prop.SetValue(obj, prop.GetValue(entity));
                }

                await dbContext.Set<TEntity>().AddAsync(obj);
            }
            else
            {
                var props = typeof(TEntity).GetProperties();

                foreach (var prop in props)
                {
                    prop.SetValue(obj, prop.GetValue(entity));
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
