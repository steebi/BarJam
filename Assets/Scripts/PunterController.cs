﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public enum PunterState
    {
        ApproachingBar = 0,
        AtBar = 1,
        ReturningToCrowd = 2
    }

    public class PunterController
    {
        // TODO: hardcoded for now, randomise in a constructor or in generator
        private Inventory _order = new Inventory(new List<ItemType>() { ItemType.Beer }, new List<int> { 1 });
        private float _angerLevel = 0f; // 0 to 1.
        private float _impatience = 1f; // multiply 
        private bool _hasGivenOrder = false;
        
        [SerializeField]
        private bool _isSatisfied = false;

        public PunterState State = PunterState.AtBar;
        public long Id = 1L;

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

        public float Satisfy()
        {
            this.State = PunterState.ReturningToCrowd;
            return 1.5f;
        }

        public void Tick(float deltaTime)
        {
            this._angerLevel += this._impatience * deltaTime * _impatience;
            if (this._angerLevel >= 1f)
            {
                // flip the fuck out
            }
        }
    }
}
