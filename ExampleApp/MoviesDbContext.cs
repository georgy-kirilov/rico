using Rico.Database;
using Microsoft.EntityFrameworkCore;

namespace ExampleApp;

public sealed class MoviesDbContext(DbContextOptions<MoviesDbContext> options) : BaseDbContext(options, [typeof(MoviesDbContext).Assembly]);
