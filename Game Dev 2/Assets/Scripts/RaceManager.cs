using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RaceManager : MonoBehaviour
{

    public Transform[] checkPoints;
    public GameObject player1;
    public GameObject player2;
    public Camera camera1;
    public Camera camera2;
    public GameObject line;
    public GameObject[] ships;

    private int player1Checkpoint = 0;
    private int player2Checkpoint = 0;
    private int player1Lap = 0;
    private int player2Lap = 0;
    private float player1Distance = 0;
    private float player2Distance = 0;
    private GameObject firstPlace;
    private cameraScript cam1Script;
    private cameraScript cam2Script;
    private playerController player1Script;
    private playerController player2Script;

    // Start is called before the first frame update
    void Awake()
    {

        Countdown.ships = new List<GameObject>();

        if (PlayerPrefs.GetInt("num_p") == 1)
        {
            //disables other cameras
            camera2.enabled = false;
            camera1.rect = new Rect(0, 0, 1, 1);

            //instantiates player1
            player1 = GameObject.Instantiate(ships[PlayerPrefs.GetInt("p0")]);
            player1.transform.position = new Vector3(0, 17, -20.5f);
            player1Script = player1.GetComponent<playerController>();
            player1Script.playerNum = 1;
            player1Script.otherPlayer = null;

            //sets up camera
            cam1Script = camera1.GetComponent<cameraScript>();
            cam1Script.player = player1.transform;

            line.SetActive(false);
            Countdown.ships.Add(player1);
        }
        else if (PlayerPrefs.GetInt("num_p") == 2)
        {
            //enables 2 cameras
            camera1.enabled = true;
            camera2.enabled = true;
            camera1.rect = new Rect(0, .5f, 1, .5f);
            camera2.rect = new Rect(0f, 0, 1, .5f);

            //instantiates player 1
            player1 = GameObject.Instantiate(ships[PlayerPrefs.GetInt("p0")]);
            player1.transform.position = new Vector3(8.5f, 17, -20.5f);
            player1Script = player1.GetComponent<playerController>();
            player1Script.playerNum = 1;

            //instantiates player2
            player2 = GameObject.Instantiate(ships[PlayerPrefs.GetInt("p1")]);
            player2.transform.position = new Vector3(-8.5f, 17, -20.5f);
            player2Script = player2.GetComponent<playerController>();
            player2Script.playerNum = 2;

            player1Script.otherPlayer = player2;
            player2Script.otherPlayer = player1;

            //sets up cameras
            cam1Script = camera1.GetComponent<cameraScript>();
            cam1Script.player = player1.transform;
            cam2Script = camera2.GetComponent<cameraScript>();
            cam2Script.player = player2.transform;

            Countdown.ships.Add(player1);
            Countdown.ships.Add(player2);
        }
    }

    // Update is called once per frame
    void LateUpdate()
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
        if (Countdown.start)
        {
            line.GetComponent<NavMeshAgent>().speed = player1.GetComponent<playerController>().maxSpeed * 1.25f;
            line.GetComponent<NavMeshAgent>().angularSpeed = line.GetComponent<NavMeshAgent>().speed * 1.5f;
            line.GetComponent<NavMeshAgent>().acceleration = line.GetComponent<NavMeshAgent>().speed * 1.5f;
        }
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

    public GameObject getFirstPlace() { return firstPlace; }
}
