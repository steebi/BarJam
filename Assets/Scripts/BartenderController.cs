using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class BartenderController
    {
        private Inventory _inventory = new Inventory();
        private Dictionary<long, Inventory> _orders = new Dictionary<long, Inventory>();

        private float _totalTips = 0f;

        public Inventory TalkToPunter(PunterController punterController)
        {
            if(this._orders.ContainsKey(punterController.Id))
            {
                if(punterController.State == PunterState.AtBar)
                {
                    return this.DeliverDrink(punterController);
                }
                return null;
            }
            else
            {
                return this.RequestOrder(punterController);
            }
        }

        public void AccessDrinkSource(DrinkSourceController drinkSourceController)
        {
            ItemType CollectedItemType = drinkSourceController.GetItem();
            this._inventory.Add(CollectedItemType, 1);
        }

        public void LogCurrentInventory()
        {
            this._inventory.Log();
        }

        private Inventory RequestOrder(PunterController punterController)
        {
            Inventory puntersOrder = punterController.GiveOrder();
            this._orders[punterController.Id] = puntersOrder;
            return puntersOrder;
        }

        private Inventory DeliverDrink(PunterController punterController)
        {
            // check if the order is wrong
            Inventory order = this._orders[punterController.Id];
            if(this._inventory.TryRemove(order))
            {
                this._totalTips += punterController.Satisfy();
                Debug.LogFormat("Total Tips: {0}.", this._totalTips);
            }
            else
            {
                // if wrong, just return. in future maybe make the punter more angry
            }
            return null;
        }
    }
}
