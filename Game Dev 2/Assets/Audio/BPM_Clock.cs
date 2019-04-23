using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPM_Clock : MonoBehaviour {

    public float BPM;
    private float BPS;
    public static float SPB;
    public static bool trigger = false;
    public static float currentTime;
    private float trigTime;
    private int currentBeat = 1;
    public AudioSource aud;

    // Use this for initialization

    void Start () {
        BPS = BPM / 60;
        SPB = 60 / BPM;
        aud.Play();
        LapTimer.timer = aud.time;
        currentTime = aud.time;
        InvokeRepeating("Beat", currentTime - SPB / 6f,  SPB);
        InvokeRepeating("Off",  currentTime + SPB / 2.75f, SPB);
    }
	
	// Update is called once per frame
	void Update () {
        print(trigger);
    }

    private void Beat()
    {
        trigger = true;

    }
    private void Off()
    {
        trigger = false;

    }
}
