using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class DrinkSourceController
    {
        private Item _storedItem;

        public DrinkSourceController(ItemType type, int count)
        {
            this._storedItem = new Item(type, count);
        }

        public ItemType GetItem()
        {
            // TODO: can handle cooldown and duration here
            this._storedItem.Count--;
            return this._storedItem.type;
        }
    }
}
