using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class PlayerController : MonoBehaviour
{
    public int speed;

    private Rigidbody rb;
    private BartenderController bc;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = new BartenderController();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        float MoveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = -(Input.GetAxis("Vertical"));

        Vector3 movement = new Vector3(MoveHorizontal, 0.0f, moveVertical);
        movement = Quaternion.Euler(0, -45, 0) * movement;

        rb.AddForce(movement*speed);
        
    }
}
