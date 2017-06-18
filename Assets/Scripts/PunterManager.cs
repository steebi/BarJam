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
            this._spawnPoints = new Vector3[7];
            this._unOccupiedQueueIndexes = new List<int>();

            for (int i = 0; i < this.numSpawnPoints; i++)
            {
                this._spawnPoints[i] = new Vector3(12f, 4f, 5f * (i - 3));
                this._unOccupiedQueueIndexes.Add(i);
            }
        }

        private GameObject _punterToClone;
        private int _maxPunters;
        private float _spawnPeriod;
        private int punterID;
        private int numSpawnPoints = 7;

        private Vector3[] _spawnPoints;
        private List<int> _unOccupiedQueueIndexes;

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
            // select from the list of spawn points, and remove it from consideration next spawn
            if (this._unOccupiedQueueIndexes.Count == 0)
            {
                Debug.LogError("Cannot spawn a new punter as all queues are occupied.");
                return;
            }

            Debug.Log("Spawning a punter!");
            GameObject punterToAdd = GameObject.Instantiate(this._punterToClone);

            int selectedQueueIndex = this._unOccupiedQueueIndexes[UnityEngine.Random.Range(0, this._unOccupiedQueueIndexes.Count)];
            this._unOccupiedQueueIndexes.Remove(selectedQueueIndex);
            punterToAdd.transform.position = this._spawnPoints[selectedQueueIndex];

            PunterController punterController = punterToAdd.GetComponent<PunterBehaviour>().PunterController;
            punterController.State = PunterState.ApproachingBar;
            punterController.Id = this.punterID;
            punterID++;
            this._currentPunters.Add(punterToAdd);

        }

        private void Cleanup()
        {
            // remove nulls from current punters
            for (int i = 0; i < this._currentPunters.Count; i++)
            {
                if(this._currentPunters[i] == null)
                {
                    this._currentPunters.RemoveAt(i);
                    this._unOccupiedQueueIndexes.Add(i);
                }
            }
        }
        
        private void UpdateNextSpawnTime(DateTime now)
        {
            // TODO: add randomness
            this._nextSpawnTime = now.AddSeconds(this._spawnPeriod);
        }
    }
}
