using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Webshop.Models.ShopViewModels
{
    public class ItemViewModel
    {
        
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public ItemViewModel(string name, decimal price, string description)
        {
            Name = name;
            Price = price;
            Description = description;
        }
    }
}
