using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent _navmeshAgent;
    public bool isStopped { get => _navmeshAgent.remainingDistance <= _navmeshAgent.stoppingDistance; }
    public Transform CurrentTarget { get; set; }

    public Vector2 SelfAreaPosition { get; private set; }
    public float selfAreaRadius = 15.0f;
    
    private void Awake()
    {
        _navmeshAgent = GetComponent<NavMeshAgent>();
        SelfAreaPosition = transform.position;
    }

    public void Move()
    {
        SetDestination(CurrentTarget.position);
    }
    
    public void SetDestination(Vector3 destination)
    {
        _navmeshAgent.destination = destination;
    }

    public void Attack()
    {
        StartCoroutine(CoroutineAttack());
    }

    private IEnumerator CoroutineAttack()
    {
        yield return new WaitForSeconds(1.5f);
    }
}
