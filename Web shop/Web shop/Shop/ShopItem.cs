using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Webshop.Shop
{
    public class ShopItem
    {
        public Guid Id { get; set;}
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }

        public List<ShopItemCategory> Categories { get; set; }

        public ShopItem(string name, decimal price, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Description = description.Trim();
            Categories = new List<ShopItemCategory>();
        }

        public ShopItem(string name, decimal price, string description, List<ShopItemCategory> categories) : this(name, price, description)
        {
            Categories = categories;
        }

        public ShopItem()
        {
        }

        public override bool Equals(object obj)
        {
            var item = obj as ShopItem;
            return item != null &&
                   Id.Equals(item.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}