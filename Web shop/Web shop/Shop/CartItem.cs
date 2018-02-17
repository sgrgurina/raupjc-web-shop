using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webshop.Shop
{
    public class CartItem
    {
        public Guid CartItemId { get; set; }
        public string CartId { get; set; }
        public int Amount { get; set; }
        public ShopItem ShopItem { get; set; }

        public CartItem(string cartId, ShopItem shopItem)
        {
            CartItemId = Guid.NewGuid();
            Amount = 1;

            CartId = cartId;
            ShopItem = shopItem;
        }

        public CartItem()
        {
        }
    }
}
