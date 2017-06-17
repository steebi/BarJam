﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class PunterController
    {
        private Inventory _order;
        private float _angerLevel = 0f; // 0 to 1.
        private float _impatience = 1f; // multiply 
        private bool _hasGivenOrder = false;

        public Inventory GiveOrder()
        {
            // can alter anger level here if needed?
            if (this._hasGivenOrder)
            {
                this._angerLevel += 0.05f;
            }
            else
            {
                this._angerLevel -= 0.1f;
                this._hasGivenOrder = true;
            }
            return this._order;
        }

        public void Tick(float deltaTime)
        {
            this._angerLevel += this._impatience * deltaTime * _impatience;
        }
    }
}