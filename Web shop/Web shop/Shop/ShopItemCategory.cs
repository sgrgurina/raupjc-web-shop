using System;
using System.Collections.Generic;

namespace Webshop.Shop
{
    public class ShopItemCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<ShopItem> CategoryItems { get; set; }

        public ShopItemCategory(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            CategoryItems = new List<ShopItem>();
        }

        public ShopItemCategory()
        {
        }

        public override bool Equals(object obj)
        {
            var category = obj as ShopItemCategory;
            return category != null &&
                   Id.Equals(category.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}