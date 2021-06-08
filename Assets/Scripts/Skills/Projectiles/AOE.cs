using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Skills.Projectiles;
using Photon.Pun;

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
        if(photonView.IsMine)
        {
            if (lifeTimer <= 0f)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    protected override void Action(Collider other)
    {
        base.Action(other);
        if(other.gameObject.GetComponent<CharacterStats>() != null && other.gameObject != shooter)
        {
            var rand = Random.Range(0f, 1f);
            bool isCrit =  rand< shooter.GetComponent<CharacterStats>().critChance;
            if(photonView.IsMine)
            {
                int givenDamage = damage + Random.Range(-1, 2);
                int shooterId = shooter.GetComponent<PhotonView>().ViewID;
                other.gameObject.GetComponent<PhotonView>().RPC("TakeDamageFromClient", RpcTarget.AllViaServer, givenDamage,shooterId, isCrit);
            }
            
            //other.gameObject.GetComponent<CharacterStats>().TakeDamageFromClient(damage+ Random.Range(-1,2),isCrit);
            var particle = Instantiate(hitParticle, other.transform);
            particle.transform.position = other.transform.position;
            StartCoroutine(DestroyParticle(particle));
        }
    }

    IEnumerator DestroyParticle(GameObject particle)
    {
        yield return new WaitForSeconds(2f);
        Destroy(particle, 2);
    }
    
}
