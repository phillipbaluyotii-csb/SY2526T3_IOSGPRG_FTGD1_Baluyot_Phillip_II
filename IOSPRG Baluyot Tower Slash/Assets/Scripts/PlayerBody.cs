/*using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
        {
            _player.TakeDamage();
        }
    }
}*/

using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy == null)
            return;

        Player player = GetComponentInParent<Player>();

        if (player != null)
        {
            player.TakeDamage();
        }

        Spawner.Instance.RemoveEnemyFromList(enemy);

        Destroy(enemy.gameObject);
    }
}