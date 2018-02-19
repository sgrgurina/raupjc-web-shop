using System.Data.Entity;

namespace Webshop.Shop
{
    public class ShopDbContext : DbContext
    {
        public IDbSet<ShopItem> Items { get; set; }
        public IDbSet<ShopItemCategory> Categories { get; set; }
        public IDbSet<CartItem> CartItems { get; set; }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<ItemOrderInfo> ItemOrderInformation { get; set; }

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

            modelBuilder.Entity<Order>().HasKey(o => o.OrderId);
            modelBuilder.Entity<Order>().Property(o => o.BuyerName).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.BuyerSurname).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.ContactEmail).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.PhoneNumber).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.Adress).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.TotalPrice).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.OrderPlacedTime).IsRequired();
            modelBuilder.Entity<Order>().HasMany(o => o.OrderInformation);

            modelBuilder.Entity<ItemOrderInfo>().HasKey(o => o.Id);
            modelBuilder.Entity<ItemOrderInfo>().Property(o => o.OrderId).IsRequired();
            modelBuilder.Entity<ItemOrderInfo>().Property(o=>o.Amount).IsRequired();
            modelBuilder.Entity<ItemOrderInfo>().Property(o => o.Price).IsRequired();
            modelBuilder.Entity<ItemOrderInfo>().HasRequired(o => o.Item);

        }
    }
}
