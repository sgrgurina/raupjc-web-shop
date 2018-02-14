using System;

namespace Webshop.Models.ShopViewModels
{
    public class ItemDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public ItemDetailsViewModel(Guid id, string name, decimal price, string description)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
        }
    }
}
