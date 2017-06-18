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
            this.numSpawnPoints = maxPunters;
            this._punterToClone = punterToClone;
            this._spawnPeriod = spawnPeriod;
            this._currentPunters = new GameObject[this.numSpawnPoints];
            this._spawnPoints = new Vector3[this.numSpawnPoints];
            this._unOccupiedQueueIndexes = new List<int>();

            for (int i = 0; i < this.numSpawnPoints; i++)
            {
                this._spawnPoints[i] = new Vector3(12f, 4f, 5f * (i - 3));
                this._unOccupiedQueueIndexes.Add(i);
            }
        }

        private GameObject _punterToClone;
        private float _spawnPeriod;
        private int punterID;
        private int numSpawnPoints;

        private Vector3[] _spawnPoints;
        private GameObject[] _currentPunters;
        private int _currentNumberOfPunters;

        private List<int> _unOccupiedQueueIndexes;

        private DateTime _nextSpawnTime = DateTime.Now;


        public void Tick(float deltaTime)
        {
            DateTime Now = DateTime.Now;
            if (this._nextSpawnTime < Now)
            {
                this.SpawnPunter();
                this.UpdateNextSpawnTime(Now);
            }
            this.UpdatePunters();
        }

        private void SpawnPunter()
        {
            // select from the list of spawn points, and remove it from consideration next spawn
            if (this._unOccupiedQueueIndexes.Count == 0)
            {
                Debug.LogError("Cannot spawn a new punter as all queues are occupied.");
                return;
            }

            int selectedQueueIndex = this._unOccupiedQueueIndexes[UnityEngine.Random.Range(0, this._unOccupiedQueueIndexes.Count)];
            if (this._currentPunters[selectedQueueIndex] != null)
            {
                Debug.LogErrorFormat("Cannot use queue index {0} to spawn a punter. already one there!", selectedQueueIndex);
                return;
            }

            Debug.Log("Spawning a punter!");
            GameObject punterToAdd = GameObject.Instantiate(this._punterToClone);

            Debug.LogFormat("Removing queue index {0}.", selectedQueueIndex);
            this._unOccupiedQueueIndexes.Remove(selectedQueueIndex);
            punterToAdd.transform.position = this._spawnPoints[selectedQueueIndex];

            PunterController punterController = punterToAdd.GetComponent<PunterBehaviour>().PunterController;
            punterController.State = PunterState.ApproachingBar;
            punterController.Id = this.punterID;
            punterID++;
            this._currentPunters[selectedQueueIndex] = punterToAdd;
        }

        private void UpdatePunters()
        {
            for (int i = 0; i < this._currentPunters.Length; i++)
            {
                if(this._currentPunters[i] == null)
                {
                    // AOF: this is horrifically inefficient, sorry everyone.
                    if (!this._unOccupiedQueueIndexes.Contains(i))
                    {
                        this._unOccupiedQueueIndexes.Add(i);
                    }
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
