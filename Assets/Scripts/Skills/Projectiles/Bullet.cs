using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using DG.Tweening;
using Skills.Projectiles;
using Random = System.Random;
using Photon.Pun;

public class Bullet : Projectile
{
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
        if(photonView.IsMine)
        {
            if (lifeTimer <= 0f)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
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
            if(photonView.IsMine)
            {
                other.gameObject.GetComponent<PhotonView>().RPC("TakeDamageFromClient", RpcTarget.All, damage + UnityEngine.Random.Range(-1, 2), isCrit);
                other.gameObject.GetComponent<PhotonView>().RPC("KnockBack", RpcTarget.AllViaServer, other.gameObject.GetComponent<PhotonView>().ViewID, transform.forward, power);
                //other.gameObject.GetComponent<CharacterStats>().TakeDamageFromClient(damage + UnityEngine.Random.Range(-1, 2), isCrit);
            }            
            if(photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }

        
        var normal = other.contacts[0].normal * 100f;
        normal.y = 0;
        transform.LookAt(transform.position + normal );
    }


}
