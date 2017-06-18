using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class BartenderBehaviour : MonoBehaviour {

    public int speed;

    private Rigidbody rb;
    private BartenderController bc;
    private bool punterInRange = false;
    private PunterBehaviour punter = null;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = new BartenderController();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("TalkToPunter") != 0)
        {
            Debug.Log("Trying to talk to Punter!");
            if (punter != null)
            {
                bc.TalkToPunter(punter.punterController);
                Debug.Log("Talking to Punter");
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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Punter")
        {
            Debug.Log("Detaching Punter");
            punter = null;
        }
    }
}
