using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class PunterBehaviour : MonoBehaviour {

    public PunterController punterController;

    public float Speed;
    private Vector3 _speedVector;

	// Use this for initialization
	void Start () {
        punterController = new PunterController();
        
	}

    void Update()
    {
        //gameObject.transform.position -= this.Speed * Time.deltaTime;
    }
}
