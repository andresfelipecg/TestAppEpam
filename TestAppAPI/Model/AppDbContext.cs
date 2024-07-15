using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TestAppAPI.Model
{
    public class AppDbContext : DbContext
    {
        public DbSet<StudyGroup> StudyGroups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudyGroupUser> StudyGroupUsers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudyGroup>()
       .HasOne(sg => sg.Subject)
       .WithMany()
       .HasForeignKey(sg => sg.SubjectId);

            modelBuilder.Entity<StudyGroupUser>()
                .HasKey(sgu => new { sgu.StudyGroupId, sgu.UserId });

            modelBuilder.Entity<StudyGroupUser>()
                .HasOne(sgu => sgu.StudyGroup)
                .WithMany(sg => sg.StudyGroupUsers)
                .HasForeignKey(sgu => sgu.StudyGroupId);

            modelBuilder.Entity<StudyGroupUser>()
                .HasOne(sgu => sgu.User)
                .WithMany(u => u.StudyGroupUsers)
                .HasForeignKey(sgu => sgu.UserId);
        }
    }
}