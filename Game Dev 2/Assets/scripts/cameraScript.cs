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
        float yAngle = player.eulerAngles.y;
        float xAngle = pivot.eulerAngles.x;
        rotation = Quaternion.Euler(xAngle, yAngle, 0f);

        transform.position = player.position - (rotation * offset);

        /*RaycastHit wallHit = new RaycastHit();
        if (Physics.Linecast(player.position, transform.position, out wallHit))
        {
            Debug.DrawLine(transform.position, player.position, Color.green);
            if (wallHit.collider.tag != "Collectible")
            {
                transform.position = new Vector3(wallHit.point.x, wallHit.point.y, wallHit.point.z) + wallHit.normal;
            }
        }*/

        transform.LookAt(player);
    }
}
