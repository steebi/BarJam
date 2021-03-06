﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class BartenderController
    {
        private Inventory _inventory = new Inventory();
        private Dictionary<long, Inventory> _orders = new Dictionary<long, Inventory>();

        public float TotalTips = 0f;

        public Inventory TalkToPunter(PunterController punterController)
        {
            if(this._orders.ContainsKey(punterController.Id))
            {
                // reminder for order (for debugging)
                Debug.LogWarning("ORDER_REMINDER");
                this._orders[punterController.Id].Log();
                if (punterController.State == PunterState.AtBar)
                {
                    return this.DeliverDrink(punterController);
                }
                return null;
            }
            else
            {
                Inventory order = this.RequestOrder(punterController);
                Debug.LogWarning("ORDER");
                order.Log();
                return order;
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
                this.TotalTips += punterController.Satisfy();
                Debug.LogFormat("Total Tips: {0}.", this.TotalTips);
            }
            else
            {
                // if wrong, just return. in future maybe make the punter more angry
            }
            return null;
        }
    }
}
