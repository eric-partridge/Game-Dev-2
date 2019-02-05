using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightResponse : MonoBehaviour {

    public int band = 0;
    public float minIntenseity, maxIntensity;
    public bool effectRange = false;
    Light myLight;

	// Use this for initialization
	void Start () {
        myLight = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        myLight.intensity = (chuck.bandBuffer[band] * (maxIntensity - minIntenseity)) + minIntenseity;
        if(effectRange)
        {
            myLight.range = (chuck.bandBuffer[band] * (maxIntensity - minIntenseity)) + minIntenseity;
        }
	}
}
