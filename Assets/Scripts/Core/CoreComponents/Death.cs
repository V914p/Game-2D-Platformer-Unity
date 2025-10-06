using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : CoreComponent
{
    [SerializeField] private GameObject[] deathParticles;

    private Stats Stats => stats ? stats : core.GetCoreComponent(ref stats);
    private Stats stats;
    private ParticlesManager ParticlesManager =>
        particleManager ? particleManager : core.GetCoreComponent(ref particleManager);

    private ParticlesManager particleManager;

    
    public void Die()
    {
        Debug.Log("Die() called from Death.cs");

        foreach (var particle in deathParticles)
        {
            ParticlesManager.StartParticles(particle);
        }

        core.transform.parent.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        if (core == null)
        {
            Debug.LogError(gameObject.name + " has no core component!");
        }
        if (Stats == null)
        {
            Debug.LogError(gameObject.name + " has no Stats component!");
        }
        else
        {
            Stats.OnHealthZero += Die;
        }
    }

    private void OnDisable()
    {
        if (Stats != null)
            Stats.OnHealthZero -= Die;
    }
}
