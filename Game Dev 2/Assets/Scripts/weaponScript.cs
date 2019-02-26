using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponScript : MonoBehaviour {

    public GameObject projectile;
    public Transform barrell;
    public float projectileSpeed;
    public float currEnergy;
    public float coolDownTime;

    private float shootTime = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetButton("Triangle") && currEnergy > 0 && Time.time > shootTime)
        {
            print("Shooting");
            GameObject tempProjectile = Instantiate(projectile) as GameObject;
            tempProjectile.transform.position = barrell.position + (transform.forward);
            Rigidbody rb = tempProjectile.GetComponent<Rigidbody>();
            rb.velocity = Camera.main.transform.forward * projectileSpeed;
            shootTime = Time.time + coolDownTime;
            currEnergy--;
        }
	}
}
