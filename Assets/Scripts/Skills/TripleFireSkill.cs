using Skills.Projectiles;
using UnityEngine;

namespace Skills
{
    public class TripleFireSkill : SkillMain
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public override void Action(GameObject player)
        {
            base.Action(player);
            CreateProjectile(transform.forward,player);
            CreateProjectile(transform.forward+transform.right*0.3f,player);
            CreateProjectile(transform.forward-transform.right*0.3f,player);

        }

        private void CreateProjectile(Vector3 forward,GameObject player)
        {
            var projectile = Instantiate(skillData.projectile);
            projectile.transform.position = transform.position + forward*0.8F;
            projectile.transform.forward = forward;
            projectile.GetComponent<Projectile>().shooter = player;
        }
    }
}