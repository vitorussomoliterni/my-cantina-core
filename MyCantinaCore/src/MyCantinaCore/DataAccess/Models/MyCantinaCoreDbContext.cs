using Microsoft.EntityFrameworkCore;

namespace MyCantinaCore.DataAccess.Models
{
    public class MyCantinaCoreDbContext : DbContext
    {
        public DbSet<Bottle> Bottles { get; set; }
        public DbSet<Consumer> Consumers { get; set; }
        public DbSet<ConsumerBottle> ConsumerBottles { get; set; }
        public DbSet<GrapeVariety> GrapeVarieties { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConsumerBottle>()
                .Property(b => b.PricePaid)
                .HasColumnType("money");

            modelBuilder.Entity<ConsumerBottle>()
                .Property(b => b.Owned)
                .HasColumnType("bit");

            modelBuilder.Entity<ConsumerBottle>()
                .HasKey(cb => new { cb.BottleId, cb.ConsumerId });

            modelBuilder.Entity<Review>()
                .HasKey(r => new { r.BottleId, r.ConsumerId });
        }
    }
}
