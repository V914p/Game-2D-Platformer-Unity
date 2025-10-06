using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Weapon")]
public class SO_WeaponData : ScriptableObject
{
    [Header("Combo Settings")]
    public int amountOfAttacks = 1;       // số combo (vd: 2 hit)

    [Header("Movement During Attack")]
    public float[] movemenSpeed;          // tốc độ di chuyển theo từng hit

    [Header("Damage Settings")]
    public int[] attackDamage;            // damage của từng hit

    [Header("Attack Range")]
    public float attackRadius = 0.5f;     // bán kính chém
    public LayerMask whatIsEnemy;         // layer Enemy
}
