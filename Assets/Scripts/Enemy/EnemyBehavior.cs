using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    private NavMeshAgent navAgent;

    [SerializeField] private float timeToWait;
    [SerializeField] private Transform[] movePoints;


    private float elapsedTime;
    private bool arrived;

    private void Awake()
    {
        navAgent = GetComponentInChildren<NavMeshAgent>();

        UpdateAgentDestination();
    }

    private void Update()
    {
        arrived = navAgent.remainingDistance <= 0.5f;

        if (arrived)
        {
            elapsedTime += Time.deltaTime;
        }

        if (elapsedTime >= timeToWait)
        {
            elapsedTime = 0;
            UpdateAgentDestination();
        }
    }

    private void UpdateAgentDestination()
    {
        navAgent.SetDestination(SortMovePoint());
    }

    private Vector3 SortMovePoint()
    {
        int sortedIndex = Random.Range(0, movePoints.Length);        
        return movePoints[sortedIndex].position;
    }
}
