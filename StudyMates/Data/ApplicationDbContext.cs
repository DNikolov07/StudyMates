using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudyMates.Models;

namespace StudyMates.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Subject> Subjects { get; set; } = null!;

        public DbSet<StudyGroup> StudyGroups { get; set; } = null!;

        public DbSet<StudySession> StudySessions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<StudyGroup>()
                .HasOne(g => g.Creator)
                .WithMany()
                .HasForeignKey(g => g.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<StudyGroup>()
                .HasMany(g => g.Members)
                .WithMany()
                .UsingEntity(j => j.ToTable("StudyGroupMembers"));

            builder.Entity<Subject>().HasData
            (
                new Subject { Id = 1, Name = "Mathematics" },
                new Subject { Id = 2, Name = "Programming" },
                new Subject { Id = 3, Name = "Physics" },
                new Subject { Id = 4, Name = "Databases" },
                new Subject { Id = 5, Name = "English" },
                new Subject { Id = 6, Name = "Economics" }

            );
        }
    }



}