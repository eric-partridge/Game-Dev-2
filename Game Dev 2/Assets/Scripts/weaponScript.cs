﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponScript : MonoBehaviour {

    public GameObject projectile;
    public Transform frontBarrelM;
    public Transform frontBarrelL;
    public Transform frontBarrelR;
    public Transform sideBarrelR;
    public Transform sideBarrelL;
    public Transform backBarrel;
    public float forwardSpeed;
    public float sideSpeed;
    public float backwardSpeed;
    public float currEnergy;
    public float coolDownTime;
    public bool useSpread;
    Rigidbody rbM;
    Rigidbody rbL;
    Rigidbody rbR;

    private float shootTime = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetButton("Triangle") && currEnergy > 0 && Time.time > shootTime)
        {
            GameObject tempProjectileM = Instantiate(projectile) as GameObject;
            tempProjectileM.transform.position = frontBarrelM.position + (transform.forward);
            rbM = tempProjectileM.GetComponent<Rigidbody>();
            rbM.AddForce(frontBarrelM.transform.forward * 200f, ForceMode.VelocityChange);

            if (useSpread)
            {
                GameObject tempProjectileL = Instantiate(projectile) as GameObject;
                tempProjectileL.transform.position = frontBarrelL.position + (frontBarrelL.transform.forward);
                rbL = tempProjectileL.GetComponent<Rigidbody>();
                rbL.AddForce(frontBarrelL.transform.forward * 200f, ForceMode.VelocityChange);

                GameObject tempProjectileR = Instantiate(projectile) as GameObject;
                tempProjectileR.transform.position = frontBarrelR.position + (transform.forward);
                rbR = tempProjectileR.GetComponent<Rigidbody>();
                rbR.AddForce(frontBarrelR.transform.forward * 200f, ForceMode.VelocityChange);
            }

            shootTime = Time.time + coolDownTime;
            currEnergy--;
        }
        if (Input.GetButton("Circle") && currEnergy > 0 && Time.time > shootTime)
        {
            GameObject tempProjectile = Instantiate(projectile) as GameObject;
            tempProjectile.transform.position = sideBarrelR.position + (transform.right);
            rbM = tempProjectile.GetComponent<Rigidbody>();
            rbM.AddForce(transform.right * 100f, ForceMode.VelocityChange);
            shootTime = Time.time + coolDownTime;
            currEnergy--;
        }
        if (Input.GetButton("Square") && currEnergy > 0 && Time.time > shootTime)
        {
            GameObject tempProjectile = Instantiate(projectile) as GameObject;
            tempProjectile.transform.position = sideBarrelL.position + (-transform.right);
            rbM = tempProjectile.GetComponent<Rigidbody>();
            rbM.AddForce(-transform.right * 100f, ForceMode.VelocityChange);
            shootTime = Time.time + coolDownTime;
            currEnergy--;
        }
        if (Input.GetButton("X") && currEnergy > 0 && Time.time > shootTime)
        {
            GameObject tempProjectile = Instantiate(projectile) as GameObject;
            tempProjectile.transform.position = backBarrel.position + (-transform.forward);
            rbM = tempProjectile.GetComponent<Rigidbody>();
            rbM.AddForce(-transform.forward * 100f, ForceMode.VelocityChange);
            shootTime = Time.time + coolDownTime;
            currEnergy--;
        }
        //rb.velocity = Camera.main.transform.forward * projectileSpeed;
    }
}
