using Microsoft.EntityFrameworkCore;

namespace Amazon.Models
{
    public class AmazonContext : DbContext
    {
        public AmazonContext(DbContextOptions<AmazonContext> options):base(options)
        {

        }
        
        public DbSet<Merchant>? Merchants { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Customer>? Customers { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Feedback>? Feedbacks { get; set; }
        public DbSet<Cart>? carts { get; set; }
        public DbSet<OrderMaster>? OrderMasters { get; set; }
        public DbSet<OrderDetail>? OrderDetails { get; set; }
        public DbSet<Admin>? Admin { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderMaster>().HasMany(od=>od.OrderDetails).WithOne(om=>om.OrderMaster).HasForeignKey(k=>k.OrderMasterId).OnDelete(DeleteBehavior.Cascade);



        }



    }
}
