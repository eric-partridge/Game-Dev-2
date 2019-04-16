﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RaceManager : MonoBehaviour
{

    public Transform[] checkPoints;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public Camera camera1;
    public Camera camera2;
    public Camera camera3;
    public Camera camera4;
    public GameObject line;
    public GameObject[] ships;

    private GameObject firstPlace;
    private int player1Checkpoint = 0;
    private int player2Checkpoint = 0;
    private int player3Checkpoint = 0;
    private int player4Checkpoint = 0;
    private int player1Lap = 0;
    private int player2Lap = 0;
    private int player3Lap = 0;
    private int player4Lap = 0;
    private float player1Distance = 0;
    private float player2Distance = 0;
    private float player3Distance = 0;
    private float player4Distance = 0;
    private cameraScript cam1Script;
    private cameraScript cam2Script;
    private cameraScript cam3Script;
    private cameraScript cam4Script;
    private playerController player1Script;
    private playerController player2Script;
    private playerController player3Script;
    private playerController player4Script;
    private weaponScript player1WeaponScript;
    private weaponScript player2WeaponScript;
    private weaponScript player3WeaponScript;
    private weaponScript player4WeaponScript;
    private float lineStopTime = 5f;
    private navMeshController navMeshScript;

    public PositionUI pos_ui;

    // Start is called before the first frame update
    void Awake()
    {

        Countdown.ships = new List<GameObject>();
        navMeshScript = line.GetComponent<navMeshController>();

        if (PlayerPrefs.GetInt("num_p") == 1)
        {
            //disables other cameras
            camera2.enabled = false;
            camera3.enabled = false;
            camera4.enabled = false;
            camera1.rect = new Rect(0, 0, 1, 1);

            //instantiates player1
            player1 = GameObject.Instantiate(ships[PlayerPrefs.GetInt("p0")]);
            player1.transform.position = new Vector3(0, 17, -20.5f);
            player1Script = player1.GetComponent<playerController>();
            player1Script.playerNum = 1;
            player1Script.raceManager = this;

            player1Script.otherPlayer = null;
            player1Script.otherPlayer2 = null;
            player1Script.otherPlayer3 = null;

            //sets up camera
            cam1Script = camera1.GetComponent<cameraScript>();
            cam1Script.player = player1.transform;

            //sets up weapon script
            player1WeaponScript = player1.GetComponent<weaponScript>();
            player1WeaponScript.playerNum = 1;
            player1WeaponScript.currEnergy = 0;

            line.SetActive(false);
            Countdown.ships.Add(player1);
        }
        else if (PlayerPrefs.GetInt("num_p") == 2)
        {
            //enables 2 cameras
            camera1.enabled = true;
            camera2.enabled = true;
            camera3.enabled = false;
            camera4.enabled = false;
            camera1.rect = new Rect(0, .5f, 1, .5f);
            camera2.rect = new Rect(0f, 0, 1, .5f);

            //instantiates player 1
            player1 = GameObject.Instantiate(ships[PlayerPrefs.GetInt("p0")]);
            player1.transform.position = new Vector3(8.5f, 17, -20.5f);
            player1Script = player1.GetComponent<playerController>();
            player1Script.playerNum = 1;
            player1Script.raceManager = this;

            //instantiates player2
            player2 = GameObject.Instantiate(ships[PlayerPrefs.GetInt("p1")]);
            player2.transform.position = new Vector3(-8.5f, 17, -20.5f);
            player2Script = player2.GetComponent<playerController>();
            player2Script.playerNum = 2;
            player2Script.raceManager = this;

            player1Script.otherPlayer = player2;
            player1Script.otherPlayer2 = null;
            player1Script.otherPlayer3 = null;
            player2Script.otherPlayer = player1;
            player2Script.otherPlayer2 = null;
            player2Script.otherPlayer3 = null;

            //sets up cameras
            cam1Script = camera1.GetComponent<cameraScript>();
            cam1Script.player = player1.transform;
            cam2Script = camera2.GetComponent<cameraScript>();
            cam2Script.player = player2.transform;

            //player 1 weapon script
            player1WeaponScript = player1.GetComponent<weaponScript>();
            player1WeaponScript.playerNum = 1;
            player1WeaponScript.currEnergy = 0;

            //player 2 weapon script
            player2WeaponScript = player2.GetComponent<weaponScript>();
            player2WeaponScript.playerNum = 2;
            player2WeaponScript.currEnergy = 0;

            Countdown.ships.Add(player1);
            Countdown.ships.Add(player2);
        }
        else if (PlayerPrefs.GetInt("num_p") == 4)
        {
            //enables 2 cameras
            camera1.enabled = true;
            camera2.enabled = true;
            camera3.enabled = true;
            camera4.enabled = true;
            camera1.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
            camera2.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            camera3.rect = new Rect(0f, 0, 0.5f, 0.5f);
            camera4.rect = new Rect(0.5f, 0, 1, 0.5f);

            //instantiates player 1
            player1 = GameObject.Instantiate(ships[PlayerPrefs.GetInt("p0")]);
            player1.transform.position = new Vector3(11, 17, -20.5f);
            player1Script = player1.GetComponent<playerController>();
            player1Script.playerNum = 1;

            //instantiates player2
            player2 = GameObject.Instantiate(ships[PlayerPrefs.GetInt("p1")]);
            player2.transform.position = new Vector3(3.5f, 17, -20.5f);
            player2Script = player2.GetComponent<playerController>();
            player2Script.playerNum = 2;

            //instantiates player3
            player3 = GameObject.Instantiate(ships[PlayerPrefs.GetInt("p2")]);
            player3.transform.position = new Vector3(-3.5f, 17, -20.5f);
            player3Script = player3.GetComponent<playerController>();
            player3Script.playerNum = 3;

            //instantiates player4
            player4 = GameObject.Instantiate(ships[PlayerPrefs.GetInt("p3")]);
            player4.transform.position = new Vector3(-11, 17, -20.5f);
            player4Script = player2.GetComponent<playerController>();
            player4Script.playerNum = 4;

            player1Script.otherPlayer = player2;
            player1Script.otherPlayer2 = player3;
            player1Script.otherPlayer3 = player4;

            player2Script.otherPlayer = player1;
            player2Script.otherPlayer2 = player3;
            player2Script.otherPlayer3 = player4;

            player3Script.otherPlayer = player1;
            player3Script.otherPlayer2 = player2;
            player3Script.otherPlayer3 = player4;

            player4Script.otherPlayer = player1;
            player4Script.otherPlayer2 = player2;
            player4Script.otherPlayer3 = player3;

            //sets up cameras
            cam1Script = camera1.GetComponent<cameraScript>();
            cam1Script.player = player1.transform;
            cam2Script = camera2.GetComponent<cameraScript>();
            cam2Script.player = player2.transform;
            cam3Script = camera3.GetComponent<cameraScript>();
            cam3Script.player = player3.transform;
            cam4Script = camera4.GetComponent<cameraScript>();
            cam4Script.player = player4.transform;

            //player 1 weapon script
            player1WeaponScript = player1.GetComponent<weaponScript>();
            player1WeaponScript.playerNum = 1;
            player1WeaponScript.currEnergy = 0;

            //player 2 weapon script
            player2WeaponScript = player2.GetComponent<weaponScript>();
            player2WeaponScript.playerNum = 2;
            player2WeaponScript.currEnergy = 0;

            //player 3 weapon script
            player3WeaponScript = player3.GetComponent<weaponScript>();
            player3WeaponScript.playerNum = 3;
            player3WeaponScript.currEnergy = 0;

            //player 4 weapon script
            player4WeaponScript = player4.GetComponent<weaponScript>();
            player4WeaponScript.playerNum = 4;
            player4WeaponScript.currEnergy = 0;

            Countdown.ships.Add(player1);
            Countdown.ships.Add(player2);
            Countdown.ships.Add(player3);
            Countdown.ships.Add(player4);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        print("Updating");
        if (PlayerPrefs.GetInt("num_p") != 1)
        {
            player1Distance = Vector3.Distance(player1.transform.position, checkPoints[player1Checkpoint].position);
            player2Distance = Vector3.Distance(player2.transform.position, checkPoints[player2Checkpoint].position);

            //determines whose in first place by lap number > checkpoint number > distance to next checkpoint
            if (player1Lap > player2Lap) { firstPlace = player1; }
            else if (player2Lap > player1Lap) { firstPlace = player2; }
            else
            {
                if (player1Checkpoint > player2Checkpoint) { firstPlace = player1; }
                else if (player2Checkpoint > player1Checkpoint) { firstPlace = player2; }
                else
                {
                    if (player1Distance < player2Distance) { firstPlace = player1; }
                    else { firstPlace = player2; }
                }
            }
            print("first: " + firstPlace.name);

            if (Countdown.start && Time.fixedTime > lineStopTime + 2f)
            {
                print("Resetting speed");
                line.SetActive(true);
                line.GetComponent<NavMeshAgent>().speed = firstPlace.GetComponent<playerController>().maxSpeed * 1.1f;
                line.GetComponent<NavMeshAgent>().angularSpeed = line.GetComponent<NavMeshAgent>().speed * 1.5f;
                line.GetComponent<NavMeshAgent>().acceleration = line.GetComponent<NavMeshAgent>().speed * 1.5f;
                navMeshScript.agent.SetDestination(navMeshScript.targets[navMeshScript.i].position);
            }
        }

        //get and display scores
        int[] scores = null;
        List<KeyValuePair<double, int>> temp = new List<KeyValuePair<double, int>>();
        if(PlayerPrefs.GetInt("num_p") == 1) {
            scores = new int[1];
            scores[0] = PlayerPrefs.GetInt("p0");
        }else if(PlayerPrefs.GetInt("num_p") == 2) {
            scores = new int[2];
            temp.Add(new KeyValuePair<double, int>(player1.GetComponent<CheckPoint>().GetScore(), PlayerPrefs.GetInt("p0")));
            temp.Add(new KeyValuePair<double, int>(player2.GetComponent<CheckPoint>().GetScore(), PlayerPrefs.GetInt("p1")));
            temp.Sort();
            for(int i = 0; i < 2; i++) {
                scores[i] = temp[i].Value;
            }
        } else if(PlayerPrefs.GetInt("num_p") == 4) {
            scores = new int[4];
            temp.Add(new KeyValuePair<double, int>(player1.GetComponent<CheckPoint>().GetScore(), PlayerPrefs.GetInt("p0")));
            temp.Add(new KeyValuePair<double, int>(player2.GetComponent<CheckPoint>().GetScore(), PlayerPrefs.GetInt("p1")));
            temp.Add(new KeyValuePair<double, int>(player3.GetComponent<CheckPoint>().GetScore(), PlayerPrefs.GetInt("p2")));
            temp.Add(new KeyValuePair<double, int>(player4.GetComponent<CheckPoint>().GetScore(), PlayerPrefs.GetInt("p3")));
            temp.Sort();
            for (int i = 0; i < 4; i++) {
                scores[i] = temp[i].Value;
            }
        }
        pos_ui.UpdateUI(scores);
    }

    public void updatePlayer1Checkpoint() {
        if(player1Checkpoint == 7)
        {
            player1Checkpoint = 1;
            player1Lap++;
        }
        else
        {
            player1Checkpoint++;
        }
    }

    public void updatePlayer2Checkpoint() {
        if (player2Checkpoint == 7)
        {
            player2Checkpoint = 1;
            player2Lap++;
        }
        else
        {
            player2Checkpoint++;
        }
    }

    public void updatePlayer3Checkpoint()
    {
        if (player3Checkpoint == 7)
        {
            player3Checkpoint = 1;
            player3Lap++;
        }
        else
        {
            player3Checkpoint++;
        }
    }

    public void updatePlayer4Checkpoint()
    {
        if (player4Checkpoint == 7)
        {
            player4Checkpoint = 1;
            player4Lap++;
        }
        else
        {
            player4Checkpoint++;
        }
    }

    public int getCheckpointNum(int p)
    {
        if(p == 1) { return player1Checkpoint; }
        else if(p == 2) { return player2Checkpoint; }
        else if(p == 3) { return player3Checkpoint; }
        else if(p == 4) { return player4Checkpoint; }
        else { return -1; }
    }

    public GameObject getFirstPlace() { return firstPlace; }

    public void adjustLineSpeed()
    {
        line.SetActive(false);
        line.GetComponent<NavMeshAgent>().speed = 0;
        line.GetComponent<NavMeshAgent>().angularSpeed = 0;
        line.GetComponent<NavMeshAgent>().acceleration = 0;
        lineStopTime = Time.fixedTime;
    }
}
