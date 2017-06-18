using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class PunterManager
    {
        public GameObject punterToClone;

        public int MaxPunters;
        public float SpawnPeriod;

        private DateTime _nextSpawnTime = DateTime.Now;

        private List<int> _punterIDs;

        public void Tick(float deltaTime)
        {
            DateTime Now = DateTime.Now;
            if (this._nextSpawnTime > Now)
            {
                this._punterIDs.Add(this.SpawnPunter());
                this.UpdateNextSpawnTime(Now);
            }
        }

        private int SpawnPunter()
        {
            Debug.Log("Spawning a punter!");
            return 1;
        }
        
        private void UpdateNextSpawnTime(DateTime now)
        {
            // TODO: add randomness
            this._nextSpawnTime = now.AddSeconds(this.SpawnPeriod);
        }
    }
}
