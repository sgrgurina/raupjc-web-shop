using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Webshop.Models.ShopViewModels;
using Webshop.Shop;
using Web_shop.Models;

namespace Web_shop.Controllers
{
    [Authorize]
    public class ShopItemController : Controller
    {
        private readonly IShopRepository _repository;

        public ShopItemController(IShopRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            List<ShopItem> items = _repository.GetAllItems();
            List<ItemViewModel> itemViewModels = new List<ItemViewModel>();

            foreach (var item in items)
            {
                ItemViewModel itemViewModel = new ItemViewModel(item.Name, item.Price, item.Description);
                itemViewModels.Add(itemViewModel);
            }

            ShopIndexViewModel indexViewModel = new ShopIndexViewModel(itemViewModels);

            return View(indexViewModel);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddItemViewModel model)
        {
            ShopItem newItem = new ShopItem(model.Name, model.Price, model.Description );

            if (!string.IsNullOrWhiteSpace(model.Categories))
            {
                string[] categoryNames = model.Categories.Split(',');
                foreach (var categoryName in categoryNames)
                {
                    string trimmedCategoryName = categoryName.Trim();
                    trimmedCategoryName = trimmedCategoryName.ToLower();

                    //if it isnt empty or null
                    if (!string.IsNullOrWhiteSpace(trimmedCategoryName))
                    {
                        ShopItemCategory existingCategory = _repository.GetCategory(trimmedCategoryName);
                        if (existingCategory == null)
                        {
                            ShopItemCategory newCategory = new ShopItemCategory(trimmedCategoryName);
                            _repository.AddCategory(newCategory);
                            newItem.Categories.Add(newCategory);
                        }
                        else
                        {
                            newItem.Categories.Add(existingCategory);
                        }
                    }
                }
            }
            _repository.AddItem(newItem);
            return RedirectToAction("Index");
        }
    }
}