using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {
    public float movementSpeed = 10;
    private float rotateX;
    private float rotateY;
    Camera mycam;
    // Use this for initialization
    void Start () {
        mycam = GetComponent<Camera>();
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * Time.deltaTime * movementSpeed;
        }
        rotateX += 0.5f * Input.GetAxis("Mouse X");
        rotateY -= 0.5f * Input.GetAxis("Mouse Y");
        rotateY = Mathf.Clamp(rotateY, -90, 90);
        transform.eulerAngles = new Vector3(rotateY, rotateX, 0);
    }
}
