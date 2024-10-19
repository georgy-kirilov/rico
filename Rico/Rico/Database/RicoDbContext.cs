using Microsoft.EntityFrameworkCore;

namespace Rico.Database;

public abstract class RicoDbContext(DbContextOptions options) : DbContext(options)
{
}
