using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    public float speed;
    public float maxSpeed;
    public float sensitivity;
    public float deacceleration;

    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float leftStickX = Input.GetAxis("Horizontal");
        bool gas = Input.GetButton("R2");
        bool brake = Input.GetButton("R1");
        Vector3 force = new Vector3(0,0,0);
        if (gas)
        {
            rb.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.VelocityChange);

            if (rb.velocity.x > maxSpeed && rb.velocity.z < maxSpeed)
            {
                rb.velocity = new Vector3(maxSpeed, 0f, rb.velocity.z);
            }
            else if (rb.velocity.z > maxSpeed && rb.velocity.x < maxSpeed)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0f, maxSpeed);
            }
            else if (rb.velocity.x > maxSpeed && rb.velocity.z > maxSpeed)
            {
                rb.velocity = new Vector3(maxSpeed, 0f, maxSpeed);
            }
        }
        else if (brake)
        {
            if(leftStickX != 0)
            {
                sensitivity += 100;
            }
            else
            {
                rb.velocity = rb.velocity * deacceleration;
            }
        }
        if(leftStickX != 0 && !brake)
        {
            sensitivity = 150;
        }

        print(sensitivity);

        float rotDegrees = 180f;
        Vector3 newVelocity = Vector3.RotateTowards(rb.velocity, transform.forward, rotDegrees * Time.deltaTime * Mathf.Deg2Rad, 0);
        rb.velocity = newVelocity;
        transform.Rotate(0, leftStickX * sensitivity * Time.deltaTime, 0);

        //Quaternion rotateAmount =  Quaternion.Euler(0, leftStickX * sensitivity * Time.deltaTime, 0);
        //rb.MoveRotation(rotateAmount);
	}
}
