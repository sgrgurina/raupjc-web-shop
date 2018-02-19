using System;

namespace Webshop.Shop
{
    public class ItemOrderInfo
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public ShopItem Item { get; set; }

        public ItemOrderInfo(Guid orderId, int amount, ShopItem item)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            Amount = amount;
            Item = item;
            Price = item.Price * amount;
        }

        public ItemOrderInfo()
        {
        }
    }
}