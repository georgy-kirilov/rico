﻿using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Rico.Date")]
[assembly: InternalsVisibleTo("Rico.Database")]
[assembly: InternalsVisibleTo("Rico.SnowflakeId")]
[assembly: InternalsVisibleTo("Rico.ValueObjects")]

namespace Rico.Abstractions.Entities;

public abstract class Entity<TKey> where TKey : IEntityId
{
    public required TKey Id { get; init; }
}
