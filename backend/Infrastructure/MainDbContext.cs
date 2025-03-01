using Infrastructure.DbEntities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure;

public class MainDbContext : DbContext
{
    public MainDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<InterviewDbEntity> Interviews { get; set; } = null!;
    public DbSet<JobApplicationDbEntity> JobApplications { get; set; } = null!;
    public DbSet<JobApplicationUpdateDbEntity> JobApplicationUpdates { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            throw new InvalidOperationException("Database provider not configured.");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // configs
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainDbContext).Assembly);
    }
}