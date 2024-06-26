using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class PatrollingNPC : MonoBehaviour
{
    public List<Transform> patrolPoints; // List of patrol points
    public float stoppingDistance = 1f; // Distance to consider NPC has reached the destination

    private NavMeshAgent navMeshAgent;
    private int currentTargetIndex = 0;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (patrolPoints != null && patrolPoints.Count > 0)
        {
            navMeshAgent.SetDestination(patrolPoints[currentTargetIndex].position);
        }
    }

    void Update()
    {
        if (patrolPoints == null || patrolPoints.Count == 0)
            return;

        // Check if NPC has reached the destination
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= stoppingDistance)
        {
            SwitchTarget();
            navMeshAgent.SetDestination(patrolPoints[currentTargetIndex].position);
        }
    }

    void SwitchTarget()
    {
        currentTargetIndex = (currentTargetIndex + 1) % patrolPoints.Count;
    }
}