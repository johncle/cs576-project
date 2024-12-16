using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    // 存储所有敌人
    public List<EnemyTeamwork> allEnemies = new List<EnemyTeamwork>();

    // 单例模式：方便其他脚本访问这个管理器
    public static EnemyManager Instance;

    void Awake()
    {
        // 确保只有一个 EnemyManager 实例存在
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 添加敌人到管理器
    public void RegisterEnemy(EnemyTeamwork enemy)
    {
        if (!allEnemies.Contains(enemy))
        {
            allEnemies.Add(enemy);
        }
    }

    // 通知所有敌人
    public void NotifyAllEnemies(Vector3 playerPosition)
    {
        foreach (var enemy in allEnemies)
        {
            enemy.OnPlayerDetected(playerPosition);
        }
    }
}
