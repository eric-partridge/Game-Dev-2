using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapTimer : MonoBehaviour {

    public static float timer;
    public Text currentTime;

    // Use this for initialization
    private void Awake()
    {
	}

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;
        currentTime.text = string.Format("{0:0.00}", timer);
    }
}
