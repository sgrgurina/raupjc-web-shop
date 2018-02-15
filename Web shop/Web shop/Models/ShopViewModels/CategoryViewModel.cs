using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webshop.Models.ShopViewModels
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public CategoryViewModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
