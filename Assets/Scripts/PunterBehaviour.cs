using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class PunterBehaviour : MonoBehaviour {

    public PunterController punterController;

    [SerializeField]
    private CapsuleCollider fixedColliderTransform;

	// Use this for initialization
	void Start () {
        punterController = new PunterController();
	}

    private void Update()
    {
        // Removed this. Will disable the collider when they are leaving instead
        // FixedColliderTransform.transform.rotation = Quaternion.identity;
    }
}
