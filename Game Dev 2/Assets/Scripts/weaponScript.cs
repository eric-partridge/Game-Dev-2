using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponScript : MonoBehaviour {

    public GameObject projectile;
    public GameObject player;
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

        float leftStickX = Input.GetAxis("Horizontal");

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
                Destroy(tempProjectileL, 5f);

                GameObject tempProjectileR = Instantiate(projectile) as GameObject;
                tempProjectileR.transform.position = frontBarrelR.position + (transform.forward);
                rbR = tempProjectileR.GetComponent<Rigidbody>();
                rbR.AddForce(frontBarrelR.transform.forward * 200f, ForceMode.VelocityChange);
                Destroy(tempProjectileR, 5f);
            }

            shootTime = Time.time + coolDownTime;
            currEnergy--;
            Destroy(tempProjectileM, 5f);
        }
        if (Input.GetButton("Circle") && currEnergy > 0 && Time.time > shootTime)
        {
            GameObject tempProjectile = Instantiate(projectile) as GameObject;
            tempProjectile.transform.position = sideBarrelR.position + (transform.right);
            rbM = tempProjectile.GetComponent<Rigidbody>();
            rbM.AddForce(transform.right * 100f, ForceMode.VelocityChange);
            Vector3 newVelocity;
            if(leftStickX > 0)
            {
                newVelocity = Vector3.RotateTowards((player.GetComponent<Rigidbody>().velocity) * .5f, transform.right, 180f * Time.deltaTime * Mathf.Deg2Rad, 0);
            }
            else
            {
                newVelocity = Vector3.RotateTowards((player.GetComponent<Rigidbody>().velocity) * 1.5f, transform.right, 180f * Time.deltaTime * Mathf.Deg2Rad, 0);
            }

            rbM.velocity = newVelocity;
            shootTime = Time.time + coolDownTime;
            currEnergy--;
            Destroy(tempProjectile, 5f);
        }
        if (Input.GetButton("Square") && currEnergy > 0 && Time.time > shootTime)
        {
            GameObject tempProjectile = Instantiate(projectile) as GameObject;
            tempProjectile.transform.position = sideBarrelL.position + (-transform.right);
            rbM = tempProjectile.GetComponent<Rigidbody>();
            rbM.AddForce(-transform.right * 100f, ForceMode.VelocityChange);
            Vector3 newVelocity;
            if (leftStickX < 0)
            {
                newVelocity = Vector3.RotateTowards((player.GetComponent<Rigidbody>().velocity) * .5f, -transform.right, 180f * Time.deltaTime * Mathf.Deg2Rad, 0);
            }
            else
            {
                newVelocity = Vector3.RotateTowards((player.GetComponent<Rigidbody>().velocity) * 1.5f, -transform.right, 180f * Time.deltaTime * Mathf.Deg2Rad, 0);
            }
            rbM.velocity = newVelocity;
            shootTime = Time.time + coolDownTime;
            currEnergy--;
            Destroy(tempProjectile, 5f);
        }
        if (Input.GetButton("X") && currEnergy > 0 && Time.time > shootTime)
        {
            GameObject tempProjectile = Instantiate(projectile) as GameObject;
            tempProjectile.transform.position = backBarrel.position + (-transform.forward);
            rbM = tempProjectile.GetComponent<Rigidbody>();
            rbM.AddForce(-transform.forward * 100f, ForceMode.VelocityChange);
            shootTime = Time.time + coolDownTime;
            currEnergy--;
            Destroy(tempProjectile, 5f);
        }
    }
}
