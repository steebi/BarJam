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
            this.Add(item.type, item.Count);
        }

        public void Add(ItemType type, int count)
        {
            Item alreadyStoredItem;
            if (this._storedItems.TryGetValue(type, out alreadyStoredItem))
            {
                alreadyStoredItem.Count += count;
            }
            else
            {
                _storedItems[type] = new Item(type, count);
            }
        }

        public Item Get(ItemType type)
        {
            return this._storedItems[type];
        }

        public IEnumerator<KeyValuePair<ItemType, Item>> GetEnumerator()
        {
            return this._storedItems.GetEnumerator();
        }

        public void Update(Inventory inventory)
        // update the type interies in the given inventory to the given count
        {
            foreach(KeyValuePair<ItemType, Item> kvp in inventory)
            {
                Item alreadyStoredItem;
                if (this._storedItems.TryGetValue(kvp.Key, out alreadyStoredItem))
                {
                    alreadyStoredItem.Count = kvp.Value.Count;
                }
                else
                {
                    this._storedItems[kvp.Key] = kvp.Value;
                }
            }
        }

        public bool TryRemove(Inventory inventory)
        {
            Inventory potentialNewInventory = new Inventory();
            // AOF this is probably not super efficient
            foreach(KeyValuePair<ItemType, Item> kvp in inventory)
            {
                Item foundItem;
                if(this._storedItems.TryGetValue(kvp.Key, out foundItem))
                {
                    potentialNewInventory.Add(new Item(kvp.Key, foundItem.Count - kvp.Value.Count));
                    if(potentialNewInventory.Get(kvp.Key).Count < 0)
                    {
                        Debug.LogFormat("Could not remove Inventory. Item of type {0} was more ({1}) than current amount ({2}).", kvp.Key, kvp.Value.Count, foundItem.Count);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            this.Update(inventory);
            return true;
        }
    }
}
