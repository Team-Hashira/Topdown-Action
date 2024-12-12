using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent _navmeshAgent;
    public bool isStopped { get => _navmeshAgent.isStopped; }
    public Transform CurrentTarget { get; set; }
    
    private void Awake()
    {
        _navmeshAgent = GetComponent<NavMeshAgent>();
    }

    public void Move()
    {
        SetDestination(CurrentTarget.position);
    }
    
    public void SetDestination(Vector3 destination)
    {
        _navmeshAgent.destination = destination;
    }
}
