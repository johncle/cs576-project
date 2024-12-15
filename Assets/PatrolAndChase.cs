using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAndChase : MonoBehaviour
{
    public Transform player;
    public Transform[] waypoints;
    public float detectionRadius = 5f;
    public float stopRadius = 8f;
    public float fieldOfViewAngle = 100f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;

    private NavMeshAgent agent;
    private int waypointIndex = 0;
    private Vector3 target;
    private bool isChasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;
        UpdateDestination();
    }

    void Update()
    {
        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
            CheckForPlayer();
        }
    }

    void Patrol()
    {
        if (Vector3.Distance(transform.position, target) < 1f)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
    }

    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
        Debug.Log("New destination: " + waypointIndex);
    }

    void IterateWaypointIndex()
    {
        waypointIndex++;
        if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
    }

    void CheckForPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if (angleToPlayer <= fieldOfViewAngle / 2)
            {
                isChasing = true;
                agent.speed = chaseSpeed;
            }
        }
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (distanceToPlayer > stopRadius) //if (distanceToPlayer > stopRadius || angleToPlayer > fieldOfViewAngle / 2)
        {
            isChasing = false;
            agent.speed = patrolSpeed;
            UpdateDestination();
        }
    }

    // Visualize the detection radius and FOV in the Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.yellow;
        Vector3 leftBoundary = Quaternion.Euler(0, -fieldOfViewAngle / 2, 0) * transform.forward * detectionRadius;
        Vector3 rightBoundary = Quaternion.Euler(0, fieldOfViewAngle / 2, 0) * transform.forward * detectionRadius;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }
}
