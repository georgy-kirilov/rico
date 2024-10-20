using Microsoft.EntityFrameworkCore;
using Rico.Database;

namespace Web.Database;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : BaseDbContext(options)
{
}
