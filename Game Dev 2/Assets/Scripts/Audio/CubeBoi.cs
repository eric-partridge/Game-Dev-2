using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBoi : MonoBehaviour {
    public GameObject cube;
    private GameObject[] cubeList = new GameObject[512];
	// Use this for initialization
	void Start () {
        for (int i = 0; i < 512; i++)
        {
            GameObject instance = (GameObject)Instantiate(cube);
            instance.transform.position = this.transform.position;
            instance.transform.parent = this.transform;
            this.transform.eulerAngles = new Vector3(0, -0.703125f * i, 0);
            instance.transform.position += Vector3.forward*10;
            instance.name = "Sample " + i;
            cubeList[i] = instance;
        }
	}
	
	// Update is called once per frame
	void Update () {
        for(int i = 0; i < 512; i++)
        {
            cubeList[i].transform.localScale = new Vector3(1, (chuck.samples[i] * i * i/4)/8, 1);
            cubeList[i].transform.position = new Vector3(cubeList[i].transform.position.x, 50 + (chuck.samples[i] * i * i / 4)/16, cubeList[i].transform.position.z);
        }
	}
}
