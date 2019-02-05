using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    private float Max_speed = 8750;
    private float Max_speed_holder;
    private float Max_Boost;
    private float Acceleration = 1875;
    private float Decceleration;
    private float Max_Turn = 1.2f;
    private float angle;
    private float Current_Speed = 0;
    private Vector3 Move;
    private Vector3 Turning;
    private int drift_direction = 1;
    private bool drifting = false;
    private float boost_build = 0;
    private int boost_hit = 100;
    private float boost_coef = 2f;
    private float boost_time = -2;
    private bool boosting = false;
    float Turn;
    private GameObject bike;
    private GameObject bikeModel;
    private Rigidbody bikeBody;
    private Vector3 startPos;
    bool air = false;
    private float Gas_Pedal;
    private float NitroTank = 0f;
    private float MaxNitro = 100;
    private bool Nitro_Boost = false;

    Quaternion leftRotate30 = Quaternion.AngleAxis(-30, Vector3.forward);
    Quaternion rightRotate30 = Quaternion.AngleAxis(30, Vector3.forward);
    Quaternion leftRotate50 = Quaternion.AngleAxis(-50, Vector3.forward);
    Quaternion rightRotate50 = Quaternion.AngleAxis(50, Vector3.forward);
    Quaternion tempQuatR;
    Quaternion tempQuatL;



    private void OnGUI()
    {
        GUI.Label(new Rect(Screen.width - 100, Screen.height - 100, 100, 30), System.Convert.ToString(Current_Speed));
        GUI.Label(new Rect(Screen.width - 100, Screen.height - 110, 100, 30), System.Convert.ToString(NitroTank));
        GUI.Label(new Rect(Screen.width - 100, Screen.height - 120, 100, 30), System.Convert.ToString(boost_build));
    }

    // Use this for initialization
    void Start () {
        startPos = transform.position;
        bike = GameObject.Find("Main");
        bikeBody = bike.GetComponent<Rigidbody>();
        bikeModel = GameObject.Find("bike");
        Turn = Input.GetAxis("LS Horizontal");
        Max_Boost = Max_speed * 2f;
        Decceleration = Acceleration * 6f;
        Max_speed_holder = Max_speed;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Acceleration = 3750;
        if (!air)
        {
            Gas_Pedal = Input.GetAxis("Right Trigger Axis");

            if (drifting && drift_direction == 1)
            {
                Turn = Input.GetAxis("LS Horizontal") + 0.6f;
                Turn /= 1.6f;
                tempQuatR = new Quaternion(rightRotate50.x, rightRotate50.y, rightRotate50.z * Turn, rightRotate50.w);
                bikeModel.transform.localRotation = Quaternion.RotateTowards(bikeModel.transform.localRotation, tempQuatR, 3f);
            }

            if (drifting && drift_direction == -1)
            {
                Turn = Input.GetAxis("LS Horizontal") - 0.6f;
                Turn /= 1.6f;
                tempQuatL = new Quaternion(leftRotate50.x, leftRotate50.y, leftRotate50.z * -Turn, leftRotate50.w);
                bikeModel.transform.localRotation = Quaternion.RotateTowards(bikeModel.transform.localRotation, tempQuatL, 3f);
            }

            if (!drifting)
            {
                Turn = Input.GetAxis("LS Horizontal");
            }

            Max_Turn = 1.2f;
        }

        if (air)
        {
            Gas_Pedal = -1;
            Turn = 0;
        }

        if (Gas_Pedal > 0)
        {
            Current_Speed += Acceleration * Time.fixedDeltaTime;
            if(Current_Speed - (Decceleration * Time.fixedDeltaTime) > Max_speed && boosting == false && Nitro_Boost == false)
            {
                Current_Speed -= Decceleration * Time.fixedDeltaTime;
            }
            else if (Current_Speed > Max_speed)
            {
                Current_Speed = Max_speed;
            }
        }

        if (Gas_Pedal <= 0)
        {
            Current_Speed -= (Acceleration/4) * Time.fixedDeltaTime;
            if (Current_Speed < 0)
            {
                Current_Speed = 0;
            }
        }

        if (Turn != 0)
        {
            if (Input.GetButton("RB"))
            {
                if(Turn > 0 && !drifting)
                {
                    drifting = true;
                    drift_direction = 1;
                }
                if (Turn < 0 && !drifting)
                {
                    drifting = true;
                    drift_direction = -1;
                }
                Max_Turn = 2.25f;
            }
            if (drifting)
            {
                boost_build += Mathf.Abs((Turn * boost_coef * Current_Speed)/Max_speed_holder);
            }

            Turning.y = Turn * Max_Turn;
            transform.Rotate(Turning, Space.Self);

            if (!drifting)
            {
                if (Turn > 0)
                {
                    bikeModel.transform.localRotation = Quaternion.RotateTowards(bikeModel.transform.localRotation, rightRotate30, 1.5f);
                }
                if (Turn < 0)
                {
                    bikeModel.transform.localRotation = Quaternion.RotateTowards(bikeModel.transform.localRotation, leftRotate30, 1.5f);
                }
            }
        }
        else
        {
            bikeModel.transform.localRotation = Quaternion.RotateTowards(bikeModel.transform.localRotation, Quaternion.identity, 1f);
        }

        if (!Input.GetButton("RB"))
        {
            if (boost_build >= boost_hit)
            {
                Boost();
            }
            boost_build = 0;
            drifting = false;
        }

        else if (!drifting)
        {
            if (Input.GetButton("RB"))
            {
                Current_Speed -= Acceleration * Time.fixedDeltaTime;
            }
        }

        if (boost_time + 0.75 <= Time.fixedTime && boosting)
        {
            Max_speed /= 1.5f;
            boosting = false;
        }


        if (Input.GetButton("LB") && drifting == false && NitroTank > 20)
        {
            Nitro();
        }

        else if (Input.GetButton("LB") && Nitro_Boost == true && boosting == false && drifting == false && NitroTank > 0)
        {
            Nitro();
        }

        else
        {
            NitroTank += 0.05f;
            if (NitroTank > MaxNitro)
            {
                NitroTank = MaxNitro;
            }
            Nitro_Boost = false;
        }


        if (Input.GetButtonUp("LB") && boosting == false && Max_speed == Max_Boost)
        {
            Max_speed /= 2f;
        }

        else if (Max_speed == Max_Boost && boosting == false && Nitro_Boost == false)
        {
            Max_speed /= 2f;
        }

        Move.z = Current_Speed * Time.fixedDeltaTime;
        bikeBody.position -= transform.forward * Move.z;
        
        //RESET
        if(Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    void Boost()
    {
        Max_speed = Max_speed * 1.5f;
        Current_Speed = Max_speed;
        boost_time = Time.fixedTime;
        boosting = true;
    }

    void Nitro()
    {
        Max_speed = Max_Boost;
        if (Current_Speed >= Max_speed)
        {
            Current_Speed = Max_speed;
        }
        else
        {
            Acceleration *= 3f;
        }
        Nitro_Boost = true;
        NitroTank -= 0.25f;
    }

    private void Reset()
    {
        Max_speed = 8750;
        Acceleration = 1875;
        Max_Turn = 1.2f;
        Current_Speed = 0;
        drift_direction = 1;
        drifting = false;
        boost_build = 0;
        boost_hit = 100;
        boost_coef = 2f;
        boost_time = -2;
        boosting = false;
        air = false;
        NitroTank = 0f;
        MaxNitro = 100;
        Nitro_Boost = false;
        transform.position = startPos;
    }

}
