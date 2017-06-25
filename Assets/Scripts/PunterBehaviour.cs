using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class PunterBehaviour : MonoBehaviour
{
    public PunterController punterController;
    public float orderDisplayTime = 5.0f;
    public float displayTime;

    private Vector3 _speedVector;
    private float _speed = 10f;
    private Canvas punterCanvas;

    void Awake()
    {
        this.punterController = new PunterController();
    }

    void Start()
    {
        this._speedVector = new Vector3(-this._speed, 0f, 0f);
        punterCanvas = GetComponent<Canvas>();
        punterCanvas.enabled = false;
    }

    void Update()
    {
        // TODO: probably switch case
        if (this.punterController.State == PunterState.ApproachingBar)
        {
            gameObject.transform.position += this._speedVector * Time.deltaTime;
        }
        else if(this.punterController.State == PunterState.ReturningToCrowd)
        {
            gameObject.transform.position -= this._speedVector * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Counter")
        {
            this.punterController.State = PunterState.AtBar;
        }
        if (other.tag == "Crowd" && this.punterController.State == PunterState.ReturningToCrowd)
        {
            Destroy(gameObject);
        }
    }

    public void ShowOrder()
    {
        Inventory inv = this.punterController.GiveOrder();
    }

    public IEnumerable DisplayOrder()
    {

        Inventory inv = punterController.GiveOrder();

        // Instantiate the spritess
        foreach (var item in inv)
        {

        }

        yield return new WaitForSeconds(displayTime);
        // Destroy the sprites
        yield return null;
    }
}