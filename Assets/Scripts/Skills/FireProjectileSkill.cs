namespace Skills
{
    public class FireProjectileSkill : SkillMain
    {
        protected override void Awake()
        {
            skillDataName = "FireProjectile";
            base.Awake();
        }

        public override void Action()
        {
            base.Action();
            var projectile = Instantiate(skillData.projectile);
            projectile.transform.position = transform.position + transform.forward*0.8F;
            projectile.transform.forward = transform.forward;
        }
    }
}