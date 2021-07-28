using BeekeepingDiary.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeekeepingDiary.Data
{
    public class BeekeepingDbContext : IdentityDbContext<ApplicationUser>
    {
        public BeekeepingDbContext(DbContextOptions<BeekeepingDbContext> options)
            : base(options)
        {
        }
            public DbSet<BeeGarden> BeeGardens { get; set; }
            public DbSet<Beehive> Beehives { get; set; }
            public DbSet<Inspection> Inspections { get; set; }
            public DbSet<Produce> Produces { get; set; }
            public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Beehive>()
                .HasOne(b => b.BeeGarden)
                .WithMany(b => b.Beehives)
                .HasForeignKey(b => b.BeeGardenId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Beehive>()
                .HasOne(b => b.Category)
                .WithMany(b => b.Beehives)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Inspection>()
                .HasOne(i => i.Beehive)
                .WithMany(i => i.Inspections)
                .HasForeignKey(i => i.BeehiveId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .Entity<Produce>()
                .HasOne(p => p.Beehive)
                .WithMany(b => b.Produces)
                .HasForeignKey(p => p.BeehiveId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
               .Entity<BeeGarden>()
               .HasOne(b => b.ApplicationUser)
               .WithMany(u => u.BeeGardens)
               .HasForeignKey(b => b.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }





    }
}
