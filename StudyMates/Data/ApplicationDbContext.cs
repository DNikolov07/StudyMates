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
        }
    }



}