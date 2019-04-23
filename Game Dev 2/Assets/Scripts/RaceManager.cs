using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    public int laps = 2;
    public GameObject endCam;
    public GameObject endCanv;
    public Text warningTextP1;
    public Text warningTextP2;
    public Text warningTextP3;
    public Text warningTextP4;
    public Image warningBoxP1;
    public Image warningBoxP2;
    public Image warningBoxP3;
    public Image warningBoxP4;

    private GameObject firstPlace;
    private int player1Checkpoint = 0;
    private int player2Checkpoint = 0;
    private int player3Checkpoint = 0;
    private int player4Checkpoint = 0;
    private int player1Lap = 1;
    private int player2Lap = 1;
    private int player3Lap = 1;
    private int player4Lap = 1;
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
    private float player1WarningTime = 0;
    private float player2WarningTime = 0;
    private float player3WarningTime = 0;
    private float player4WarningTime = 0;
    private float lineStopTime = 5f;
    private navMeshController navMeshScript;
    private bool warning1On = false;
    private bool warning2On = false;
    private bool warning3On = false;
    private bool warning4On = false;

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
            camera1.GetComponent<PlayerUI>().player = player1;

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
            camera1.GetComponent<PlayerUI>().player = player1;
            cam2Script = camera2.GetComponent<cameraScript>();
            cam2Script.player = player2.transform;
            camera2.GetComponent<PlayerUI>().player = player2;

            //player 1 weapon script
            player1WeaponScript = player1.GetComponent<weaponScript>();
            player1WeaponScript.playerNum = 1;
            player1WeaponScript.currEnergy = 0;

            //player 2 weapon script
            player2WeaponScript = player2.GetComponent<weaponScript>();
            player2WeaponScript.playerNum = 2;
            player2WeaponScript.currEnergy = 0;

            line.SetActive(false);
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
            player4Script = player4.GetComponent<playerController>();
            player4Script.playerNum = 4;

            //sets up other players to avoid sphere colliders
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
            camera1.GetComponent<PlayerUI>().player = player1;
            cam2Script = camera2.GetComponent<cameraScript>();
            cam2Script.player = player2.transform;
            camera2.GetComponent<PlayerUI>().player = player2;
            cam3Script = camera3.GetComponent<cameraScript>();
            cam3Script.player = player3.transform;
            camera3.GetComponent<PlayerUI>().player = player3;
            cam4Script = camera4.GetComponent<cameraScript>();
            cam4Script.player = player4.transform;
            camera4.GetComponent<PlayerUI>().player = player4;

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
        //if more than one player get distance to next checkpoint
        if (PlayerPrefs.GetInt("num_p") == 1)
        {
            warningBoxP1.enabled = false;
            warningTextP1.text = "";
        }
        else if (PlayerPrefs.GetInt("num_p") != 1)
        {
            if (player1Checkpoint == checkPoints.Length)
            {
                player1Distance = Vector3.Distance(player1.transform.position, checkPoints[0].position);
            }
            else
            {
                player1Distance = Vector3.Distance(player1.transform.position, checkPoints[player1Checkpoint].position);
            }
                
            if (player2Checkpoint == checkPoints.Length)
            {
                player2Distance = Vector3.Distance(player2.transform.position, checkPoints[0].position);
            }
            else { 
                player2Distance = Vector3.Distance(player2.transform.position, checkPoints[player2Checkpoint].position);
            }
            if(PlayerPrefs.GetInt("num_p") == 4)
            {
                if (player3Checkpoint == checkPoints.Length)
                {
                    player3Distance = Vector3.Distance(player3.transform.position, checkPoints[0].position);
                }
                else
                {
                    player3Distance = Vector3.Distance(player3.transform.position, checkPoints[player3Checkpoint].position);
                }
                if (player4Checkpoint == checkPoints.Length)
                {
                    player4Distance = Vector3.Distance(player4.transform.position, checkPoints[0].position);
                }
                else
                {
                    player4Distance = Vector3.Distance(player4.transform.position, checkPoints[player4Checkpoint].position);
                }
            }

            if (PlayerPrefs.GetInt("num_p") == 2)
            {
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
            }
            //checks first place for 4 player
            else
            {
                if(player1Lap > player2Lap && player1Lap > player3Lap && player1Lap > player4Lap) { firstPlace = player1; }
                else if(player2Lap > player1Lap && player2Lap > player3Lap && player2Lap > player4Lap) { firstPlace = player2; }
                else if(player3Lap > player1Lap && player3Lap > player2Lap && player3Lap > player4Lap) { firstPlace = player3; }
                else if (player4Lap > player1Lap && player4Lap > player2Lap && player4Lap > player3Lap) { firstPlace = player4; }
                else
                {
                    if(player1Checkpoint > player2Checkpoint && player1Checkpoint > player3Checkpoint && player1Checkpoint > player4Checkpoint) { firstPlace = player1; }
                    else if(player2Checkpoint > player1Checkpoint && player2Checkpoint > player3Checkpoint && player2Checkpoint > player4Checkpoint) { firstPlace = player2; }
                    else if(player3Checkpoint > player1Checkpoint && player3Checkpoint > player2Checkpoint && player3Checkpoint > player4Checkpoint) { firstPlace = player3; }
                    else if(player4Checkpoint > player1Checkpoint && player4Checkpoint > player2Checkpoint && player4Checkpoint > player3Checkpoint) { firstPlace = player4; }
                    else
                    {
                        if(player1Distance > player2Distance && player1Distance > player3Distance && player1Distance > player4Distance) { firstPlace = player1; }
                        else if(player2Distance > player1Distance && player2Distance > player3Distance && player2Distance > player4Distance) { firstPlace = player2; }
                        else if(player3Distance > player1Distance && player3Distance > player2Distance && player3Distance > player4Distance) { firstPlace = player3; }
                        else { firstPlace = player4; }
                    }
                }

                print("4 player first place: " + firstPlace);
            }

            //respawning for 2 player
            if(PlayerPrefs.GetInt("num_p") == 2)
            {
                //checks if player 1 is about to be respawned
                if (player2 == firstPlace)
                {
                    if (player2Lap > player1Lap)
                    {
                        if (player2Checkpoint + 7 >= player1Checkpoint + 1 && !warning1On)
                        {
                            player1WarningTime = Time.fixedTime;
                            warning1On = true;
                        }
                    }
                    else if (player2Checkpoint >= player1Checkpoint + 1 && !warning1On)
                    {
                        player1WarningTime = Time.fixedTime;
                        warning1On = true;
                    }
                }
                //if player 1 reached time limit respawn
                if (Time.fixedTime >= (player1WarningTime + 4f) && warning1On)
                {
                    player1Script.respawnPlayer();
                    player1WeaponScript.currEnergy = 0;
                    player1Checkpoint = player2Checkpoint;
                    player1Lap = player2Lap;
                    warning1On = false;
                }

                //checks if player2 is about to be respawned
                if (player1 == firstPlace)
                {
                    if (player1Lap > player2Lap)
                    {
                        if (player1Checkpoint + 7 >= player2Checkpoint + 1 && !warning2On)
                        {
                            player2WarningTime = Time.fixedTime;
                            warning2On = true;
                        }
                    }
                    else if (player1Checkpoint >= player2Checkpoint + 1 && !warning2On)
                    {
                        player2WarningTime = Time.fixedTime;
                        warning2On = true;
                    }
                }
                //if player2 reached time limit respawn
                if (Time.fixedTime >= (player2WarningTime + 4f) && warning2On)
                {
                    player2Script.respawnPlayer();
                    player2Checkpoint = player1Checkpoint;
                    player2WeaponScript.currEnergy = 0;
                    player2Lap = player1Lap;
                    warning2On = false;
                }

                //bug handling
                if (player1Checkpoint == player2Checkpoint)
                {
                    warning1On = false;
                    warning2On = false;
                }

                //printing warning on UI
                if (warning1On)
                {
                    warningBoxP1.enabled = true;
                    warningTextP1.text = ((player1WarningTime + 4f) - Time.fixedTime).ToString("F1") + "s until Respawn";
                }
                else
                {
                    warningBoxP1.enabled = false;
                    warningTextP1.text = "";
                }
                if (warning2On)
                {
                    warningBoxP2.enabled = true;
                    warningTextP2.text = ((player2WarningTime + 4f) - Time.fixedTime).ToString("F1") + "s until Respawn";
                }
                else
                {
                    warningBoxP2.enabled = false;
                    warningTextP2.text = "";
                }
                if(warning1On && player1Checkpoint == player2Checkpoint)
                {
                    warning1On = false;
                    warningBoxP2.enabled = false;
                    warningTextP2.text = "";
                }
                if(warning2On && player2Checkpoint == player1Checkpoint)
                {
                    warning2On = false;
                    warningBoxP2.enabled = false;
                    warningTextP2.text = "";
                }
            }
            //respawning 4 player
            else
            {
                print("Checking respawnign for 4 player");  
                //checks if player 1 is about to respawned
                if (player2 == firstPlace ||  player3 == firstPlace || player4 == firstPlace)
                {
                    if (player2Lap > player1Lap || player3Lap > player1Lap || player4Lap > player1Lap)
                    {
                        if ((player2Checkpoint + 7 >= player1Checkpoint + 1 && !warning1On) || (player3Checkpoint + 7 >= player1Checkpoint + 1 && !warning1On) || (player4Checkpoint + 7 >= player1Checkpoint + 1 && !warning1On))
                        {
                            player1WarningTime = Time.fixedTime;
                            warning1On = true;
                        }
                    }
                    else if ((player2Checkpoint >= player1Checkpoint + 1 && !warning1On) || (player3Checkpoint >= player1Checkpoint + 1 && !warning1On) || (player4Checkpoint >= player1Checkpoint + 1 && !warning1On))
                    {
                        player1WarningTime = Time.fixedTime;
                        warning1On = true;
                    }
                }
                //if player 1 reached time limit respawn
                if (Time.fixedTime >= (player1WarningTime + 4f) && warning1On)
                {
                    player1Script.respawnPlayer();
                    player1WeaponScript.currEnergy = 0;
                    if (player2 == firstPlace)
                    {
                        player1Checkpoint = player2Checkpoint;
                        player1Lap = player2Lap;
                    }
                    else if (player3 == firstPlace)
                    {
                        player1Checkpoint = player3Checkpoint;
                        player1Lap = player3Lap;
                    }
                    else
                    {
                        player1Checkpoint = player4Checkpoint;
                        player1Lap = player4Lap;
                    }
                    warning1On = false;
                }

                //checks if player 2 is about to respawned
                if (player1 == firstPlace || player3 == firstPlace || player4 == firstPlace)
                {
                    if (player1Lap > player2Lap || player3Lap > player2Lap || player4Lap > player2Lap)
                    {
                        if ((player1Checkpoint + 7 >= player2Checkpoint + 1 && !warning2On) || (player3Checkpoint + 7 >= player2Checkpoint + 1 && !warning2On) || (player4Checkpoint + 7 >= player2Checkpoint + 1 && !warning2On))
                        {
                            player2WarningTime = Time.fixedTime;
                            warning2On = true;
                        }
                    }
                    else if ((player1Checkpoint >= player2Checkpoint + 1 && !warning2On) || (player3Checkpoint >= player2Checkpoint + 1 && !warning2On) || (player4Checkpoint >= player2Checkpoint + 1 && !warning2On))
                    {
                        player2WarningTime = Time.fixedTime;
                        warning2On = true;
                        print("Warning for P2");
                    }
                }
                //if player 2 reached time limit respawn
                if (Time.fixedTime >= (player2WarningTime + 4f) && warning2On)
                {
                    player2Script.respawnPlayer();
                    player2WeaponScript.currEnergy = 0;
                    if (player1 == firstPlace)
                    {
                        player2Checkpoint = player1Checkpoint;
                        player2Lap = player1Lap;
                    }
                    else if (player3 == firstPlace)
                    {
                        player2Checkpoint = player3Checkpoint;
                        player2Lap = player3Lap;
                    }
                    else
                    {
                        player2Checkpoint = player4Checkpoint;
                        player2Lap = player4Lap;
                    }
                    warning2On = false;
                }

                //checks if player 3 is about to respawned
                if (player1 == firstPlace || player2 == firstPlace || player4 == firstPlace)
                {
                    if (player1Lap > player3Lap || player2Lap > player3Lap || player4Lap > player3Lap)
                    {
                        if ((player1Checkpoint + 7 >= player3Checkpoint + 1 && !warning3On) || (player2Checkpoint + 7 >= player3Checkpoint + 1 && !warning3On) || (player4Checkpoint + 7 >= player3Checkpoint + 1 && !warning3On))
                        {
                            player3WarningTime = Time.fixedTime;
                            warning3On = true;
                        }
                    }
                    else if ((player1Checkpoint >= player3Checkpoint + 1 && !warning3On) || (player2Checkpoint >= player3Checkpoint + 1 && !warning3On) || (player4Checkpoint >= player3Checkpoint + 1 && !warning3On))
                    {
                        player3WarningTime = Time.fixedTime;
                        warning3On = true;
                    }
                }
                //if player 3 reached time limit respawn
                if (Time.fixedTime >= (player3WarningTime + 4f) && warning3On)
                {
                    player3Script.respawnPlayer();
                    player3WeaponScript.currEnergy = 0;
                    if (player1 == firstPlace)
                    {
                        player3Checkpoint = player1Checkpoint;
                        player3Lap = player1Lap;
                    }
                    else if (player2 == firstPlace)
                    {
                        player3Checkpoint = player2Checkpoint;
                        player3Lap = player2Lap;
                    }
                    else
                    {
                        player3Checkpoint = player4Checkpoint;
                        player3Lap = player4Lap;
                    }
                    warning3On = false;
                }

                //checks if player 3 is about to respawned
                if (player1 == firstPlace || player2 == firstPlace || player2 == firstPlace)
                {
                    if (player1Lap > player4Lap || player2Lap > player4Lap || player3Lap > player4Lap)
                    {
                        if ((player1Checkpoint + 7 >= player4Checkpoint + 1 && !warning4On) || (player2Checkpoint + 7 >= player4Checkpoint + 1 && !warning4On) || (player3Checkpoint + 7 >= player4Checkpoint + 1 && !warning4On))
                        {
                            player4WarningTime = Time.fixedTime;
                            warning4On = true;
                        }
                    }
                    else if ((player1Checkpoint >= player4Checkpoint + 1 && !warning4On) || (player2Checkpoint >= player4Checkpoint + 1 && !warning4On) || (player3Checkpoint >= player4Checkpoint + 1 && !warning4On))
                    {
                        player4WarningTime = Time.fixedTime;
                        warning4On = true;
                    }
                }
                //if player 3 reached time limit respawn
                if (Time.fixedTime >= (player4WarningTime + 4f) && warning4On)
                {
                    player4Script.respawnPlayer();
                    player4WeaponScript.currEnergy = 0;
                    if (player1 == firstPlace)
                    {
                        player4Checkpoint = player1Checkpoint;
                        player4Lap = player1Lap;
                    }
                    else if (player2 == firstPlace)
                    {
                        player4Checkpoint = player2Checkpoint;
                        player4Lap = player2Lap;
                    }
                    else
                    {
                        player4Checkpoint = player3Checkpoint;
                        player4Lap = player3Lap;
                    }
                    warning4On = false;
                }

                //printing warning on UI
                if (warning1On)
                {
                    warningBoxP1.enabled = true;
                    warningTextP1.text = ((player1WarningTime + 4f) - Time.fixedTime).ToString("F1") + "s until Respawn";
                }
                else
                {
                    warningBoxP1.enabled = false;
                    warningTextP1.text = "";
                }
                if (warning2On)
                {
                    warningBoxP2.enabled = true;
                    warningTextP2.text = ((player2WarningTime + 4f) - Time.fixedTime).ToString("F1") + "s until Respawn";
                }
                else
                {
                    warningBoxP2.enabled = false;
                    warningTextP2.text = "";
                }
                if (warning3On)
                {
                    warningBoxP3.enabled = true;
                    warningTextP3.text = ((player3WarningTime + 4f) - Time.fixedTime).ToString("F1") + "s until Respawn";
                }
                else
                {
                    warningBoxP3.enabled = false;
                    warningTextP3.text = "";
                }
                if (warning4On)
                {
                    warningBoxP4.enabled = true;
                    warningTextP4.text = ((player4WarningTime + 4f) - Time.fixedTime).ToString("F1") + "s until Respawn";
                }
                else
                {
                    warningBoxP4.enabled = false;
                    warningTextP4.text = "";
                }
            }
            

            print("Player 1 Lap:" + player1Lap + " Checkpoint: " + player1Checkpoint + "\nPlayer 2 Lap: " + player2Lap + " Checkpoint: " + player2Checkpoint);

            /* no longer need this but this is the line code
            if (Countdown.start && Time.fixedTime > lineStopTime + 2f)
            {
                print("Resetting speed");
                line.SetActive(true);
                line.GetComponent<NavMeshAgent>().speed = firstPlace.GetComponent<playerController>().maxSpeed * 1.1f;
                line.GetComponent<NavMeshAgent>().angularSpeed = line.GetComponent<NavMeshAgent>().speed * 1.5f;
                line.GetComponent<NavMeshAgent>().acceleration = line.GetComponent<NavMeshAgent>().speed * 1.5f;
                navMeshScript.agent.SetDestination(navMeshScript.targets[navMeshScript.i].position);
            }*/
        }

        //get and display scores
        int[] scores = null;
        List<KeyValuePair<int, int>> temp = new List<KeyValuePair<int, int>>();
        if(PlayerPrefs.GetInt("num_p") == 1) {
            scores = new int[1];
            scores[0] = PlayerPrefs.GetInt("p0");
        }else if(PlayerPrefs.GetInt("num_p") == 2) {
            scores = new int[2];
            temp.Add(new KeyValuePair<int, int>(player1.GetComponent<CheckPoint>().GetScore(), PlayerPrefs.GetInt("p0")));
            temp.Add(new KeyValuePair<int, int>(player2.GetComponent<CheckPoint>().GetScore(), PlayerPrefs.GetInt("p1")));
            temp.Sort((x, y) => y.Key.CompareTo(x.Key));
            for (int i = 0; i < 2; i++) {
                scores[i] = temp[i].Value;
            }
        } else if(PlayerPrefs.GetInt("num_p") == 4) {
            scores = new int[4];
            temp.Add(new KeyValuePair<int, int>(player1.GetComponent<CheckPoint>().GetScore(), PlayerPrefs.GetInt("p0")));
            temp.Add(new KeyValuePair<int, int>(player2.GetComponent<CheckPoint>().GetScore(), PlayerPrefs.GetInt("p1")));
            temp.Add(new KeyValuePair<int, int>(player3.GetComponent<CheckPoint>().GetScore(), PlayerPrefs.GetInt("p2")));
            temp.Add(new KeyValuePair<int, int>(player4.GetComponent<CheckPoint>().GetScore(), PlayerPrefs.GetInt("p3")));
            temp.Sort((x, y) => y.Key.CompareTo(x.Key));
            for (int i = 0; i < 4; i++) {
                scores[i] = temp[i].Value;
            }
        }
        pos_ui.UpdateUI(scores);
        if (PlayerPrefs.GetInt("num_p") == 1)
        {
            if(player1Lap == laps + 1)
            {
                List<KeyValuePair<int, int>> finalScore = new List<KeyValuePair<int, int>>();
                finalScore.Add(new KeyValuePair<int, int>(PlayerPrefs.GetInt("p0"), player1.GetComponent<CheckPoint>().GetScore()));
                endCam.SetActive(true);
                endCanv.SetActive(true);
                endCanv.GetComponent<EndUI>().UpdateUI(finalScore);
            }
        }
        else if (PlayerPrefs.GetInt("num_p") == 2)
        {
            if (player1Lap >= laps + 1 && player2Lap >= laps + 1)
            {
                print("FINISH");
                List<KeyValuePair<int, int>> finalScore = new List<KeyValuePair<int, int>>();
                finalScore.Add(new KeyValuePair<int, int>(PlayerPrefs.GetInt("p0"), player1.GetComponent<CheckPoint>().GetScore()));
                finalScore.Add(new KeyValuePair<int, int>(PlayerPrefs.GetInt("p1"), player2.GetComponent<CheckPoint>().GetScore()));
                endCam.SetActive(true);
                endCanv.SetActive(true);
                endCanv.GetComponent<EndUI>().UpdateUI(finalScore);
            }
        }
        else if (PlayerPrefs.GetInt("num_p") == 4)
        {
            if (player1Lap >= laps + 1 && player2Lap >= laps + 1 && player3Lap >= laps + 1 && player4Lap >= laps + 1)
            {
                List<KeyValuePair<int, int>> finalScore = new List<KeyValuePair<int, int>>();
                finalScore.Add(new KeyValuePair<int, int>(PlayerPrefs.GetInt("p0"), player1.GetComponent<CheckPoint>().GetScore()));
                finalScore.Add(new KeyValuePair<int, int>(PlayerPrefs.GetInt("p1"), player2.GetComponent<CheckPoint>().GetScore()));
                finalScore.Add(new KeyValuePair<int, int>(PlayerPrefs.GetInt("p2"), player3.GetComponent<CheckPoint>().GetScore()));
                finalScore.Add(new KeyValuePair<int, int>(PlayerPrefs.GetInt("p3"), player4.GetComponent<CheckPoint>().GetScore()));
                endCam.SetActive(true);
                endCanv.SetActive(true);
                endCanv.GetComponent<EndUI>().UpdateUI(finalScore);
            }
        }
    }

    //increases player checkpoint/lap numbers
    public void updatePlayer1Checkpoint() {
        if(player1Checkpoint == checkPoints.Length)
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
        if (player2Checkpoint == checkPoints.Length)
        {
            player2Checkpoint = 1;
            player2Lap++;
            print("updating player 2 lap: " + player2Lap);
        }
        else
        {
            player2Checkpoint++;
        }
    }

    public void updatePlayer3Checkpoint()
    {
        if (player3Checkpoint == checkPoints.Length)
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
        if (player4Checkpoint == checkPoints.Length)
        {
            player4Checkpoint = 1;
            player4Lap++;
        }
        else
        {
            player4Checkpoint++;
        }
    }

    //gets a specfifc players checkpoint number
    public int getCheckpointNum(int p)
    {
        if(p == 1) { return player1Checkpoint; }
        else if(p == 2) { return player2Checkpoint; }
        else if(p == 3) { return player3Checkpoint; }
        else if(p == 4) { return player4Checkpoint; }
        else { return -1; }
    }

    //returns the game object of whoever is in first place 
    public GameObject getFirstPlace() { return firstPlace; }

    //prolly dont need this anymore
    public void adjustLineSpeed()
    {
        line.SetActive(false);
        line.GetComponent<NavMeshAgent>().speed = 0;
        line.GetComponent<NavMeshAgent>().angularSpeed = 0;
        line.GetComponent<NavMeshAgent>().acceleration = 0;
        lineStopTime = Time.fixedTime;
    }
}
