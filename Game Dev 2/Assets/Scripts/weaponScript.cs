using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    none,
    single,
    spread,
}

[System.Serializable]
public class WeaponDefinition
{
    public WeaponType type = WeaponType.none;
    public float velocity = 0;
    public float damage = 0;
    public GameObject projectilePrefab;
    //public Transform barrell;
}

public class weaponScript : MonoBehaviour {

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
    public WeaponType _type = WeaponType.none;
    public int playerNum;

    WeaponDefinition def;

    private float shootTime = 0f;
    private float checkPointTime = 0f;
    private string Horizontal;
    private string Triangle;
    private string Square;
    private string Circle;
    private string X;

	// Use this for initialization
	void Start () {
        SetType(_type);

        Horizontal = "Horizontal P" + playerNum.ToString();
        Triangle = "Triangle P" + playerNum.ToString();
        Square = "Square P" + playerNum.ToString();
        Circle = "Circle P" + playerNum.ToString();
        X = "X P" + playerNum.ToString();
    }

    public WeaponType type
    {
        get { return (_type);  }
        set { SetType(value);  }
    }

    public void SetType(WeaponType wt)
    {
        _type = wt;
        if(type == WeaponType.none)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
        def = main.GetWeaponDefinition(_type);
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        float leftStickX = Input.GetAxis(Horizontal);

        if (Input.GetButton(Triangle) && currEnergy > 0 && Time.time > shootTime)
        {
            switch (_type)
            {
                case WeaponType.single:
                    GameObject tempProjectile = Instantiate(def.projectilePrefab) as GameObject;
                    Physics.IgnoreCollision(tempProjectile.GetComponent<BoxCollider>(), player.GetComponent<BoxCollider>());
                    Physics.IgnoreCollision(tempProjectile.GetComponent<BoxCollider>(), player.GetComponentInChildren<SphereCollider>());
                    tempProjectile.transform.position = frontBarrelM.position + (transform.forward);
                    Rigidbody rb = tempProjectile.GetComponent<Rigidbody>();
                    rb.AddForce(frontBarrelM.forward * def.velocity, ForceMode.VelocityChange);
                    Destroy(tempProjectile, 5f);
                    break;

                case WeaponType.spread:
                    GameObject tempProjectileM = Instantiate(def.projectilePrefab) as GameObject;
                    Physics.IgnoreCollision(tempProjectileM.GetComponent<BoxCollider>(), player.GetComponent<BoxCollider>());
                    Physics.IgnoreCollision(tempProjectileM.GetComponent<BoxCollider>(), player.GetComponentInChildren<SphereCollider>());
                    tempProjectileM.transform.position = frontBarrelM.position + (frontBarrelM.forward);
                    Rigidbody rbM = tempProjectileM.GetComponent<Rigidbody>();
                    rbM.AddForce(frontBarrelM.forward * def.velocity, ForceMode.VelocityChange);
                    Destroy(tempProjectileM, 5f);

                    GameObject tempProjectileL = Instantiate(def.projectilePrefab) as GameObject;
                    Physics.IgnoreCollision(tempProjectileL.GetComponent<BoxCollider>(), player.GetComponent<BoxCollider>());
                    Physics.IgnoreCollision(tempProjectileL.GetComponent<BoxCollider>(), player.GetComponentInChildren<SphereCollider>());
                    tempProjectileL.transform.position = frontBarrelL.position + (frontBarrelL.forward);
                    Rigidbody rbL = tempProjectileL.GetComponent<Rigidbody>();
                    rbL.AddForce(frontBarrelL.forward * def.velocity, ForceMode.VelocityChange);
                    Destroy(tempProjectileL, 5f);

                    GameObject tempProjectileR = Instantiate(def.projectilePrefab) as GameObject;
                    Physics.IgnoreCollision(tempProjectileR.GetComponent<BoxCollider>(), player.GetComponent<BoxCollider>());
                    Physics.IgnoreCollision(tempProjectileR.GetComponent<BoxCollider>(), player.GetComponentInChildren<SphereCollider>());
                    tempProjectileR.transform.position = frontBarrelR.position + (frontBarrelR.forward);
                    Rigidbody rbR = tempProjectileR.GetComponent<Rigidbody>();
                    rbR.AddForce(frontBarrelR.forward * def.velocity, ForceMode.VelocityChange);
                    Destroy(tempProjectileR, 5f);

                    break;
            }
            shootTime = Time.time + coolDownTime;
            currEnergy--;
        }
        if (Input.GetButton(Circle) && currEnergy > 0 && Time.time > shootTime)
        {
            GameObject tempProjectile = Instantiate(def.projectilePrefab) as GameObject;
            Physics.IgnoreCollision(tempProjectile.GetComponent<BoxCollider>(), player.GetComponent<BoxCollider>());
            Physics.IgnoreCollision(tempProjectile.GetComponent<BoxCollider>(), player.GetComponentInChildren<SphereCollider>());
            tempProjectile.transform.position = sideBarrelR.position + (transform.right);
            Rigidbody rbM = tempProjectile.GetComponent<Rigidbody>();
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
        if (Input.GetButton(Square) && currEnergy > 0 && Time.time > shootTime)
        {
            GameObject tempProjectile = Instantiate(def.projectilePrefab) as GameObject;
            Physics.IgnoreCollision(tempProjectile.GetComponent<BoxCollider>(), player.GetComponent<BoxCollider>());
            Physics.IgnoreCollision(tempProjectile.GetComponent<BoxCollider>(), player.GetComponentInChildren<SphereCollider>());
            tempProjectile.transform.position = sideBarrelL.position + (-transform.right);
            Rigidbody rbM = tempProjectile.GetComponent<Rigidbody>();
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
        if (Input.GetButton(X) && currEnergy > 0 && Time.time > shootTime)
        {
            GameObject tempProjectile = Instantiate(def.projectilePrefab) as GameObject;
            Physics.IgnoreCollision(tempProjectile.GetComponent<BoxCollider>(), player.GetComponent<BoxCollider>());
            Physics.IgnoreCollision(tempProjectile.GetComponent<BoxCollider>(), player.GetComponentInChildren<SphereCollider>());
            tempProjectile.transform.position = backBarrel.position + (-transform.forward);
            Rigidbody rbM = tempProjectile.GetComponent<Rigidbody>();
            rbM.AddForce(-transform.forward * 100f, ForceMode.VelocityChange);
            shootTime = Time.time + coolDownTime;
            currEnergy--;
            Destroy(tempProjectile, 5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CheckPoint")
        {
            if(Time.time > shootTime)
            {
                currEnergy++;
                shootTime = Time.time + coolDownTime;
            }
        }
    }
}
