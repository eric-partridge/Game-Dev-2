using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPM_Clock : MonoBehaviour {

    public float BPM;
    private float BPS;
    public static float SPB;
    public static bool trigger = true;
    public static float currentTime;
    private float trigTime;
    public AudioSource aud;

    // Use this for initialization

    void Start () {
        BPS = BPM / 60;
        SPB = 60 / BPM;
        aud.Play();
        LapTimer.timer = aud.time;
        currentTime = LapTimer.timer;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        currentTime += Time.fixedDeltaTime;
        trigTime += Time.fixedDeltaTime;
        if (currentTime + SPB/6 > SPB)
        {
            trigger = true;
            trigTime = 0;
        }
        if(trigTime > SPB/2.5)
        {
            trigger = false;
        }
        if (currentTime > SPB)
        {
            currentTime = 0;
        }
    }
}
