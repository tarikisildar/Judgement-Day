using System;

namespace Skills.Projectiles
{
    public class Teleport : Projectile
    {
        private void Start()
        {
            var particle = Instantiate(shootParticle, transform);
            particle.transform.position = transform.position;
        }
    }
}