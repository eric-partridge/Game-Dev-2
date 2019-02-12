using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParaColor : MonoBehaviour {

    public int freqBand = 0;
    public Color target;
    private Renderer rend;
    private Color myColor;
    private Color current;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
        current = rend.material. GetColor("_Color");
    }

    // Update is called once per frame
    void Update()
    {
        myColor = new Color((Mathf.Abs(chuck.intensity[freqBand] - 1) * current.r) + (chuck.intensity[freqBand] * target.r),
                            (Mathf.Abs(chuck.intensity[freqBand] - 1) * current.g) + (chuck.intensity[freqBand] * target.g),
                            (Mathf.Abs(chuck.intensity[freqBand] - 1) * current.b) + (chuck.intensity[freqBand] * target.b), target.a);
        rend.material.SetColor("_Color", myColor);
    }
}
