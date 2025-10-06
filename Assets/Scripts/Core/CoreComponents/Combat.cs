using UnityEngine;

public class Combat : CoreComponent, IDamageable
{
    private Movement movement;
    private CollisionSenses collisionSenses;
    private Stats stats;
    private ParticlesManager particlesManager;

    private Movement Movement => movement ?? core.GetCoreComponent(ref movement);
    private CollisionSenses CollisionSenses => collisionSenses ?? core.GetCoreComponent(ref collisionSenses);
    private Stats Stats => stats ?? core.GetCoreComponent(ref stats);
    private ParticlesManager ParticlesManager => particlesManager ?? core.GetCoreComponent(ref particlesManager);

    [SerializeField] private GameObject damageParticles;

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public void Damage(float amount)
    {
        Debug.Log(core.transform.parent.name + " Damaged! -" + amount);
        Stats.DecreaseHealth(amount);

        if (damageParticles != null)
            ParticlesManager.StartParticlesWithRandomRotation(damageParticles);
    }
}
