using DefaultNamespace;
using UnityEngine;
using Photon.Pun;

namespace Skills.Projectiles
{
    public class GroundBreakProjectile : Projectile
    {
        public float speed = 20f;
        public float lifeDuration = 3f;
        private float lifeTimer;
        
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
            transform.position += transform.forward * (speed * Time.deltaTime);
            lifeTimer -= Time.deltaTime;
            if(lifeTimer<=0f) {
                if(photonView.IsMine)
                {
                    PhotonNetwork.Destroy(gameObject);
                }
            }
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit, 10))
            {
                var ground = hit.collider.transform.parent.GetComponent<GroundBreak>();
                if (ground && !ground.broke && ground.CompareTag(Constants.GroundBreakTag))
                {
                    ground.Break();
                }
            }
        }
    }
    
    
}