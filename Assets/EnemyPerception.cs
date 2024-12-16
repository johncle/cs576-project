using UnityEngine;

public class EnemyPerception : MonoBehaviour
{
    public float sightRange = 15f; // 视野范围
    public float sightAngle = 45f; // 视野角度
    public Transform player; // 玩家位置
    public LayerMask obstaclesLayer; // 障碍物的 Layer，避免视线被阻挡

    void Update()
    {
        // 检查是否在视野范围内
        if (CanSeePlayer())
        {
            // 通知所有敌人
            EnemyManager.Instance.NotifyAllEnemies(player.position);
        }
    }

    bool CanSeePlayer()
    {
        Debug.Log("Checking if enemy can see player...");
        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
       
        Debug.DrawRay(transform.position, directionToPlayer.normalized*sightRange, Color.blue,10,true);
        // 检查玩家是否在视野角度范围内
        if (angle < sightAngle / 2f && Vector3.Distance(transform.position, player.position) <= sightRange)
        {
            // 检查视线是否被阻挡
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer.normalized, out hit, sightRange, obstaclesLayer))
            {
                if (hit.transform == player)
                {
                    Debug.Log("Player detected!");
                    return true; // 玩家在视野内
                }
                else{
                    Debug.Log($"Hit object: {hit.transform.name}, not the player.");
                }
            }
        }
        return false;
    }
}
