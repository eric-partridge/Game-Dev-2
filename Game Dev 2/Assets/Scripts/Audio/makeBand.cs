using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeBand : MonoBehaviour {
    public GameObject cube;
    private GameObject[] cubeList = new GameObject[8];
    private GameObject[] cubeListNoBuff = new GameObject[8];
    private Color myColor;
    private MeshRenderer[] meshList = new MeshRenderer[8];

    // Use this for initialization
    void Start () {
        for (int i = 0; i < 8; i++)
        {
            GameObject instance = (GameObject)Instantiate(cube);
            instance.transform.position = this.transform.position;
            instance.transform.rotation = this.transform.rotation;
            instance.transform.parent = this.transform;
            instance.transform.localScale *= 3;
            instance.transform.position += Vector3.forward * 5 * i;
            cubeList[i] = instance;
            meshList[i] = cubeList[i].GetComponent<MeshRenderer>();

            /*GameObject instance2 = (GameObject)Instantiate(cube);
            instance2.transform.position = this.transform.position + (Vector3.forward *50);
            instance2.transform.parent = this.transform;
            instance2.transform.localScale *= 10;
            instance2.transform.position += Vector3.right * 15 * i;
            cubeListNoBuff[i] = instance2;*/
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 8; i++)
        {
            cubeList[i].transform.localScale = new Vector3(cubeList[i].transform.localScale.x, (chuck.bandBuffer[i] * 5) + 1, cubeList[i].transform.localScale.z);
            cubeList[i].transform.position = new Vector3(cubeList[i].transform.position.x, cubeList[i].transform.position.y, cubeList[i].transform.position.z);
            myColor = new Color (chuck.intensity[i], chuck.intensity[i], 1);
            meshList[i].materials[0].EnableKeyword("_EMISSION");
            meshList[i].materials[0].SetColor("_EmissionColor", myColor);
            //cubeListNoBuff[i].transform.localScale = new Vector3(cubeListNoBuff[i].transform.localScale.x, (chuck.bands[i] * 10) + 1, cubeListNoBuff[i].transform.localScale.z);
            //cubeListNoBuff[i].transform.position = new Vector3(cubeListNoBuff[i].transform.position.x, cubeListNoBuff[i].transform.localScale.y / 2, cubeListNoBuff[i].transform.position.z);
        }
    }
}
