using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Skills.Projectiles;

public class AOE : Projectile
{
    public float lifeDuration = 2f;
    private float lifeTimer;

    public int damage = 3;
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
        if(other.gameObject.GetComponent<CharacterStats>() != null && other.gameObject != shooter)
        {
            var rand = Random.Range(0f, 1f);
            bool isCrit =  rand< shooter.GetComponent<CharacterStats>().critChance;
            other.gameObject.GetComponent<CharacterStats>().TakeDamage(damage+ Random.Range(-1,2),isCrit);
            var particle = Instantiate(hitParticle, other.transform);
            particle.transform.position = other.transform.position;
            Destroy(particle,2);
        }
    }
    
}
