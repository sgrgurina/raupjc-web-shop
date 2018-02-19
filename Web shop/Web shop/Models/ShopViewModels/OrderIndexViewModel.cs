using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webshop.Models.ShopViewModels
{
    public class OrderIndexViewModel
    {
        public List<OrderViewModel> OrderViewModels;

        public OrderIndexViewModel(List<OrderViewModel> orderViewModels)
        {
            OrderViewModels = orderViewModels;
        }
    }
}
