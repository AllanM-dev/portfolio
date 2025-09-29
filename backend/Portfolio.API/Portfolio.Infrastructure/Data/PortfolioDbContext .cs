using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Infrastructure.Data
{
    public class PortfolioDbContext: DbContext
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options)
        {
        }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VersionCV> VersionsCV { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VersionCV>()
                .HasOne(v => v.User)
                .WithMany(u => u.VersionCVs)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
