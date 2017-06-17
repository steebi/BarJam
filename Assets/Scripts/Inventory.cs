using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Inventory
    {
        private List<Item> _storedItems;

        public void Add(Item item)
        {
            this._storedItems.Add(item);
        }
    }
}
