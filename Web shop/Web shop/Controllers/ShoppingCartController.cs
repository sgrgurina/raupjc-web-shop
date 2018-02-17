using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Webshop.Models.ShopViewModels;
using Webshop.Shop;

namespace Web_shop.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IShopRepository _repository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IShopRepository repository, ShoppingCart shoppingCart)
        {
            _repository = repository;
            _shoppingCart = shoppingCart;
        }

        public IActionResult Index()
        {
            var items = _shoppingCart.getCartItems();
            _shoppingCart.CartItems = items;

            ShoppingCartViewModel cartViewModel = new ShoppingCartViewModel(_shoppingCart, _shoppingCart.GetCartTotalPrice());

            return View(cartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(Guid itemId)
        {
            ShopItem item = _repository.GetItem(itemId);
            if (item != null)
            {
                _shoppingCart.AddToCart(item);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(Guid itemId)
        {
            ShopItem item = _repository.GetItem(itemId);
            if (item != null)
            {
                _shoppingCart.RemoveFromCart(item);
            }
            return RedirectToAction("Index");
        }
        
    }
}