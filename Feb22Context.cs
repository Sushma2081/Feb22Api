using Microsoft.EntityFrameworkCore;

namespace Feb22Api;

public class Feb22ApiContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Subject> Subjects { get; set; }

    public DbSet<Marks> Marks { get; set; }

    public Feb22ApiContext(DbContextOptions<Feb22ApiContext> options)
           : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Marks>(entity =>
        {
            entity.HasKey(m => new { m.StudentId, m.SubjectId });
            entity.HasOne(m => m.Student)
            .WithMany(s => s.Marks)
            .HasForeignKey(m => m.StudentId);
            entity.HasOne(m => m.Subject)
            .WithMany(s => s.Marks)
            .HasForeignKey(m => m.SubjectId);

        });
        
    }

    
}