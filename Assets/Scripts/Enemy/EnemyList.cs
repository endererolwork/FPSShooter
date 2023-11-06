using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    public List<GameObject> enemys = new List<GameObject>();
    public GameObject enemyPrefab;
    public void RespawnEnemyToList()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemys.Add(newEnemy);
    }
    
    
}
