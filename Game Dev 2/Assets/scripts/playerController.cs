﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    public float speed;
    public float maxSpeed;
    public float sensitivity;
    public float driftSensitivity;
    public float deacceleration;
    public GameObject shipModel;
    public GameObject pitchGO;
    public LayerMask ignoreLayer;
    public float normalGravity;
    public float airGravity;
    public float boostChange;
    public GameObject camReference;
    public bool grounded;
    public Vector3 rayNormal;
    public int playerNum;

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
        rb = this.GetComponent<Rigidbody>();
        defaultSensitivity = sensitivity;
        print("Controller name for player: " + (playerNum-1).ToString() + " is: " + Input.GetJoystickNames()[playerNum-1]);
	}

    void Update()
    {
    }

    // Update is called once per frame
    void FixedUpdate () {

        // print(Input.GetAxis("RSX"));
        //print(Input.GetAxis("RSY"));
        string R2Button = "R2 " + this.tag;
        print("New R2: " + R2Button);

        float leftStickX = Input.GetAxis("Horizontal");
        bool gas = Input.GetButton("R2 P" + playerNum.ToString());
        bool brake = Input.GetButton("R1");
        Vector3 force = new Vector3(0,0,0);


        if (isGrounded())
        {
            if (gas)
            {
                print("R2: " + R2Button + " with player: " + this.tag);
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
            print("Not boosting");
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
    }

    public bool isGrounded()
    {
        RaycastHit hit;
        bool ret = Physics.Raycast(transform.position, -Vector3.up, out hit, 3f, ~(1 << 9));
        if (ret)
        {
            Debug.DrawRay(transform.position, Vector3.down * hit.distance, Color.green);
            Physics.gravity = new Vector3(0, normalGravity, 0); //new Vector3(-hit.normal.x, normalGravity * hit.normal.y, -hit.normal.z);
            print("Normal: " + hit.normal);

            /*Quaternion t = pitchGO.transform.rotation * Quaternion.FromToRotation(pitchGO.transform.up, hit.normal);
            pitchGO.transform.rotation = Quaternion.RotateTowards(pitchGO.transform.rotation, t, 1f);

            Vector3 diff = pitchGO.transform.up - hit.normal;
            print("Diff: " + diff);

            Vector3 new_rot = pitchGO.transform.rotation.eulerAngles + diff;

            Vector3 newVelocity = Vector3.RotateTowards(rb.velocity, transform.forward + diff, 180f * Time.deltaTime * Mathf.Deg2Rad, 0);
            //rb.velocity = newVelocity;

            Vector3 tempX = new Vector3(hit.normal.x, transform.up.y, transform.up.z);
            Vector3 tempY = new Vector3(transform.up.x, hit.normal.y, transform.up.z);
            Vector3 tempZ = new Vector3(transform.up.x, transform.up.y, hit.normal.z);


            float angleX = Vector3.Angle(tempX, transform.up);
            float angleY = Vector3.Angle(tempY, transform.up);
            float angleZ = Vector3.Angle(tempZ, transform.up);

            Vector3 newAngle = new Vector3(angleX, angleY, angleZ);

            Quaternion newQuat = Quaternion.Euler(newAngle);

            //Vector3 pitchRot = pitchGO.transform.rotation.eulerAngles + diff;
            //pitchGO.transform.Rotate(Vector3.Angle(hit.normal, pitchGO.transform.up)*Vector3.up);
            rayNormal = hit.normal;


            //Quaternion normalRotate = Quaternion.Euler(hit.normal);
            //print(normalRotate);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, transform.localRotation*newQuat, 1f);*/
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * hit.distance, Color.red);
            Physics.gravity = new Vector3(0f, airGravity, -0f);
        }
        change++;
        print("Is grounded: " + ret);
        grounded = ret;
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
                    rb.AddForce(-other.transform.right * 1.5f * speed, ForceMode.VelocityChange);
                    //rb.velocity = rb.velocity - Vector3.Project(rb.velocity,  transform.forward);
                }
            }
            // RIGHT
            if (other.GetComponent<Boost_Pad>().GetType2() == "RIGHT" && other.GetComponent<Boost_Pad>().GetDirection() == 1)
            {
                if (Input.GetAxis("RSX") > 0.3)
                {
                    rb.velocity = new Vector3(0, 0, 0);
                    rb.AddForce(other.transform.right * 1.5f * speed, ForceMode.VelocityChange);
                }
            }
        }
    }

    void boost()
    {
        if (!boosting){
            maxSpeed = maxSpeed * boostChange;
            boostTime = Time.fixedTime;
            boosting = true;
        }        
    }
}
