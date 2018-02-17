using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Webshop.Shop
{
    public class ShoppingCart
    {
        private readonly ShopDbContext _dbContext;

        public string ShoppingCartId { get; set; }

        public List<CartItem> CartItems { get; set; }

        public ShoppingCart(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<ShopDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context)
            {
                ShoppingCartId = cartId
            };
        }

        public void AddToCart(ShopItem item)
        {
            var shoppingCartItem =
                _dbContext.CartItems.FirstOrDefault(s => s.ShopItem.Id == item.Id && s.CartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new CartItem(ShoppingCartId, item);
                _dbContext.CartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
                _dbContext.Entry(shoppingCartItem).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }

        public int RemoveFromCart(ShopItem item)
        {
            var shoppingCartItem =
                _dbContext.CartItems.FirstOrDefault(s => s.ShopItem.Id == item.Id && s.CartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                    _dbContext.Entry(shoppingCartItem).State = EntityState.Modified;
                }
                else
                {
                    _dbContext.CartItems.Remove(shoppingCartItem);
                }
            }

            _dbContext.SaveChanges();

            return localAmount;
        }

        public List<CartItem> getCartItems()
        {
            return CartItems ?? (CartItems = _dbContext.CartItems.Where(c => c.CartId == ShoppingCartId)
                       .Include(c => c.ShopItem).ToList());
        }

        public void ClearCart()
        {
            var cartItems = _dbContext.CartItems.Where(c => c.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                _dbContext.CartItems.Remove(cartItem);
            }

            _dbContext.SaveChanges();
        }

        public decimal GetCartTotalPrice()
        {
            if (_dbContext.CartItems.Count(c => c.CartId == ShoppingCartId) == 0)
            {
                return 0;

            }
            var totalPrice = _dbContext.CartItems.Where(c => c.CartId == ShoppingCartId)
                .Select(c => c.ShopItem.Price * c.Amount).Sum();
            return totalPrice;
        }
    }
}