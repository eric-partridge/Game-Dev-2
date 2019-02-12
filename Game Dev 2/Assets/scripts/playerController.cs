using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    public float speed;
    public float maxSpeed;
    public float sensitivity;
    public float driftSensitivity;
    public float deacceleration;

    Rigidbody rb;
    private float defaultSensitivity;
    private float change = 0f;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        defaultSensitivity = sensitivity;
	}

    void Update()
    {
        print("Time: " + change + " sens: " + sensitivity);
        change++;
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
        }
        if (brake)
        {
            if(leftStickX != 0)
            {
                sensitivity = driftSensitivity;
            }
            else
            {
                rb.velocity = rb.velocity * deacceleration;
            }
        }
        if(!brake)
        {
            sensitivity = defaultSensitivity;
        }

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

        if (rb.velocity.x < -maxSpeed && rb.velocity.z < maxSpeed)
        {
            rb.velocity = new Vector3(-maxSpeed, 0f, rb.velocity.z);
        }
        else if (rb.velocity.z < -maxSpeed && rb.velocity.x < maxSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, -maxSpeed);
        }
        else if (rb.velocity.x < -maxSpeed && rb.velocity.z < -maxSpeed)
        {
            rb.velocity = new Vector3(-maxSpeed, 0f, maxSpeed);
        }

        float rotDegrees = 180f;
        Vector3 newVelocity = Vector3.RotateTowards(rb.velocity, transform.forward, rotDegrees * Time.deltaTime * Mathf.Deg2Rad, 0);
        rb.velocity = newVelocity;
        transform.Rotate(0, leftStickX * sensitivity, 0);
	}
}
