using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simeplePara : MonoBehaviour
{

    public int freqBand = 0;
    public float strength = 5;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x, (chuck.bandBuffer[freqBand] * strength) + 1, transform.localScale.z);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
}
