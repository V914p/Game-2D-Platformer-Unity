using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity
{
    public E2_MoveState moveState { get; private set; }
    public E2_IdleState idleState { get; private set; }

    public E2_PlayerDetectedState playerDetectedState { get; private set; }
    public E2_LookForPlayerState lookForPlayerState { get; private set; }

    public E2_DeadState deadState { get; private set; }
    public E2_RangedAttackState rangedAttackState { get; private set; }

    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_PlayerDetected playerDetectedStateData;
    [SerializeField] private D_LookForPlayer lookForPlayerStateData;
    [SerializeField] private D_DeadState deadStateData;
    [SerializeField] private D_RangeAttackState rangedAttackStateData;
    [SerializeField] private Transform rangedAttackPosition;

    public override void Awake()
    {
        base.Awake();
        moveState = new E2_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E2_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E2_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        lookForPlayerState = new E2_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        deadState = new E2_DeadState(this, stateMachine, "dead", deadStateData, this);
        rangedAttackState = new E2_RangedAttackState(this, stateMachine, "rangedAttack", rangedAttackPosition, rangedAttackStateData, this);
    }

    public override State GetDefaultState()
    {
        return moveState;
    }

    public override void ResetState()
    {
        base.ResetState();
        stateMachine.ChangeState(GetDefaultState());
    }

    private void Start()
    {
        stateMachine.Initialize(GetDefaultState());
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);
        if (isDead)
        {
            stateMachine.ChangeState(deadState);
        }
        else if (CheckPlayerInMinAgroRange())
        {
            stateMachine.ChangeState(rangedAttackState);
        }
        else
        {
            lookForPlayerState.SetTurnImmediately(true);
            stateMachine.ChangeState(lookForPlayerState);
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}
