using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifeDuration = 3f;
    private float lifeTimer;

    void Start() 
    {
        lifeTimer = lifeDuration;
    }

    void Update() 
    {
        lifeTimer -= Time.deltaTime;
        if(lifeTimer<=0f) {
            Destroy(gameObject);
        }

    }
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Projectile") {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
    
}
