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

            ShoppingCartViewModel cartViewModel =
                new ShoppingCartViewModel(_shoppingCart, _shoppingCart.GetCartTotalPrice());

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

        public IActionResult CreateOrder()
        {
            if (_shoppingCart.getCartItems() == null)
            {
                    return RedirectToAction("Index", "ShopItem");
            }
            else
            {
                if (_shoppingCart.getCartItems().Count == 0)
                {
                    return RedirectToAction("Index", "ShopItem");
                }
            }
            
            return View();
        }

        [HttpPost]
        public IActionResult CreateOrder(AddOrderViewModel addOrderViewModel)
        {
            List<ItemOrderInfo> itemOrderInformation = new List<ItemOrderInfo>();
            
            Order newOrder = new Order(addOrderViewModel.BuyerName.Trim(),
                addOrderViewModel.BuyerSurname.Trim(), addOrderViewModel.ContactEmail.Trim(), addOrderViewModel.PhoneNumber.Trim(),
                addOrderViewModel.Adress.Trim());

            foreach (var cartItem in _shoppingCart.getCartItems())
            {
                ItemOrderInfo orderInfo = new ItemOrderInfo(newOrder.OrderId, cartItem.Amount, cartItem.ShopItem);
                newOrder.AddItemOrderInfo(orderInfo);
            }
            newOrder.CalculateTotalPrice();

            _repository.AddOrder(newOrder);
            _shoppingCart.ClearCart();
            return RedirectToAction("OrderCompleted");
        }

        public IActionResult OrderCompleted()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult OrderIndex()
        {
            List<Order> orders = _repository.GetAllOrders();
            List<OrderViewModel> orderViewModels = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                OrderViewModel orderViewModel = new OrderViewModel(order.OrderId, order.BuyerName, order.BuyerSurname,
                    order.ContactEmail, order.TotalPrice, order.OrderPlacedTime);
                orderViewModels.Add(orderViewModel);
            }

            OrderIndexViewModel indexViewModel = new OrderIndexViewModel(orderViewModels);

            return View(indexViewModel);
        }

        [HttpGet("OrderDetails/{orderId}")]
        public async Task<IActionResult> OrderDetails(Guid orderId)
        {
            Order order = _repository.GetOrder(orderId);
            return View(order);
        }

    }
}