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
    public float boostChange;
    public GameObject camReference;

    private bool drifting = false;
    Rigidbody rb;
    private float defaultSensitivity;
    private float change = 0f;
    private float drift_boost;
    private int drift_direction;
    private bool boosting = false;
    private float boostTime = -2f;

    Quaternion leftRotate30 = Quaternion.AngleAxis(-30, Vector3.forward);
    Quaternion rightRotate30 = Quaternion.AngleAxis(30, Vector3.forward);
    Quaternion leftRotate50 = Quaternion.AngleAxis(-50, Vector3.forward);
    Quaternion rightRotate50 = Quaternion.AngleAxis(50, Vector3.forward);
    Quaternion upRotate10 = Quaternion.AngleAxis(-50, Vector3.forward);
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

        print(Input.GetAxis("RSX"));
        //print(Input.GetAxis("RSY"));

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
                //print(drift_boost);
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

        if((boostTime + 0.75f) <= Time.fixedTime && boosting){
            maxSpeed /= boostChange;
            boosting = false;
            //camReference.GetComponent<cameraScript>().rotateCamera(-5f);
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
        print("Boosting?" + boosting);
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
            Physics.gravity = new Vector3(0f, airGravity, -0f);
        }
        change++;
        return ret;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Boost")
        {
            // UP
            if(other.GetComponent<Boost_Pad>().GetType2() == "UP" && other.GetComponent<Boost_Pad>().GetDirection() == 1)
            {
                if(Input.GetAxis("RSY") > 0.3)
                {
                    rb.AddForce(transform.forward * 2 * speed , ForceMode.VelocityChange);
                }
            }
            // DOWN
            if (other.GetComponent<Boost_Pad>().GetType2() == "UP" && other.GetComponent<Boost_Pad>().GetDirection() == -1)
            {
                if (Input.GetAxis("RSY") < -0.3)
                {
                    rb.AddForce(-transform.forward * 2 * speed, ForceMode.VelocityChange);
                }
            }
            // LEFT
            if (other.GetComponent<Boost_Pad>().GetType2() == "RIGHT" && other.GetComponent<Boost_Pad>().GetDirection() == -1)
            {
                if (Input.GetAxis("RSX") < -0.3)
                {
                    rb.velocity = new Vector3(0, 0, 0);
                    rb.AddForce(new Vector3(-1,0,0) * 1.5f * speed, ForceMode.VelocityChange);
                    //rb.velocity = rb.velocity - Vector3.Project(rb.velocity,  transform.forward);
                }
            }
            // RIGHT
            if (other.GetComponent<Boost_Pad>().GetType2() == "RIGHT" && other.GetComponent<Boost_Pad>().GetDirection() == 1)
            {
                if (Input.GetAxis("RSX") > 0.3)
                {
                    rb.velocity = new Vector3(0, 0, 0);
                    rb.AddForce(new Vector3(1, 0, 0) * 1.5f * speed, ForceMode.VelocityChange);
                }
            }
        }
    }

    void boost()
    {
        maxSpeed = maxSpeed * boostChange;
        boostTime = Time.fixedTime;
        boosting = true;
    }
}
