using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost_Pad : MonoBehaviour {

    public int direction = 1;
    public string type = "UP";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public string GetType2()
    {
        return type;
    }

    public int GetDirection()
    {
        return direction;
    }
}
