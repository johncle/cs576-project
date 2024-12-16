using UnityEngine;
using UnityEngine.AI; // 引入NavMesh的命名空间

public class EnemyTeamwork : MonoBehaviour
{
    public float alertRange = 20f;  // 敌人被警告的范围
    public Transform player;        // 玩家对象
    private bool isChasing = false; // 是否正在追击玩家
    private NavMeshAgent navAgent;  // 引用NavMeshAgent组件

    void Start()
    {
        // 获取NavMeshAgent组件
        navAgent = GetComponent<NavMeshAgent>();

        // 注册到敌人管理器
        EnemyManager.Instance.RegisterEnemy(this);
    }

    void Update()
    {
        if (isChasing)
        {
            // 启动追击逻辑
            ChasePlayer();
        }
    }

    // 玩家被敌人发现时的行为
    public void OnPlayerDetected(Vector3 playerPosition)
    {
        // 如果玩家在视野范围内且没有处于追击状态，则开始追击
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
