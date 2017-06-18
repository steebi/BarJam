using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class CrowdBehaviour : MonoBehaviour {

    public GameObject PunterToClone;
    public int MaxPunters;
    public float SpawnPeriod;

    private PunterManager _punterManager;

	// Use this for initialization
	void Start () {
        this._punterManager = new PunterManager(PunterToClone, MaxPunters, SpawnPeriod);
	}
	
	// Update is called once per frame
	void Update () {
        _punterManager.Tick(Time.deltaTime);
	}
}
