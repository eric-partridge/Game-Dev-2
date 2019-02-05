using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParaCube : MonoBehaviour {

    public int freqBand = 0;
    private Color myColor;
    private Material mat;
    public float strength = 5;

    // Use this for initialization
    void Start () {
        mat = GetComponent<MeshRenderer>().material;	
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3(transform.localScale.x, (chuck.bandBuffer[freqBand] * strength) + 1, transform.localScale.z);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        myColor = new Color(chuck.intensity[freqBand], chuck.intensity[freqBand], 1);
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", myColor);
    }
}
