using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AggressiveWeapon : Weapon
{
	private Movement Movement
	{
		get => movement ?? core.GetCoreComponent(ref movement);
	}

	private Movement movement;
	protected SO_AggressiveWeaponData aggressiveWeaponData;

	private List<IDamageable> detectedDamageable = new List<IDamageable>();
	protected override void Awake()
	{
		base.Awake();

		if (weaponData.GetType() == typeof(SO_AggressiveWeaponData))
		{
			aggressiveWeaponData = (SO_AggressiveWeaponData)weaponData;
		}
		else
		{
			Debug.LogError("wrong WEAPON");
		}
	}
	public override void EnterWeapon()
	{
		base.EnterWeapon();
		attackCounter = 0;
	}
	public override void ExitWeapon()
	{
		base.ExitWeapon();
		detectedDamageable.Clear();
	}


	public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
		CheckMeleeAttack();
	}

    private void CheckMeleeAttack()
    {
		WeaponAttackDetails details = aggressiveWeaponData.AttackDetails[attackCounter];
		foreach (IDamageable item in detectedDamageable.ToList())
        {
            item.Damage(details.damageAmount);
        }
    }

    public void AddToDetected(Collider2D collision)
    {
		IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            detectedDamageable.Add(damageable);
        }
	}


    public void RemoveFromDetected(Collider2D collision)
    {
		IDamageable damageable = collision.GetComponent<IDamageable>();

		if (damageable != null)
		{
			detectedDamageable.Remove(damageable);
		}
	}
	
}
