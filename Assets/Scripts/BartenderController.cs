using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class BartenderController
    {
        private Inventory _inventory;
        private Dictionary<long, Inventory> _orders;

        public void TalkToPunter(PunterController punterController)
        {
            if(this._orders.ContainsKey(punterController.Id))
            {
                this.DeliverDrink(punterController);
            }
            else
            {
                this.RequestOrder(punterController);
            }
        }

        public void AccessDrinkSource(DrinkSourceController drinkSourceController)
        {
            ItemType CollectedItemType = drinkSourceController.GetItem();
            this._inventory.Add(CollectedItemType, 1);
        }

        private void RequestOrder(PunterController punterController)
        {
            this._orders[punterController.Id] = punterController.GiveOrder();
        }

        private void DeliverDrink(PunterController punterController)
        {
            // check if the order is wrong
            Inventory order = this._orders[punterController.Id];
            if(this._inventory.TryRemove(order))
            {
                punterController.Satisfy();
            }
            else
            {
                // if wrong, just return. in future maybe make the punter more angry
            }
        }
    }
}
