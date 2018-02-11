using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webshop.Models.ShopViewModels
{
    public class ShopIndexViewModel
    {
        public List<ItemViewModel> ItemViewModels;

        public ShopIndexViewModel(List<ItemViewModel> itemViewModels)
        {
            ItemViewModels = itemViewModels;
        }
    }
}