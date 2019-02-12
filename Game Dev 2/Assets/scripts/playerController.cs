using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    public float speed;
    public float maxSpeed;
    public float sensitivity;
    public float driftSensitivity;
    public float deacceleration;
    public GameObject shipModel;
    public LayerMask ignoreLayer;
    public float normalGravity;
    public float airGravity;

    private bool drifting = false;
    Rigidbody rb;
    private float defaultSensitivity;
    private float change = 0f;
    private float drift_boost;
    private int drift_direction;

    Quaternion leftRotate30 = Quaternion.AngleAxis(-30, Vector3.forward);
    Quaternion rightRotate30 = Quaternion.AngleAxis(30, Vector3.forward);
    Quaternion leftRotate50 = Quaternion.AngleAxis(-50, Vector3.forward);
    Quaternion rightRotate50 = Quaternion.AngleAxis(50, Vector3.forward);
    Quaternion tempQuatR;
    Quaternion tempQuatL;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        defaultSensitivity = sensitivity;
	}

    void Update()
    {
    }

    // Update is called once per frame
    void FixedUpdate () {

        float leftStickX = Input.GetAxis("Horizontal");
        bool gas = Input.GetButton("R2");
        bool brake = Input.GetButton("R1");
        Vector3 force = new Vector3(0,0,0);


        if (isGrounded())
        {
            if (gas)
            {
                rb.AddForce(transform.forward * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            }
            if (brake)
            {
                //if turning and braking ie. drifitng increase turning senesitivity
                if (leftStickX != 0)
                {
                    if (leftStickX > 0)
                    {
                        drift_direction = 1;
                    }
                    else
                    {
                        drift_direction = -1;
                    }
                    sensitivity = driftSensitivity;
                    drifting = true;
                }
                else if (!gas)
                {
                    rb.velocity = rb.velocity * deacceleration;
                    sensitivity = defaultSensitivity;
                }
            }
            //once done braking, reset sensitivity to default
            if (!brake)
            {
                sensitivity = defaultSensitivity;
            }
            //ensure vehicle doesn't exceed max speed
            if (rb.velocity.x > maxSpeed && rb.velocity.z < maxSpeed)
            {
                rb.velocity = new Vector3(maxSpeed, rb.velocity.y, rb.velocity.z);
            }
            else if (rb.velocity.z > maxSpeed && rb.velocity.x < maxSpeed)
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, maxSpeed);
            }
            else if (rb.velocity.x > maxSpeed && rb.velocity.z > maxSpeed)
            {
                rb.velocity = new Vector3(maxSpeed, rb.velocity.y, maxSpeed);
            }
            if (rb.velocity.x < -maxSpeed && rb.velocity.z < maxSpeed)
            {
                rb.velocity = new Vector3(-maxSpeed, rb.velocity.y, rb.velocity.z);
            }
            else if (rb.velocity.z < -maxSpeed && rb.velocity.x < maxSpeed)
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -maxSpeed);
            }
            else if (rb.velocity.x < -maxSpeed && rb.velocity.z < -maxSpeed)
            {
                rb.velocity = new Vector3(-maxSpeed, rb.velocity.y, maxSpeed);
            }

            if (drifting)
            {
                drift_boost += Mathf.Abs(leftStickX * sensitivity * Time.fixedDeltaTime * 50);
                print(drift_boost);
            }

            if (!brake)
            {
                if (drift_boost >= 100)
                {
                    boost();
                }
                drift_boost = 0;
                drifting = false;
                drift_direction = 0;
            }
        }
        float rotDegrees = 180f;
        Vector3 newVelocity = Vector3.RotateTowards(rb.velocity, transform.forward, rotDegrees * Time.deltaTime * Mathf.Deg2Rad, 0);
        rb.velocity = newVelocity;
        if (!gas)
        {
            rb.velocity = rb.velocity * deacceleration;
        }

        if (drifting && drift_direction == 1)
        {
            leftStickX = Input.GetAxis("Horizontal") + 0.6f;
            transform.Rotate(0, leftStickX / 1.6f * sensitivity, 0);
            tempQuatR = new Quaternion(leftRotate50.x, leftRotate50.y, leftRotate50.z * leftStickX / 1.6f, leftRotate50.w);
            shipModel.transform.localRotation = Quaternion.RotateTowards(shipModel.transform.localRotation, tempQuatR, 3f);
        }
        else if (drifting && drift_direction == -1)
        {
            leftStickX = Input.GetAxis("Horizontal") - 0.6f;
            transform.Rotate(0, leftStickX / 1.6f * sensitivity, 0);
            tempQuatL = new Quaternion(rightRotate50.x, rightRotate50.y, rightRotate50.z * -leftStickX / 1.6f, rightRotate50.w);
            shipModel.transform.localRotation = Quaternion.RotateTowards(shipModel.transform.localRotation, tempQuatL, 3f);
        }
        else if (leftStickX != 0)
        {
            transform.Rotate(0, leftStickX * sensitivity, 0);
            if (leftStickX > 0)
            {
                shipModel.transform.localRotation = Quaternion.RotateTowards(shipModel.transform.localRotation, leftRotate30, 1.5f);
            }
            if (leftStickX < 0)
            {
                shipModel.transform.localRotation = Quaternion.RotateTowards(shipModel.transform.localRotation, rightRotate30, 1.5f);
            }
        }
        else
        {
            shipModel.transform.localRotation = Quaternion.RotateTowards(shipModel.transform.localRotation, Quaternion.identity, 1f);
        }   
    }

    public bool isGrounded()
    {
        RaycastHit hit;
        bool ret = Physics.Raycast(transform.position, -Vector3.up, out hit, 2f, ~(1 << 9));
        if (ret)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * hit.distance, Color.green);
            Physics.gravity = new Vector3(0f, normalGravity, -0f);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * hit.distance, Color.red);
            print(change + " Not Grounded");
            Physics.gravity = new Vector3(0f, airGravity, -0f);
        }
        change++;

        return ret;
    }

    void boost()
    {

    }
}
