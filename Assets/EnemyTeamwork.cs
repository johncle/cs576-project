using UnityEngine;
using UnityEngine.AI; 

public class EnemyTeamwork : MonoBehaviour
{
    public float alertRange = 20f;  
    public Transform player;        
    private bool isChasing = false; 
    private NavMeshAgent navAgent;  

    void Start()
    {
        
        navAgent = GetComponent<NavMeshAgent>();

        
        EnemyManager.Instance.RegisterEnemy(this);
    }

    void Update()
    {
        if (isChasing)
        {
            
            ChasePlayer();
        }
    }

    
    public void OnPlayerDetected(Vector3 playerPosition)
    {
        
        if (Vector3.Distance(transform.position, playerPosition) <= alertRange && !isChasing)
        {
            isChasing = true;
            Debug.Log($"{gameObject.name} has detected the player and is now chasing!");
        }
    }

    void ChasePlayer()
    {
        navAgent.SetDestination(player.position);
        navAgent.speed = 5f;
    }
}
