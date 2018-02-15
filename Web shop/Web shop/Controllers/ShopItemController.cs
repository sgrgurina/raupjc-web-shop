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
                ItemViewModel itemViewModel = new ItemViewModel(item.Id, item.Name, item.Price);
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
                        ShopItemCategory existingCategory = _repository.GetCategoryByName(trimmedCategoryName);
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

        public IActionResult AddCategory()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryViewModel model)
        {
            string newCategoryName = model.Name.Trim().ToLower();
            
            ShopItemCategory existingCategory = _repository.GetCategoryByName(newCategoryName);
            if (existingCategory == null)
            {
                ShopItemCategory newCategory = new ShopItemCategory(newCategoryName);
                _repository.AddCategory(newCategory);
            }
            else
            {
                return RedirectToAction("AddCategory");
            }

            return RedirectToAction("CategoryIndex");

        }

        public async Task<IActionResult> CategoryIndex()
        {
            List<ShopItemCategory> categories = _repository.GetAllCategories();
            List<CategoryViewModel> categoryViewModels = new List<CategoryViewModel>();

            foreach (var category in categories)
            {
                CategoryViewModel newCategoryViewModel = new CategoryViewModel(category.Id, category.Name);
                categoryViewModels.Add(newCategoryViewModel);
            }

            CategoryIndexViewModel categoryIndexViewModel = new CategoryIndexViewModel(categoryViewModels);
            return View(categoryIndexViewModel);
        }

        [HttpGet("ItemDetails/{itemId}")]
        public async Task<IActionResult> ItemDetails(Guid itemId)
        {
            ShopItem item = _repository.GetItem(itemId);

            ItemDetailsViewModel itemDetailsViewModel = new ItemDetailsViewModel(item.Id, item.Name, item.Price, item.Description);

            return View(itemDetailsViewModel);
        }


        public async Task<IActionResult> EditItem(Guid itemId)
        {
            ShopItem item = _repository.GetItem(itemId);
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> EditItem(ShopItem item)
        {
            _repository.UpdateItem(item);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditCategory(Guid categoryId)
        {
            ShopItemCategory category = _repository.GetCategoryById(categoryId);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(ShopItemCategory category)
        {
            _repository.UpdateCategory(category);
            return RedirectToAction("CategoryIndex");
        }

        [HttpGet("RemoveItem/{itemId}")]
        public async Task<IActionResult> RemoveItem(Guid itemId)
        {

            _repository.RemoveItem(itemId);
            return RedirectToAction("Index");
        }

        [HttpGet("RemoveCategory/{itemId}")]
        public async Task<IActionResult> RemoveCategory(Guid itemId)
        {
            _repository.RemoveCategory(itemId);
            return RedirectToAction("CategoryIndex");
        }

        
    }
}