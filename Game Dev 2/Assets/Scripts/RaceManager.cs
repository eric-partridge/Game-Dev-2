using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RaceManager : MonoBehaviour
{

    public Transform[] checkPoints;
    public GameObject player1;
    public GameObject player2;
    public GameObject line;

    private int player1Checkpoint = 0;
    private int player2Checkpoint = 0;
    private int player1Lap = 0;
    private int player2Lap = 0;
    private float player1Distance = 0;
    private float player2Distance = 0;
    private GameObject firstPlace;

    // Start is called before the first frame update
    void Start()
    {
        
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
            print("Updating lap");
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
