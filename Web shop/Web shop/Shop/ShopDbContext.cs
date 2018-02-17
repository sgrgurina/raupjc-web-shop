using System.Data.Entity;

namespace Webshop.Shop
{
    public class ShopDbContext : DbContext
    {
        public IDbSet<ShopItem> Items { get; set; }
        public IDbSet<ShopItemCategory> Categories { get; set; }
        public IDbSet<CartItem> CartItems { get; set; }

        public ShopDbContext(string cnnstr) : base(cnnstr)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ShopItem>().HasKey(i => i.Id);
            modelBuilder.Entity<ShopItem>().Property(i => i.Name).IsRequired();
            modelBuilder.Entity<ShopItem>().Property(i => i.Description).IsRequired();
            modelBuilder.Entity<ShopItem>().Property(i => i.Price).IsRequired();
            modelBuilder.Entity<ShopItem>().HasMany(i => i.Categories).WithMany(c => c.CategoryItems);

            modelBuilder.Entity<ShopItemCategory>().HasKey(c => c.Id);
            modelBuilder.Entity<ShopItemCategory>().Property(c => c.Name).IsRequired();

            modelBuilder.Entity<CartItem>().HasKey(i => i.CartItemId);
            modelBuilder.Entity<CartItem>().Property(i => i.CartId).IsRequired();
            modelBuilder.Entity<CartItem>().Property(i => i.Amount).IsRequired();
            modelBuilder.Entity<CartItem>().HasRequired(i => i.ShopItem);

        }
    }
}
