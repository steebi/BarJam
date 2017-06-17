using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed;
    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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

        rb.AddForce(movement*speed);
        
    }
}
