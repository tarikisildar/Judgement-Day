using System;
using UnityEngine;

namespace Skills.Projectiles
{
    public class Projectile : MonoBehaviour
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