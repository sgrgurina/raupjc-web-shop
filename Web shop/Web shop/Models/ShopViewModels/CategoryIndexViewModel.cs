using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webshop.Models.ShopViewModels
{
    public class CategoryIndexViewModel
    {
        public List<CategoryViewModel> CategoryViewModels;

        public CategoryIndexViewModel(List<CategoryViewModel> categoryViewModels)
        {
            CategoryViewModels = categoryViewModels;
        }
    }
}
