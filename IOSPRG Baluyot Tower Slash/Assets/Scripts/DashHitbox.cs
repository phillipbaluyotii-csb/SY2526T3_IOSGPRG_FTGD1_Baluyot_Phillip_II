using UnityEngine;

public class DashHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
        {
            Spawner.Instance.RemoveEnemyFromList(enemy);

            Destroy(enemy.gameObject);
        }
    }
}