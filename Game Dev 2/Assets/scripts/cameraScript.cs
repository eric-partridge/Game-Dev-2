using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {

    public Transform player;
    public Transform pivot;
    public float camRotateSpeed = 10f;
    public Vector3 offset;
    public bool useOffset;

    private Quaternion rotation;


    // Use this for initialization
    void Start () {
        if (!useOffset)
        {
            offset = player.position - transform.position;
        }
        pivot.transform.position = player.position;
        pivot.transform.parent = player;
    }
	 
	// Update is called once per frame
	void LateUpdate () {

        float horizontal = Input.GetAxis("Horizontal") * camRotateSpeed;
        player.Rotate(0, horizontal, 0);
        pivot.Rotate(0, 0, 0);

        float yAngle = player.eulerAngles.y;
        float xAngle = pivot.eulerAngles.x;
        rotation = Quaternion.Euler(xAngle, yAngle, 0f);

        transform.position = player.position - (rotation * offset);
        transform.LookAt(player);
    }
}
