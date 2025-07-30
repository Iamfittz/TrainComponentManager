using Microsoft.EntityFrameworkCore;

namespace Train_Management_App.Data {
    public class AppDbContext : DbContext {
        public DbSet<TrainComponent> TrainComponents { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> context) : base(context) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<TrainComponent>(entity => {
                entity.Property(x => x.Name).IsRequired();
                entity.Property(x => x.UniqueNumber).IsRequired();
                entity.Property(x => x.QuantityAssignment).HasDefaultValue(null);

                entity.ToTable(x => x.HasCheckConstraint("CK_TrainComponent_Quantity",
                    "[CanAssignQuantity] = 0 AND [QuantityAssignment] IS NULL OR " +
                    "[CanAssignQuantity] = 1 AND [QuantityAssignment] > 0"));

                entity.HasIndex(x => x.UniqueNumber).IsUnique(); 
            });
        }
    }
}
