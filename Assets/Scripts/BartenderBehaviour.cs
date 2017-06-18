using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class BartenderBehaviour : MonoBehaviour {

    public int speed;

    private Rigidbody rb;
    private BartenderController _bartenderController;
    private bool punterInRange = false;
    private PunterBehaviour punter = null;
    private DrinkSourceBehaviour drinkSourceBehavour = null;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _bartenderController = new BartenderController();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("TalkToPunter"))
        {
            Debug.Log("Trying to talk to Punter!");
            if (punter != null)
            {
                _bartenderController.TalkToPunter(punter.PunterController);
                Debug.Log("Talking to Punter");
            }
            else if (this.drinkSourceBehavour != null)
            {
                _bartenderController.AccessDrinkSource(this.drinkSourceBehavour.DrinkSourceController);
                Debug.Log("Accessing Drink Source");
            }
        }
    }

    private void FixedUpdate()
    {
        float MoveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = -(Input.GetAxis("Vertical"));

        Vector3 movement = new Vector3(MoveHorizontal, 0.0f, moveVertical);
        movement = Quaternion.Euler(0, -45, 0) * movement;

        rb.AddForce(movement * speed);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Punter")
        {
            Debug.Log("Attaching Punter.");
            punter = other.GetComponentInParent<PunterBehaviour>();
        }
        else if (other.tag == "DrinkSource")
        {
            Debug.Log("Attaching DrinkSource.");
            this.drinkSourceBehavour = other.GetComponent<DrinkSourceBehaviour>();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Punter")
        {
            Debug.Log("Detaching Punter");
            punter = null;
        }
        else if (other.tag == "DrinkSource")
        {
            Debug.Log("Detaching DrinkSource.");
            this.drinkSourceBehavour = null;
        }
    }
}
