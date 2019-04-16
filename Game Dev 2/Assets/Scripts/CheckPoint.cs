using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour {

    public Text lapTime;
    private float diff = 0;
    private float checkTime;
    public static int score = 0;

	// Use this for initialization
	void Start () {
        checkTime = LapTimer.timer;
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {

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
                other.GetComponent<CheckPointData>().hit = true;
                score += 150;
                lapTime.text = string.Format("{0} GET: {1}", score, 150);
            }
            else
            {
                diff = LapTimer.timer - other.GetComponent<CheckPointData>().hitTime;
                score += Mathf.RoundToInt(15 - (diff * 5)) * 10;
                lapTime.text = string.Format("{0} GET: {1}", score, Mathf.RoundToInt(15 - diff) * 10);
            }
        }
    }
}
