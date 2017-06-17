using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Inventory
    {
        private Dictionary<ItemType, Item> _storedItems;

        public void Add(Item item)
        {
            Item alreadyStoredItem;
            this._storedItems.TryGetValue(item.type, out alreadyStoredItem);
            if (alreadyStoredItem == null)
            {
                _storedItems[item.type] = new Item(item.Count, item.type);
            }
            else
            {
                _storedItems[item.type].Count += item.Count;
            }
        }
    }
}
