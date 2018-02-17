using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webshop.Shop;

namespace Webshop.Models.ShopViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCart ShoppingCart { get; set; }

        public decimal ShoppingCartTotalPrice { get; set; }

        public ShoppingCartViewModel(ShoppingCart shoppingCart, decimal shoppingCartTotalPrice)
        {
            ShoppingCart = shoppingCart;
            ShoppingCartTotalPrice = shoppingCartTotalPrice;
        }
    }
}
