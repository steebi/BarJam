using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class PunterManager
    {
        public PunterManager(GameObject punterToClone, int maxPunters, float spawnPeriod)
        {
            this._punterToClone = punterToClone;
            this._maxPunters = maxPunters;
            this._spawnPeriod = spawnPeriod;
            this._currentPunters = new List<GameObject>();
        }

        private GameObject _punterToClone;
        private int _maxPunters;
        private float _spawnPeriod;
        private Vector3[] _spawnPoints = new Vector3[7] {
                                                        new Vector3(12f, 0f, 15f), new Vector3(12f, 0f, -15f),
                                                        new Vector3(12f, 0f, 10f), new Vector3(12f, 0f, -10f),
                                                        new Vector3(12f, 0f, 5f), new Vector3(12f, 0f, -5f),
                                                        new Vector3(12f, 0f, 0f),
                                                        };

        private DateTime _nextSpawnTime = DateTime.Now;

        private List<GameObject> _currentPunters;

        public void Tick(float deltaTime)
        {
            DateTime Now = DateTime.Now;
            if (this._nextSpawnTime < Now && this._currentPunters.Count < this._maxPunters)
            {
                this.SpawnPunter();
                this.UpdateNextSpawnTime(Now);
            }
            this.Cleanup();
        }

        private void SpawnPunter()
        {
            Debug.Log("Spawning a punter!");
            GameObject punterToAdd = GameObject.Instantiate(this._punterToClone);
            punterToAdd.transform.position = this._spawnPoints[UnityEngine.Random.Range(0, this._spawnPoints.Length)];
            this._currentPunters.Add(punterToAdd);

        }

        private void Cleanup()
        {
            // remove nulls from current punters
        }
        
        private void UpdateNextSpawnTime(DateTime now)
        {
            // TODO: add randomness
            this._nextSpawnTime = now.AddSeconds(this._spawnPeriod);
        }
    }
}
