using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClothesShop.Models
{
    public class ShopContext : IdentityDbContext<AppUser>
    {
        public ShopContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<CartModel> Carts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var user in builder.Model.GetEntityTypes())
            {
                var tableName = user.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    user.SetTableName(tableName.Substring(6));
                }
            }
        }
 
    }
}
