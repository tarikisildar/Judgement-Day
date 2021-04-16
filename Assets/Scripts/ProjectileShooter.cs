using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    public float fireRate = 1f;
    float nextFire;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && Time.time > nextFire) {
            Fire();
            nextFire = Time.time + fireRate;
        }
    }

    public void Fire()
    {
        GameObject projectile = Instantiate(bullet);
        projectile.transform.position = transform.position + transform.forward;
        projectile.transform.forward = transform.forward;
    }
}
