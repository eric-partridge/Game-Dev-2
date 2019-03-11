using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponScript : MonoBehaviour {

    public GameObject projectile;
    public Transform frontBarrel;
    public Transform sideBarrelR;
    public Transform sideBarrelL;
    public Transform backBarrel;
    public float forwardSpeed;
    public float sideSpeed;
    public float backwardSpeed;
    public float currEnergy;
    public float coolDownTime;
    Rigidbody rb;

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
            tempProjectile.transform.position = frontBarrel.position + (transform.forward);
            rb = tempProjectile.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 200f , ForceMode.VelocityChange);
            shootTime = Time.time + coolDownTime;
            currEnergy--;
        }
        if (Input.GetButton("Circle") && currEnergy > 0 && Time.time > shootTime)
        {
            print("Shooting right");
            GameObject tempProjectile = Instantiate(projectile) as GameObject;
            tempProjectile.transform.position = sideBarrelR.position + (transform.right);
            rb = tempProjectile.GetComponent<Rigidbody>();
            rb.AddForce(transform.right * 100f, ForceMode.VelocityChange);
            shootTime = Time.time + coolDownTime;
            currEnergy--;
        }
        if (Input.GetButton("Square") && currEnergy > 0 && Time.time > shootTime)
        {
            print("Shooting right");
            GameObject tempProjectile = Instantiate(projectile) as GameObject;
            tempProjectile.transform.position = sideBarrelL.position + (-transform.right);
            rb = tempProjectile.GetComponent<Rigidbody>();
            rb.AddForce(-transform.right * 100f, ForceMode.VelocityChange);
            shootTime = Time.time + coolDownTime;
            currEnergy--;
        }
        if (Input.GetButton("X") && currEnergy > 0 && Time.time > shootTime)
        {
            print("Shooting right");
            GameObject tempProjectile = Instantiate(projectile) as GameObject;
            tempProjectile.transform.position = backBarrel.position + (-transform.forward);
            rb = tempProjectile.GetComponent<Rigidbody>();
            rb.AddForce(-transform.forward * 100f, ForceMode.VelocityChange);
            shootTime = Time.time + coolDownTime;
            currEnergy--;
        }
        //rb.velocity = Camera.main.transform.forward * projectileSpeed;
    }
}
