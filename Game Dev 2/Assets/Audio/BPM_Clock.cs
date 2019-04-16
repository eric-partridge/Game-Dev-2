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
    private void Awake()
    {
        currentTime = Time.time;
    }

    void Start () {
        BPS = BPM / 60;
        SPB = 60 / BPM;
        LapTimer.timer = 0;
        aud.Play();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        currentTime += Time.fixedDeltaTime;
        trigTime += Time.fixedDeltaTime;
        if (currentTime + 0.02f > SPB)
        {
            trigger = true;
            trigTime = 0;
        }
        if(trigTime > 0.03f)
        {
            trigger = false;
        }
        if (currentTime > SPB)
        {
            currentTime = 0;
        }
    }
}
