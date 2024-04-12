using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator), 
                  typeof(NavMeshAgent),
                  typeof(Detector))]
public abstract class BaseEnemy : MonoBehaviour
{
    protected Animator animator;
    protected NavMeshAgent navAgent;
    protected Detector detector;

    [Header("Base Enemy properties")]
    [SerializeField] protected float speed;

    protected virtual void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        detector = GetComponent<Detector>();
    }

    protected void UpdateAgentDestination(Vector3 destination)
    {
        navAgent.SetDestination(destination);
    }

    protected abstract void Update();
}
