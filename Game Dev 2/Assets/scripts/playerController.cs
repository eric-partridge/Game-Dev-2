using System;
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
    public GameObject pitchGO;
    public LayerMask ignoreLayer;
    public float normalGravity;
    public float airGravity;
    public float boostChange;
    public float hitChange;
    public bool grounded;
    public Vector3 rayNormal;
    public int playerNum;
    public GameObject otherPlayer;
    public GameObject otherPlayer2;
    public GameObject otherPlayer3;
    public GameObject Master;
    public AudioSource BGM;
    public AudioSource Sampler;
    public RaceManager raceManager;
    public ParticleSystem boostParticles;

    private bool drifting = false;
    private Resample Resampler;
    Rigidbody rb;
    private float defaultSensitivity;
    private float change = 0f;
    private float drift_boost;
    private int drift_direction;
    private bool boosting = false;
    private bool hitByEnemy = false;
    private float boostTime = -2f;
    private float hitTime = -2f;
    private string R2Button;
    private string L2Button;
    private string L1Button;
    private string Horizontal;
    private string RSX;
    private string RSY;
    private float waitTime = 0f;
    private bool playerCheckpointPassed = false;
    private float playerCheckpointTime;
    private bool resetStickX = true;
    private bool resetStickY = true;
    private string respawnCheckpoint;

    Quaternion leftRotate30 = Quaternion.AngleAxis(-30, Vector3.forward);
    Quaternion rightRotate30 = Quaternion.AngleAxis(30, Vector3.forward);
    Quaternion leftRotate50 = Quaternion.AngleAxis(-50, Vector3.forward);
    Quaternion rightRotate50 = Quaternion.AngleAxis(50, Vector3.forward);
    Quaternion upRotate10 = Quaternion.AngleAxis(-50, Vector3.forward);
    Quaternion tempQuatR;
    Quaternion tempQuatL;

    // Use this for initialization
    void Start () {
        //boostParticles = GameObject.Find("BoostParticle").GetComponent<ParticleSystem>();
        Resampler = GameObject.Find("Master").GetComponent<Resample>();
        rb = this.GetComponent<Rigidbody>();
        defaultSensitivity = sensitivity;
        R2Button = "R2 P" + playerNum.ToString();
        L2Button = "L2 P" + playerNum.ToString();
        L1Button = "L1 P" + playerNum.ToString();
        Horizontal = "Horizontal P" + playerNum.ToString();
        RSX = "RSX P" + playerNum.ToString();
        RSY = "RSY P" + playerNum.ToString();
        if(PlayerPrefs.GetInt("num_p") == 2)
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), otherPlayer.GetComponent<Collider>());
        }
        else if(PlayerPrefs.GetInt("num_p") == 4)
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), otherPlayer2.GetComponent<Collider>());
            Physics.IgnoreCollision(this.GetComponent<Collider>(), otherPlayer3.GetComponent<Collider>());
        }
    }

    void Update()
    {
    }

    // Update is called once per frame
    void FixedUpdate () {

        // print(Input.GetAxis("RSX"));
        //print(Input.GetAxis("RSY"));
        float leftStickX = Input.GetAxis(Horizontal);
        //print("Horizontal is: " + Horizontal);
        bool gas = Input.GetButton(R2Button);
        bool brake = (Input.GetButton(L2Button) || Input.GetButton(L1Button));
        Vector3 force = new Vector3(0,0,0);

        //print("is brake: " + brake);

        if (isGrounded())
        {
            if (gas)
            {
                rb.AddForce(transform.forward * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            }
            if (!gas)
            {
                rb.velocity = rb.velocity * deacceleration;
            }
            if (brake)
            {
                //if turning and braking ie. drifitng increase turning senesitivity
                if (leftStickX != 0)
                {
                    if (leftStickX > 0 && drift_direction == 0)
                    {
                        drift_direction = 1;
                    }
                    else if(leftStickX < 0 && drift_direction == 0)
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

            else
            {
                drift_direction = 0;
            }

            if (drifting && drift_direction == 1)
            {
                leftStickX = leftStickX + 0.6f;
                transform.Rotate(0, leftStickX / 1.6f * sensitivity, 0);
                tempQuatR = new Quaternion(leftRotate50.x, leftRotate50.y, leftRotate50.z * leftStickX / 1.6f, leftRotate50.w);
                shipModel.transform.localRotation = Quaternion.RotateTowards(shipModel.transform.localRotation, tempQuatR, 3f);
                print("Drifting RIGHT");
            }
            else if (drifting && drift_direction == -1)
            {
                leftStickX = leftStickX - 0.6f;
                transform.Rotate(0, leftStickX / 1.6f * sensitivity, 0);
                tempQuatL = new Quaternion(rightRotate50.x, rightRotate50.y, rightRotate50.z * -leftStickX / 1.6f, rightRotate50.w);
                shipModel.transform.localRotation = Quaternion.RotateTowards(shipModel.transform.localRotation, tempQuatL, 3f);
                print("Drifting LEFT");
            }

            else if (leftStickX != 0 && drift_direction == 0)
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
        else
        {
            transform.Rotate(0, leftStickX * sensitivity/1.5f, 0);
            if (leftStickX > 0)
            {
                shipModel.transform.localRotation = Quaternion.RotateTowards(shipModel.transform.localRotation, leftRotate30, 1.5f);
            }
            if (leftStickX < 0)
            {
                shipModel.transform.localRotation = Quaternion.RotateTowards(shipModel.transform.localRotation, rightRotate30, 1.5f);
            }
        }
        float rotDegrees = 180f;
        Vector3 newVelocity = Vector3.RotateTowards(rb.velocity, transform.forward, rotDegrees * Time.deltaTime * Mathf.Deg2Rad, 0);
        rb.velocity = newVelocity;

        if((boostTime + 0.75f) <= Time.fixedTime && boosting){
            maxSpeed /= boostChange;
            boosting = false;
            boostParticles.Stop();
            
            //print("Not boosting");
            //camReference.GetComponent<cameraScript>().rotateCamera(-5f);
        }

        if((hitTime + 1.5f) <= Time.fixedTime && hitByEnemy)
        {
            maxSpeed /= hitChange;
            hitByEnemy = false;
            //print("Not slowed");
        }

        if(playerCheckpointTime + 3f <= Time.fixedTime)
        {
            playerCheckpointPassed = false;
        }
    }

    public bool isGrounded()
    {
        RaycastHit hit;
        bool ret = Physics.Raycast(this.transform.position, -Vector3.up, out hit, 3f, ~(1 << 9));
        if (ret)
        {
            Debug.DrawRay(this.transform.position, Vector3.down * hit.distance, Color.green);
            Physics.gravity = new Vector3(0, normalGravity, 0); //new Vector3(-hit.normal.x, normalGravity * hit.normal.y, -hit.normal.z);
            //print("Normal: " + hit.normal);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal)), 0.5f);

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
            rb.AddForce(new Vector3(0f, airGravity, -0f), ForceMode.Acceleration);
            
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
            //print("Test out: " + Input.GetAxis(RSX));
            // UP
            if(other.GetComponent<Boost_Pad>().GetType2() == "UP" && other.GetComponent<Boost_Pad>().GetDirection() == 1)
            {
                if(Input.GetAxis(RSY) < -0.3 && BPM_Clock.trigger && resetStickY)
                {
                    rb.AddForce(transform.forward * 1.5f * speed, ForceMode.VelocityChange);
                    boost();
                    boostParticles.Play();
                    waitTime = Resampler.ResampleLoop();
                }
                else if(Input.GetAxis(RSY) < -0.3f && !BPM_Clock.trigger && resetStickY)
                {
                    rb.velocity = rb.velocity * (deacceleration - (1 - deacceleration));
                    gameObject.GetComponent<CheckPoint>().score -= 25;
                }
            }
            // DOWN
            if (other.GetComponent<Boost_Pad>().GetType2() == "UP" && other.GetComponent<Boost_Pad>().GetDirection() == -1)
            {
                if (Input.GetAxis(RSY) > 0.3)
                {
                    rb.AddForce(-transform.forward * 2 * speed, ForceMode.VelocityChange);
                }
            }
            // LEFT
            if (other.GetComponent<Boost_Pad>().GetType2() == "RIGHT" && other.GetComponent<Boost_Pad>().GetDirection() == -1)
            {
                if (Input.GetAxis(RSX) < -0.3 && resetStickX)
                {
                    rb.velocity = new Vector3(0, 0, 0);
                    rb.AddForce(-other.transform.right * 1.5f * speed, ForceMode.VelocityChange);
                    //waitTime = Resampler.ResampleLoop();
                    //rb.velocity = rb.velocity - Vector3.Project(rb.velocity,  transform.forward);
                }
            }
            // RIGHT
            if (other.GetComponent<Boost_Pad>().GetType2() == "RIGHT" && other.GetComponent<Boost_Pad>().GetDirection() == 1)
            {
                if (Input.GetAxis(RSX) > 0.3 && resetStickX)
                {
                    rb.velocity = new Vector3(0, 0, 0);
                    rb.AddForce(other.transform.right * 1.5f * speed, ForceMode.VelocityChange);
                    //waitTime = Resampler.ResampleLoop();
                }
            }
            //makes you have to actually flick the stick (cant hold it)
            if (Input.GetAxis(RSX) > 0.3f || Input.GetAxis(RSX) < -0.3f)
            {
                resetStickX = false;
            }
            else if (Input.GetAxis(RSX) < 0.3f && Input.GetAxis(RSX) > -0.3f)
            {
                resetStickX = true;
            }
            if (Input.GetAxis(RSY) > 0.3f || Input.GetAxis(RSY) < -0.3f)
            {
                resetStickY = false;
            }
            else if (Input.GetAxis(RSY) < 0.3f && Input.GetAxis(RSY) > -0.3f)
            {
                resetStickY = true;
            }
        }
        if(other.tag == "Jump" )
        {
            if (boosting)
            {
                maxSpeed /= boostChange;
                boosting = false;
                boostParticles.Stop();
            }
        }
    }

    void boost()
    {
        if (!boosting){
            maxSpeed = maxSpeed * boostChange;
            boostTime = Time.fixedTime;
            boosting = true;
            boostParticles.Play();
        }        
    }

    void slowDown()
    {
        if (!hitByEnemy)
        {
            maxSpeed = maxSpeed * hitChange;
            rb.velocity = new Vector3(0, 0, 0);
            hitTime = Time.fixedTime;
            hitByEnemy = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Missile")
        {
            print("Player: " + this.tag + " Hit by: " + collision.gameObject.tag);
            Destroy(collision.gameObject);
            slowDown();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "CheckPoint")
        {
            //if player 1 goes through a checkpoint
            if(playerNum == 1 && !playerCheckpointPassed && respawnCheckpoint != other.name) {
                raceManager.updatePlayer1Checkpoint();
                playerCheckpointPassed = true;
                playerCheckpointTime = Time.fixedTime;
                respawnCheckpoint = "";
            }
            //if player 2 goes through a checkpoint
            else if(playerNum == 2 && !playerCheckpointPassed && respawnCheckpoint != other.name) {
                raceManager.updatePlayer2Checkpoint();
                playerCheckpointPassed = true;
                playerCheckpointTime = Time.fixedTime;
                respawnCheckpoint = "";
            }
            //if player 3 goes through a checkpoint
            else if (playerNum == 3 && !playerCheckpointPassed && respawnCheckpoint != other.name)
            {
                raceManager.updatePlayer3Checkpoint();
                playerCheckpointPassed = true;
                playerCheckpointTime = Time.fixedTime;
                respawnCheckpoint = "";
            }
            //if player 4 goes through a checkpoint
            else if (playerNum == 4 && !playerCheckpointPassed && respawnCheckpoint != other.name)
            {
                raceManager.updatePlayer4Checkpoint();
                playerCheckpointPassed = true;
                playerCheckpointTime = Time.fixedTime;
                respawnCheckpoint = "";
            }
        }
        //if its the line
        else if(other.tag == "Line" && other is BoxCollider)
        {
            if(raceManager.getFirstPlace().name != this.name)
            {
                respawnPlayer();
            }
            else
            {
                raceManager.adjustLineSpeed();
            }
        }
        if (other.tag == "DeadZone")
        {
            respawnPlayer();
            print("Deadzone respawn");
        }
        if (other.tag == "Jump")
        {
            if (boosting)
            {
                print("wtf");
                maxSpeed /= boostChange;
                boosting = false;
                boostParticles.Stop();
                rb.AddForce(new Vector3(0f, airGravity, -0f), ForceMode.VelocityChange);
            }
        }
    }

    public void respawnPlayer()
    {
        //if multiplayer spawn player at position of whoever is in first place
        if (PlayerPrefs.GetInt("num_p") != 1)
        {
            this.gameObject.transform.position = raceManager.getFirstPlace().transform.position;
            this.gameObject.transform.rotation = raceManager.getFirstPlace().transform.rotation;
            this.gameObject.SetActive(true);
        }
        //else spawn them at the most recent checkpoint they passed
        else
        {
            this.gameObject.transform.position = raceManager.checkPoints[raceManager.getCheckpointNum(playerNum)-1].GetComponent<Renderer>().bounds.center;
            respawnCheckpoint = raceManager.checkPoints[raceManager.getCheckpointNum(playerNum) - 1].name;
            this.gameObject.transform.rotation = raceManager.checkPoints[raceManager.getCheckpointNum(playerNum)].rotation;
            this.gameObject.transform.Rotate(0, 90, 0);
            this.gameObject.SetActive(true);
        }
        slowDown();
        StartCoroutine(Immune(1f));
    }

    private IEnumerator Immune(float waitTime)
    {
        Physics.IgnoreLayerCollision(12, 12, true);
        yield return new WaitForSeconds(waitTime);
        Physics.IgnoreLayerCollision(12, 12, false);
    }

}
