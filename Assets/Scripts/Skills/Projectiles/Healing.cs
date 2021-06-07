namespace Skills.Projectiles
{
    public class Healing : Projectile
    {
        public float power = 10f;

        private void Start()
        {
            var particle = Instantiate(shootParticle, transform);
            particle.transform.position = transform.position;
        }
    }
}