using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{

    // List<Enemy> _enemies;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null )
        {
            Spawner.Instance.RemoveEnemyFromList(enemy);
            Destroy(enemy.gameObject);
        }
    }
}
