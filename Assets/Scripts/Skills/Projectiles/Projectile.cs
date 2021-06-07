using System;
using UnityEngine;
using Photon.Pun;

namespace Skills.Projectiles
{
    public class Projectile : MonoBehaviourPun
    {
        [SerializeField] protected GameObject shootParticle;
        [SerializeField] protected GameObject hitParticle;
        [SerializeField] protected AudioClip hitSound;
        public GameObject shooter;
      

        protected void OnTriggerEnter(Collider other)
        {
            Action(other);
        }
        protected void OnCollisionEnter(Collision other)
        {
            Action(other);
        }

        protected virtual void Action(Collider other)
        {
            
        }

        protected virtual void Action(Collision other)
        {
            
        }
    }
}