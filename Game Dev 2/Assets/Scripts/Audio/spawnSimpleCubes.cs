using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnSimpleCubes : MonoBehaviour
{

    public GameObject paramCube;
    public float strength = 10f;
    public float thickness = 1f;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            makeCube(90, 11.25f * i, 0, i);
            makeCube(0, 11.25f * i, 90, i);
            makeCube(90, 11.25f * i, 90, i);
            makeCube(90, 11.25f * i, -90, i);
            //makeCube(90, 90, 11.25f * i + 90, i);
            //makeCube(11.25f * i, 90, 90, i);
            //makeCube(11.25f * i + 90, 90, 90, i);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void makeCube(float x, float y, float z, int i)
    {
        GameObject cube = Instantiate(paramCube,
                transform.position,
                new Quaternion(x, y, z, 0));
        cube.GetComponent<simeplePara>().freqBand = i;
        cube.GetComponent<simeplePara>().strength = strength;
        cube.transform.localScale *= thickness;
        cube.name = "paraCube " + i;
        cube = Instantiate(paramCube,
                transform.position,
                new Quaternion(x, y + 90, z, 0));
        cube.GetComponent<simeplePara>().freqBand = i;
        cube.GetComponent<simeplePara>().strength = strength;
        cube.transform.localScale *= thickness;
        cube.name = "paraCube " + i;
        cube = Instantiate(paramCube,
                transform.position,
                new Quaternion(x, y + 180, z, 0));
        cube.GetComponent<simeplePara>().freqBand = i;
        cube.GetComponent<simeplePara>().strength = strength;
        cube.transform.localScale *= thickness;
        cube.name = "paraCube " + i;
        cube = Instantiate(paramCube,
                transform.position,
                new Quaternion(x, y + 270, z, 0));
        cube.GetComponent<simeplePara>().freqBand = i;
        cube.GetComponent<simeplePara>().strength = strength;
        cube.transform.localScale *= thickness;
        cube.name = "paraCube " + i;
    }
}
