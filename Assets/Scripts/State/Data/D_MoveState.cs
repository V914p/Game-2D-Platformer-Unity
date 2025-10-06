using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMoveStateData", menuName = "Data/State Data/Move State")]

public class D_MoveState : ScriptableObject
{
    // Start is called before the first frame update
    public float moveSpeed = 3f;
}
