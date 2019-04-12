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
        if (agent.remainingDistance < distAway && agent.remainingDistance != 0 && !incremented)
        {
            i++;
            print("I is: " + i);
            if(i == targets.Length)
            {
                i = 0;
            }
            agent.SetDestination(targets[i].position);
            incremented = true;
            if (i + 1 < targets.Length)
            {
                if (targets[i + 1].tag == "corner") { distAway = 5f; }
            }
            else { distAway = 10f; }
            
        }
        if(agent.remainingDistance > 10f)
        {
            incremented = false;
        }
    }
}
