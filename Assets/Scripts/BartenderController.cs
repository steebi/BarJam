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

        public void RequestOrder(PunterController punterController)
        {
            this._orders[punterController.Id] = punterController.GiveOrder();
        }

        public void DeliverDrink(PunterController punterController)
        {
            // check if the order is wrong

            // if wrong, just return. in future maybe make the punter more angry
            // if right, 
        }
    }
}
