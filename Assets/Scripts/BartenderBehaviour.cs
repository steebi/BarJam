using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class BartenderBehaviour : MonoBehaviour {

    public int speed;

    private Rigidbody rb;
    public BartenderController BartenderController;
    private bool punterInRange = false;
    private PunterBehaviour punter = null;
    private DrinkSourceBehaviour drinkSourceBehavour = null;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        BartenderController = new BartenderController();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("TalkToPunter"))
        {
            Debug.Log("Trying to talk to Punter!");
            if (punter != null)
            {
<<<<<<< HEAD
                Inventory orderList = _bartenderController.TalkToPunter(punter.punterController);

                // If the order list is not null then inform us of the order
                if (orderList != null)
                {
                    // Punter object is called to show what drinks are wanted
                    punter.ShowOrder();
                }

=======
                BartenderController.TalkToPunter(punter.PunterController);
>>>>>>> 7dfaab3ebfd24456a9b8fcd22c736cd1d42f2eb1
                Debug.Log("Talking to Punter");
            }
            else if (this.drinkSourceBehavour != null)
            {
                BartenderController.AccessDrinkSource(this.drinkSourceBehavour.DrinkSourceController);
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
