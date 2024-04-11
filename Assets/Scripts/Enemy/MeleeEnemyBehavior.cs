using UnityEngine;

public class MeleeEnemyBehavior : BaseEnemy
{
    [Header("Melee Enemy properties")]
    [SerializeField] private float timeToWait;
    [SerializeField] private Transform[] movePoints;
    [SerializeField] private float chaseSpeed = 5;

    private float walkCooldownTimer;
    private bool arrived;

    protected override void Awake()
    {
        base.Awake();
        navAgent.speed = speed;

        UpdateAgentDestination(SortMovePoint());
    }

    protected override void Update()
    {
        animator.SetFloat("speed", navAgent.velocity.magnitude);
        if (detector.isInDetectArea())
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
        Vector3 playerPosition = detector.GetCollidersInDetectArea()[0].
            GetComponent<Transform>().position;

        UpdateAgentDestination(playerPosition);
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
}
