using System;
using System.Collections.Generic;

namespace Webshop.Shop
{
    public interface IShopRepository
    {
        /// <summary>
        /// Gets item with given id.
        /// </summary>
        /// <param name="itemId"> Id of the item </param>
        /// <returns>Item if found, null otherwise </returns>
        ShopItem GetItem(Guid itemId);

        /// <summary>
        /// Gets category with given name from repository.
        /// </summary>
        /// <param name="name">Name of the category</param>
        /// <returns>Category if exists, null otherwise. </returns>
        ShopItemCategory GetCategoryByName(string name);

        /// <summary>
        /// Gets category with given id from repository.
        /// </summary>
        /// <param name="id">Id of the category</param>
        /// <returns>Category if it exists, null otherwise. </returns>
        ShopItemCategory GetCategoryById(Guid id);

        /// <summary>
        /// Adds item to database. If item already exists throws DuplicateItemException.
        /// </summary>
        /// <param name="item">Item to add to database.</param>
        void AddItem(ShopItem item);

        /// <summary>
        /// Add category to database. If category with same id already exists throws DuplicateCategoryException.
        /// </summary>
        /// <param name="category">Category to add</param>
        void AddCategory(ShopItemCategory category);

        /// <summary>
        /// Remove item with given id from database.
        /// </summary>
        /// <param name="itemId">Id of the item to remove.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool RemoveItem(Guid itemId);

        /// <summary>
        /// Remove category with given id from database.
        /// </summary>
        /// <param name="categoryId">Id of the category to remove.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool RemoveCategory(Guid categoryId);

        /// <summary>
        /// Updates item in database. If it does not exist, add one.
        /// </summary>
        /// <param name="item">Item to update.</param>
        void UpdateItem(ShopItem item);

        /// <summary>
        /// Updates category in the database. If it does not exist, add one.
        /// </summary>
        /// <param name="category">Category to update.</param>
        void UpdateCategory(ShopItemCategory category);

        /// <summary>
        /// Gets all items from database.
        /// </summary>
        /// <returns>List of all items.</returns>
        List<ShopItem> GetAllItems();

        /// <summary>
        /// Gets all categories from database.
        /// </summary>
        /// <returns>List of categories.</returns>
        List<ShopItemCategory> GetAllCategories();

        /// <summary>
        /// Gets all items that are in the category.
        /// </summary>
        /// <param name="category">Category of items we are looking for</param>
        /// <returns>List of all items with the category </returns>
        List<ShopItem> GetFilteredByCategory(ShopItemCategory category);
        

    }
}
