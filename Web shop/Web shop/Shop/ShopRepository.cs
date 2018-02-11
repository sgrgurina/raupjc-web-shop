﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

namespace Webshop.Shop
{
    public class ShopRepository : IShopRepository
    {
        private readonly ShopDbContext _context;

        public ShopRepository(ShopDbContext context)
        {
            _context = context;
        }

        public ShopItem GetItem(Guid itemId)
        {
            ShopItem item = _context.Items.FirstOrDefault(i => i.Id == itemId);
            return item;
        }

        public ShopItemCategory GetCategory(string name)
        {
            ShopItemCategory category = _context.Categories.FirstOrDefault(c => c.Name == name);
            return category;
        }

        public void AddItem(ShopItem item)
        {
            ShopItem existingItem = _context.Items.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
            {
                throw new DuplicateItemException("Duplicate item id: {id}");
            }
            _context.Items.Add(item);
            _context.SaveChanges();
        }

        public void AddCategory(ShopItemCategory category)
        {
            ShopItemCategory existingCategory = _context.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (existingCategory != null)
            {
                throw new DuplicateCategoryException("Duplicate label id: {id}");
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public bool RemoveItem(Guid itemId)
        {
            ShopItem itemToRemove = _context.Items.FirstOrDefault(i => i.Id == itemId);
            if (itemToRemove == null)
            {
                return false;
            }
            _context.Items.Remove(itemToRemove);
            _context.SaveChanges();
            return true;
        }

        public void UpdateItem(ShopItem item)
        {

            _context.Items.AddOrUpdate(item);
            _context.SaveChanges();
        }

        public List<ShopItem> GetAllItems()
        {
            List<ShopItem> items = _context.Items.OrderBy(i => i.Name).ToList();
            return items;
        }

        public List<ShopItemCategory> GetAllCategories()
        {
            List<ShopItemCategory> categories = _context.Categories.OrderBy(c => c.Name).ToList();
            return categories;
        }

        public List<ShopItem> GetFilteredByCategory(ShopItemCategory category)
        {
            List<ShopItem> items = _context.Items.Where(i => i.Categories.Contains(category)).ToList();
            return items;
        }
    }
}