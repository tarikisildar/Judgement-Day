using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using DG.Tweening;
using Skills.Projectiles;
using Random = System.Random;

public class Bullet : Projectile
{
    // Start is called before the first frame update
    public float speed = 40f;
    public float lifeDuration = 3f;
    public int damage = 3;
    private float lifeTimer;
    public float power = 10f;
    void Start()
    {
        lifeTimer = lifeDuration;
        var projectile = Instantiate(shootParticle, transform.parent);
        projectile.transform.position = shooter.transform.position;
        projectile.transform.LookAt(shooter.transform.position + shooter.transform.forward*100f);
        Destroy(projectile,2);
    }



    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        lifeTimer -= Time.deltaTime;
        if(lifeTimer<=0f) {
            Destroy(gameObject);
        }
    }

    void KnockBack(Collision other)
    {
        Debug.Log("KNOCKBACKED");
        other.transform.GetComponent<Rigidbody>().AddForce(power * transform.forward );
        //other.gameObject.transform.DOMove(other.transform.position + transform.forward , 1f);
    }

    protected override void Action(Collision other)
    {
        base.Action(other);
        
        var particle = Instantiate(hitParticle,transform.parent);
        var hitpoint = other.contacts[0].point;
        particle.transform.position = hitpoint;
        particle.transform.LookAt(hitpoint + other.contacts[0].normal * 100f);
        
        Destroy(particle,3);

        var collidedSound = other.gameObject.GetComponent<HitSound>();
        AudioSource.PlayClipAtPoint(collidedSound ? collidedSound.hitSound : hitSound, transform.position);

        if(other.gameObject.GetComponent<CharacterStats>() != null)
        {

            var rand = UnityEngine.Random.Range(0f, 1f);
            bool isCrit = rand < shooter.GetComponent<CharacterStats>().critChance;
        
            other.gameObject.GetComponent<CharacterStats>().TakeDamage(damage + UnityEngine.Random.Range(-1,2),isCrit);
            KnockBack(other);
            Destroy(gameObject);

        }

        
        
        var normal = other.contacts[0].normal * 100f;
        normal.y = 0;
        transform.LookAt(transform.position + normal );
    }


}
