using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Singleton<Spawner>
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _spawnLocation;

    [SerializeField] private int _maxEnemies = 9999;
    // remove for uncapping

    [SerializeField] private float _minSpawn = 0.3f;
    [SerializeField] private float _maxSpawn = 1.5f;

    private List<GameObject> _enemies = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(CO_SpawnRoutine());
    }

    public void SpawnEnemy()
    {
        GameObject enemy = Instantiate(_enemyPrefab, _spawnLocation.transform.position, Quaternion.identity);
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        enemyScript.InitializeEnemy();
        _enemies.Add(enemy);
    }

    public void RemoveEnemyFromList(Enemy enemy)
    {
        _enemies.Remove(enemy.gameObject);
    }

    private IEnumerator CO_SpawnRoutine()
    {
        while (true)
        {
            if (_enemies.Count < _maxEnemies)
            {
                SpawnEnemy();
            }

            yield return new WaitForSeconds(Random.Range(_minSpawn, _maxSpawn));
        }
    }
}
