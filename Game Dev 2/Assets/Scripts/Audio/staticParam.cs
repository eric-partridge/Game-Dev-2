using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staticParam : MonoBehaviour
{

    public static int _freqband = 0;
    private Color _myColor;
    private Material _mat;
    public static float _strength = 5;

    // Use this for initialization
    void Start()
    {
        _mat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x, (chuck.bandBuffer[_freqband] * _strength) + 1, transform.localScale.z);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        _myColor = new Color(chuck.intensity[_freqband], chuck.intensity[_freqband], 1);
        _mat.EnableKeyword("_EMISSION");
        _mat.SetColor("_EmissionColor", _myColor);
    }
}
