using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParaEmission : MonoBehaviour
{

    public int freqBand = 0;
    public Color target;
    public float maxIntensity = 3f;
    public float minIntensity = 0f;
    private Renderer rend;
    private Color myColor;
    private Color current;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
        current = rend.material.GetColor("_EmissionColor");
    }

    // Update is called once per frame
    void Update()
    {
        myColor = new Color((Mathf.Abs(chuck.intensity[freqBand]) * current.r) + (chuck.intensity[freqBand] - 1 * target.r),
                            (Mathf.Abs(chuck.intensity[freqBand]) * current.g) + (chuck.intensity[freqBand] - 1 * target.g),
                            (Mathf.Abs(chuck.intensity[freqBand]) * current.b) + (chuck.intensity[freqBand] - 1 * target.b), target.a);
        myColor *= Mathf.LinearToGammaSpace((maxIntensity * chuck.intensity[freqBand]) + minIntensity);
        rend.material.SetColor("_EmissionColor", myColor);
    }
}
