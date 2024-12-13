using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent _navmeshAgent;
    public bool isStopped { get => _navmeshAgent.remainingDistance <= _navmeshAgent.stoppingDistance; }
    public Transform CurrentTarget { get; set; }
    [SerializeField] private Transform _visualTrm;
    public Vector2 SelfAreaPosition { get; private set; }
    public float selfAreaRadius = 15.0f;

    public EnemyAttack EnemyAttack { get; private set; }
    
    private void Awake()
    {
        EnemyAttack = GetComponent<EnemyAttack>();
        _navmeshAgent = GetComponent<NavMeshAgent>();
        SelfAreaPosition = transform.position;
    }

    public void VisualFlip(bool flip)
    {
        _visualTrm.localScale = new Vector3(flip ? -1 : 1, 1, 1);
    }
    
    public void Move()
    {
        SetDestination(CurrentTarget.position);
    }
    public void SetDestination(Vector3 destination)
    {
        VisualFlip(destination.x < transform.position.x);
        _navmeshAgent.destination = destination;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(SelfAreaPosition, selfAreaRadius);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 10.0f);
        Gizmos.color = Color.white;
    }
}
