using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MimicSpace;

[RequireComponent(typeof(NavMeshAgent))]
public class MimicController : MonoBehaviour
{
    ///////////////////////// Public Variables ////////////////////////////
    [Header("Move to Target")] 
    public Transform activeTarget; // Target the mimic will move towards
    public string InitialTargetName = "Monument";
    
    [Header("Target Detection Settings")]
    public float detectionDistance = 50f; // Distance the raycast will check for buildings
    public LayerMask detectionLayer; // Only detect objects within this layer
    public string targetTag = "Building"; // Tag to filter detection
    
    [Header("Attack Settings")]
    public string attackTargetTag = "Building"; // Tag to filter attackable objects
    public GameObject auraSphere; // Visual representation of the attack's area of effect
    public GameObject attackSphere; // Visual representation of the attack's area of effect
    //public float attackDelay = 1f; // Delay before the enemy starts attacking
    public float attackRange = 5f; // Range within which the enemy can attack the building
    public int attackDamage = 10; // Damage dealt to the buildings
    public float attackTime = 3f; // Delay before dealing damage
    public float AOEExpandRate = 5f; // Rate at which the attack spheres grow per second of the attackTime
    
    ///////////////////////// Private Variables //////////////////////////
    private NavMeshAgent agent;
    private Mimic myMimic;
    private Vector3 currentVelocity = Vector3.zero; 
    private bool isAttacking = false;


    ///////////////////////// Unity Methods /////////////////////////////
    void Start()
    {
        myMimic = GetComponent<Mimic>();
        agent = GetComponent<NavMeshAgent>();
        
        // Set the initial target
        activeTarget = GameObject.Find(InitialTargetName).transform;
    }

    void Update()
    {
        UpdateMovement();
        DetectBuilding();
        TryAttack();
    }

    ///////////////////////// Control Methods //////////////////////////

    private void UpdateMovement()
    {
        MoveToTarget();
    }
    
    /// <summary>
    /// Move the mimic towards the target
    /// </summary>

    private void MoveToTarget()
    {
        if (activeTarget == null) return; // If target is null, do nothing

        agent.SetDestination(activeTarget.position);

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.isStopped = true;
            currentVelocity = Vector3.zero; // Stop mimic movement
        }
        else
        {
            agent.isStopped = false;
            currentVelocity = agent.velocity; // Directly match the agent's velocity
        }

        myMimic.velocity = currentVelocity; // Assign smoothed velocity to the mimic for leg placement
    }
    
    /// <summary>
    /// Detect potential target buildings in front of the mimic
    /// </summary>
    
    void DetectBuilding()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionDistance, detectionLayer))
        {
            if (hit.collider.CompareTag(targetTag))
            {
                ChangeTarget(hit.collider.transform);
            }
        }
    }
    
    /// <summary>
    /// Change the target the mimic is moving towards
    /// </summary>

    public void ChangeTarget(Transform newTarget)
    {
        activeTarget = newTarget;
    }
    
    /// <summary>
    /// Try to attack the target building which includes preparing the attack and executing it
    /// </summary>

    void TryAttack()
    {
        if (!isAttacking && Vector3.Distance(transform.position, activeTarget.position) <= attackRange)
        {
            StartCoroutine(PrepareAttack());
        }
    }
    
    /// <summary>
    /// Prepare the attack by scaling the attack spheres
    /// </summary>

    IEnumerator PrepareAttack()
    {
       
        isAttacking = true;
        float startTime = Time.time;

        while (Time.time - startTime < attackTime)
        {
            // Increase the scale by growthRate every second
            auraSphere.transform.localScale += new Vector3(AOEExpandRate, AOEExpandRate, AOEExpandRate) * Time.deltaTime;
            attackSphere.transform.localScale += new Vector3(AOEExpandRate, AOEExpandRate, AOEExpandRate) * Time.deltaTime;
        
            yield return null; // Wait until the next frame
        }
    
        ExecuteAttack();
        Destroy(gameObject); // Enemy disappears after attacking
    }
    
    /// <summary>
    /// Execute the attack on the target building
    /// </summary>

    void ExecuteAttack()
    {
        // Perform real-time detection of buildings only at the point of attack execution
        float actualRadius = attackSphere.transform.localScale.x / 4;
        Collider[] hitColliders = Physics.OverlapSphere(attackSphere.transform.position, actualRadius);
        
        foreach (Collider hit in hitColliders)
        {
            if (hit.CompareTag(attackTargetTag))
            {
                HealthSystem_Building building = hit.GetComponent<HealthSystem_Building>();

                if (building != null) building.Damage(attackDamage);
            }
        }
    }

    ///////////////////////// Editor Methods //////////////////////////
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * detectionDistance);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
#endif
}

