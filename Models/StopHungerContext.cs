using Microsoft.EntityFrameworkCore;

namespace StopHunger.Models
{
    public class StopHungerContext : DbContext


    {
        public StopHungerContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShelterInfoType> ShelterInfoType { get; set; }
        public DbSet<UserProductDonation> UserProductDonations { get; set; }
        public DbSet<ProductDonutionToShelter> ProductDonutionToShelters {get; set;}
    }
}