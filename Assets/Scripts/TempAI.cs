using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
public class TempAI : MonoBehaviour
{
    public GameObject target;
    public GameObject bullet;
    public GameObject throwFrom;
    float timer = 0f;
    float waitDuration = 2f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(target == null) {
            target = SurroundingsManager.Instance.mainPlayer;
        }
        else {
            if(timer>waitDuration) {
                Shoot();
                timer = 0f;
            }
            Quaternion lookOnLook = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime);
        }
        
    }

    void Shoot()
    {
        var projectile = Instantiate(bullet);
        projectile.transform.position = throwFrom.transform.position + throwFrom.transform.forward*0.3F;
        projectile.transform.forward = throwFrom.transform.forward;
    }
}
