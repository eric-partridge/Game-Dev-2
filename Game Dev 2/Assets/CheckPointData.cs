using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointData : MonoBehaviour
{

    public float hitTime;
    private float waitTime;
    public bool hit = false;
    bool first = true;
    private Renderer rend;
    public Color triggerColor;
    private Color current;

    // Use this for initialization
    void Start()
    {
        waitTime = 0;
        rend = GetComponent<Renderer>();
        current = rend.material.GetColor("_Color");
    }

    void FixedUpdate()
    {
        if (hit)
        {
            if (first)
            {
                rend.material.SetColor("_Color", triggerColor);
                hitTime = LapTimer.timer;
                waitTime = LapTimer.timer + 20;
                first = false;
            }
            else if (waitTime < LapTimer.timer)
            {
                hit = false;
                first = true;
                rend.material.SetColor("_Color", current);
            }
        }

    }
}
