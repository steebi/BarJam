using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class PunterBehaviour : MonoBehaviour
{
    private Vector3 _speedVector;
    private float _speed = 10f;
    public PunterController PunterController;


    void Awake()
    {
        this.PunterController = new PunterController();
    }

    void Start()
    {
        this._speedVector = new Vector3(-this._speed, 0f, 0f);
    }

    void Update()
    {
        if (this.PunterController.State == PunterState.ApproachingBar)
        {
            gameObject.transform.position += this._speedVector * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Counter")
        {
            this.PunterController.State = PunterState.AtBar;
        }
    }
}