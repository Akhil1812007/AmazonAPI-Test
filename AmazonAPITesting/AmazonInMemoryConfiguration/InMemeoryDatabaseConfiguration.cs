using AmazonAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace EFCoreInMemoryDbDemo
{
    public class ApiContext : DbContext
    {
        
        protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseInMemoryDatabase(databaseName: "akhil");

        }
        public DbSet<Merchant> Authors { get; set; }
        public DbSet<Product> Books { get; set; }
    }
}