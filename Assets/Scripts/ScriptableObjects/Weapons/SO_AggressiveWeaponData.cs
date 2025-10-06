using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newAggressiveWeaponData", menuName = "Data/Weapon Data/ Aggessive Weapon")]

public class SO_AggressiveWeaponData : SO_WeaponData
{
    [SerializeField] private WeaponAttackDetails[] attackDetails;


	public WeaponAttackDetails[] AttackDetails { get => attackDetails; private set => attackDetails = value; }

	private void OnEnable()
	{
		amountOfAttacks = attackDetails.Length;
		movemenSpeed = new float[amountOfAttacks];
		for (int i = 0; i < amountOfAttacks; i++)
		{
			movemenSpeed[i] = attackDetails[i].movementSpeed;
		}
	}
}
