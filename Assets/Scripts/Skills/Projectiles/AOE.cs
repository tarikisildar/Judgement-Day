using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Skills.Projectiles;

public class AOE : Projectile
{
    public float lifeDuration = 2f;
    private float lifeTimer;

    public float damage = 3f;
    void Start()
    {
        lifeTimer = lifeDuration;
        Instantiate(shootParticle, transform);
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer -= Time.deltaTime;
        if(lifeTimer<=0f) {
            Destroy(gameObject);
        }
    }

    protected override void Action(Collider other)
    {
        base.Action(other);
        if(other.gameObject.GetComponent<CharacterStats>() != null && other.gameObject != shooter) {
            other.gameObject.GetComponent<CharacterStats>().health -= damage;
            var particle = Instantiate(hitParticle, other.transform);
            particle.transform.position = other.transform.position;
            Destroy(particle,2);
        }
    }
    
}
