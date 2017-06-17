using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class PunterBehaviour : MonoBehaviour {

    public PunterController punterController;

	// Use this for initialization
	void Start () {
        punterController = new PunterController();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
