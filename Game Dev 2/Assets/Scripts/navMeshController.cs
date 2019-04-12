using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navMeshController : MonoBehaviour
{

    private NavMeshAgent agent;
    public Transform[] targets;

    private int i = 0;
    private bool incremented = false;
    private float distAway = 5f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(targets[i].position);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //set next destination for line
        if (agent.remainingDistance < 10f && agent.remainingDistance != 0 && !incremented)
        {
            print("incremented to: " + i);
            i++;
            if(i == targets.Length)
            {
                i = 0;
            }
            agent.SetDestination(targets[i].position);
            incremented = true;
            
        }
        if(agent.remainingDistance > 10f)
        {
            incremented = false;
        }
    }
}
