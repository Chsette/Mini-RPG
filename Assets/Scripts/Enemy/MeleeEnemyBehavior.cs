using UnityEngine;

public class MeleeEnemyBehavior : BaseEnemy
{
    [Header("Melee Enemy basic properties")]
    [SerializeField] private float timeToWait;
    [SerializeField] private Transform[] movePoints;
    [SerializeField] private float chaseSpeed = 5;

    [Header("Melee Attack properties")]
    [SerializeField] private Vector3 attackPositionOffset;
    [SerializeField] private Vector3 attackRange;

    private float walkCooldownTimer;
    private bool arrived;
    private bool canAttack;
    private bool canChase;

    protected override void Awake()
    {
        base.Awake();
        navAgent.speed = speed;

        UpdateAgentDestination(SortMovePoint());
    }

    protected override void Update()
    {
        animator.SetFloat("speed", navAgent.velocity.magnitude);
        if(!canAttack) canChase = detector.isInDetectArea();
        if (canChase)
        {
            ChasePlayer();
        }
        else
        {
            HandlePatrol();
        }
    }

    private void ChasePlayer()
    {
        navAgent.speed = chaseSpeed;

        Vector3 playerPosition = detector.GetCollidersInDetectAreaSphere()[0].
            GetComponent<Transform>().position;

        Collider[] detectedPlayers =
            detector.
            GetCollidersInDetectAreaBox(transform.position + attackPositionOffset,
                                        attackRange);

        bool isPlayerDetected = detectedPlayers.Length > 0;
        if (isPlayerDetected)
        {
            canAttack = true;
            AttackPlayer();
        }
        UpdateAgentDestination(playerPosition);
    }

    private void AttackPlayer()
    {
        canChase = false;
        navAgent.speed = 0;
        animator.SetTrigger("attack");
    }

    private void HandlePatrol()
    {
        navAgent.speed = base.speed;
        arrived = navAgent.remainingDistance <= 0.5f;

        if (arrived)
        {
            walkCooldownTimer += Time.deltaTime;
        }

        if (walkCooldownTimer >= timeToWait)
        {
            walkCooldownTimer = 0;
            UpdateAgentDestination(SortMovePoint());
        }
    }

    private Vector3 SortMovePoint()
    {
        int sortedIndex = Random.Range(0, movePoints.Length);
        return movePoints[sortedIndex].position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position + attackPositionOffset, attackRange);
    }
}
