using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatColorResponse : MonoBehaviour {

    public Color target;
    private Renderer rend;
    private Color myColor;
    private Color current;
    public float rate = 0.1f;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        current = rend.material.GetColor("_EmissionColor");
    }
	
	// Update is called once per frame
	void Update () {
		if(BPM_Clock.trigger == true)
        {
            myColor = new Color(rate * target.r, rate * target.g, rate * target.b);
            rend.material.SetColor("_EmissionColor", myColor);
            print("YES");
        }
        else
        {
            myColor = new Color(rate * current.r, rate * current.g, rate * current.b);
            rend.material.SetColor("_EmissionColor", myColor);
        }
	}
}
