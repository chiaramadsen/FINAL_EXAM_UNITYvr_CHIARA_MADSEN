using UnityEngine;
using UnityEngine.AI;

public class WanderingNPC : MonoBehaviour
{
    public float wanderRadius = 10f; // Radius within which the NPC will wander
    public float stoppingDistance = 1f; // Distance to consider NPC has reached the destination

    private NavMeshAgent navMeshAgent;
    private Vector3 currentDestination;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetNewDestination();
    }

    void Update()
    {
        // Check if NPC has reached the destination
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= stoppingDistance)
        {
            SetNewDestination();
        }
    }

    void SetNewDestination()
    {
        currentDestination = GetRandomPoint(transform.position, wanderRadius);
        navMeshAgent.SetDestination(currentDestination);
    }

    Vector3 GetRandomPoint(Vector3 center, float radius)
    {
        Vector3 randomPos = Random.insideUnitSphere * radius;
        randomPos += center;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPos, out hit, radius, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return center; // If no valid point found, return the center
    }
}