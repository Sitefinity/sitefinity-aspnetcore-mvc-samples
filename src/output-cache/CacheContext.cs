using Microsoft.EntityFrameworkCore;

public class CacheContext : DbContext
{
    public DbSet<CacheEntity> CacheItems { get; set; }

    public CacheContext(DbContextOptions<CacheContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<CacheEntity>().ToTable("cache_items");
        entity.Property(x => x.Key);
        entity.Property(x => x.Data);
        entity.Property(x => x.Tags);

        entity.HasKey(x => x.Key);
    }
}
