using UnityEngine;

public class DashHitbox : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy == null)
            return;

        Spawner.Instance.RemoveEnemyFromList(enemy);

        DashGauge.Instance.AddGauge(Player.Instance.GaugeReward);

        Spawner.Instance.RemoveEnemyFromList(enemy);

        Destroy(enemy.gameObject);
    }
}

/*public class DashHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy == null)
            return;

        Spawner.Instance.RemoveEnemyFromList(enemy);

        Destroy(enemy.gameObject);

        DashGauge.Instance.AddGauge(5f);
    }
}*/