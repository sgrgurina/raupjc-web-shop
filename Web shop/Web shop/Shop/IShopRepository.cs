using System;
using System.Collections.Generic;

namespace Webshop.Shop
{
    interface IShopRepository
    {
        /// <summary>
        /// Gets item with given id.
        /// </summary>
        /// <param name="itemId"> Id of the item </param>
        /// <returns>Item if found, null otherwise </returns>
        ShopItem GetItem(Guid itemId);

        /// <summary>
        /// Adds item to database. If item already exists throws DuplicateItemException.
        /// </summary>
        /// <param name="item">Item to add to database.</param>
        void AddItem(ShopItem item);

        /// <summary>
        /// Remove item with given id from database.
        /// </summary>
        /// <param name="itemId">Id of the item to remove.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool RemoveItem(Guid itemId);

        /// <summary>
        /// Updates item in database.
        /// </summary>
        /// <param name="item">Item to update.</param>
        void UpdateItem(ShopItem item);

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
