using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour {

    private float diff = 0;
    private float checkTime;
    public int score = 0;
    private List<GameObject> hitList;

	// Use this for initialization
	void Start () {
        checkTime = LapTimer.timer;
        score = 0;
        hitList = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
        for(int i = 0; i < hitList.Count; i++)
        {
            if(hitList[i].GetComponent<CheckPointData>().hit == false)
            {
                hitList.Remove(hitList[i]);
                i--;
            }
        }
    }

    public int GetScore() {
        return score;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "CheckPoint")
        {
            if (!other.GetComponent<CheckPointData>().hit)
            {
                other.gameObject.GetComponent<CheckPointData>().hit = true;
                hitList.Add(other.gameObject);
                score += 150;
            }
            else if(!hitList.Contains(other.gameObject))
            {
                hitList.Add(other.gameObject);
                diff = LapTimer.timer - other.GetComponent<CheckPointData>().hitTime;
                score += Mathf.RoundToInt(15 - (diff * 3)) * 10;
            }
        }
    }
}
