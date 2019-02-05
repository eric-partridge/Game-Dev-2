using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    public float speed;
    public float sensitivity;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float leftStickX = Input.GetAxis("Horizontal");
        bool gas = Input.GetButton("R2");
        bool reverse = Input.GetButton("L2");
        Vector3 force = new Vector3(0,0,0);
        if (gas)
        {
            //force = new Vector3(speed * transform.forward, 0f, 0f);
            rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
        }
        else if (reverse)
        {
            rb.AddForce(transform.forward * -speed, ForceMode.VelocityChange);
        }

        //Quaternion deltaRotation = Quaternion.AngleAxis(leftStickX*sensitivity*Time.deltaTime, transform.up);

        //rb.rotation = deltaRotation;
	}
}
