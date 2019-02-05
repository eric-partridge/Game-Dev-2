using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmpReact : MonoBehaviour {

    public float scaleFactor = 10;
    public float startValue = 5;
    private Color myColor;
    private Material mat;

    // Use this for initialization
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update () {
        transform.localScale = new Vector3((chuck.amplitude * scaleFactor) + startValue, (chuck.amplitude * scaleFactor) + startValue, (chuck.amplitude * scaleFactor) + startValue);
        myColor = new Color(chuck.amplitude, chuck.amplitude, 1);
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", myColor);
    }
}
