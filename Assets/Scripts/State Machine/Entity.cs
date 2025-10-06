using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    private Movement Movement
    {
        get => movement ?? Core.GetCoreComponent(ref movement);
    }
    private Movement movement;

    public D_Entity entityData;
    public FiniteStateMachine stateMachine;
    public Animator anim { get; private set; }
    public AnimationToStatemachine atsm { get; private set; }
    public int lastDamageDirection { get; private set; }
    public Core Core { get; private set; }
    public string defaultAnimation { get; protected set; } = "move"; // Thêm để tùy chỉnh animation

    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private Transform groundCheck;

    private float currentHealth;
    private float currentStunResistance;
    private float lastDamageTime;

    protected bool isDead;
    protected bool isStunned;

    public Vector3 initialPosition { get; private set; }

    public void SetInitialPosition(Vector3 pos)
    {
        initialPosition = pos;
    }

    private Vector2 velocityWorkspace;

    public virtual void Awake()
    {
        initialPosition = transform.position; // Lưu vị trí ban đầu
        Core = GetComponentInChildren<Core>();
        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;

        anim = GetComponent<Animator>();
        atsm = GetComponent<AnimationToStatemachine>();
        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();

        if (Time.time >= lastDamageTime + entityData.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public bool IsDead() => isDead;
    public void SetIsDead(bool value) => isDead = value;

    public bool IsStunned() => isStunned;
    public void SetIsStunned(bool value) => isStunned = value;

    public void SetHealth(float health)
    {
        currentHealth = health;
    }

    public void SetStunResistance(float stunResistance)
    {
        currentStunResistance = stunResistance;
    }

    public void ResetLastDamageTime()
    {
        lastDamageTime = 0f;
    }

    public virtual void ResetState()
    {
        SetIsDead(false);
        SetIsStunned(false);
        SetHealth(entityData.maxHealth);
        SetStunResistance(entityData.stunResistance);
        ResetLastDamageTime();
        StopAllCoroutines();
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }

    public virtual void DamageHop(float velocity)
    {
        velocityWorkspace.Set(Movement.RB.velocity.x, velocity);
        Movement.RB.velocity = velocityWorkspace;
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }

    public virtual void Damage(AttackDetails attackDetails)
    {
        lastDamageTime = Time.time;
        currentHealth -= attackDetails.damageAmount;
        currentStunResistance -= attackDetails.stunDamageAmount;

        DamageHop(entityData.damageHopSpeed);
        Instantiate(entityData.hitParticle, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

        if (attackDetails.position.x > transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }

        if (currentStunResistance <= 0)
        {
            isStunned = true;
        }

        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }

    // Thêm phương thức GetDefaultState
    public abstract State GetDefaultState();

    public virtual void OnDrawGizmos()
    {
        if (Core != null)
        {
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * Movement.FacingDirection * entityData.wallCheckDistance));
            Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minAgroDistance), 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxAgroDistance), 0.2f);
        }
    }
}