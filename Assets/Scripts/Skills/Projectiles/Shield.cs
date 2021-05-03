using System.Collections;
using System.Collections.Generic;
using Skills.Projectiles;
using UnityEngine;

public class Shield : Projectile
{
    // Start is called before the first frame update
    public float lifeDuration = 3f;
    private float lifeTimer;

    void Start() 
    {
        lifeTimer = lifeDuration;

        Instantiate(shootParticle, transform);
    }

    void Update() 
    {
        lifeTimer -= Time.deltaTime;
        if(lifeTimer<=0f) {
            Destroy(gameObject);
        }

    }
    protected override void Action(Collision other) {
        if(other.gameObject.CompareTag(Constants.ProjectileTag))
        {
            Instantiate(hitParticle, transform);
            hitParticle.transform.position = other.contacts[0].point;
            hitParticle.transform.LookAt(other.contacts[0].normal*100 + transform.position);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
    
}
