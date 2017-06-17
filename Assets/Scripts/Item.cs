using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public enum ItemType
    {
        Water = 0,
        Beer = 1,
        Whiskey = 2,
        Coke = 3
    }

    public class Item
    {
        public int Count;
        public ItemType type;

        public Item(int count, ItemType type)
        {
            this.Count = count;
            this.type = type;
        }
    }
}
