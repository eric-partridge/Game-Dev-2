using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simplePulse : MonoBehaviour {

    public float scaleFactor = 10;
    public float startValue = 5;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3((chuck.amplitude * scaleFactor) + startValue, (chuck.amplitude * scaleFactor) + startValue, (chuck.amplitude * scaleFactor) + startValue);
    }
}
