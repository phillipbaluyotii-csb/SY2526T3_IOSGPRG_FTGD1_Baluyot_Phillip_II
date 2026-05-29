using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Player : MonoBehaviour
{
    [Header("Dash Mechanic")]
    [SerializeField] private float _dashDuration = 1f;
    [SerializeField] private bool _isDashing;

    [Header("Player Lives")]
    [SerializeField] private int _maxLives = 3;
    [SerializeField] private int _currentLives;
    
    private Enemy _currentEnemy;

    private void Start()
    {
        _currentLives = _maxLives;
    }

    private void Update()
    {
        CheckEnemy();
    }

    private void CheckEnemy()
    {
        if (_currentEnemy == null)
            return;

        if (!_currentEnemy.CanBeKilled)
            return;

        SwipeType currentSwipe = TouchInput.Instance.swipeType;

        if (currentSwipe == SwipeType.NONE)
            return;

        SwipeType expectedSwipe = _currentEnemy.SwipeType;

        if (_currentEnemy.IsReverse)
        {
            expectedSwipe = GetOppositeDirection(expectedSwipe);
        }

        if (currentSwipe == expectedSwipe)
        {
            Debug.Log("Enemy Defeated");

            Destroy(_currentEnemy.gameObject);
        }
        else
        {
            Debug.Log("Wrong Swipe");
        }

        TouchInput.Instance.swipeType = SwipeType.NONE;
    }

    private SwipeType GetOppositeDirection(SwipeType swipeType)
    {
        switch (swipeType)
        {
            case SwipeType.UP:
                return SwipeType.DOWN;

            case SwipeType.DOWN:
                return SwipeType.UP;

            case SwipeType.LEFT:
                return SwipeType.RIGHT;

            case SwipeType.RIGHT:
                return SwipeType.LEFT;
        }

        return SwipeType.NONE;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            if (_isDashing)
            {
                Spawner.Instance.RemoveEnemyFromList(enemy);
                Destroy(enemy.gameObject);

                DashGauge.Instance.AddGauge(5f);

                TrySpawnExtraLife();    // extra life chance
                return;
            }

            //GameManager.Instance.GameOver();
            TakeDamage();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.DisableKill();

            _currentEnemy = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.EnableKill();
            _currentEnemy = enemy;
        }
    }

    public void TakeDamage()
    {
        _currentLives--;

        Debug.Log("Lives left: " + _currentLives);

        if ( _currentLives == 0 )
        {
            GameManager.Instance.GameOver();
        }
    }

    public void AddLife()

    {
        _currentLives++;

        _currentLives = Mathf.Clamp(_currentLives, 0, _maxLives);

        Debug.Log("Extra Life Obtained!");
    }
    public void StartDash()
    {
        if (_isDashing)
            return;

        if (!DashGauge.Instance.CanDash())
            return;

        StartCoroutine(CO_DashRoutine());
    }

    private void TrySpawnExtraLife()
    {
        int chance = Random.Range(0, 100);

        if (chance < 3)
        {
            AddLife();
        }
    }

    private IEnumerator CO_DashRoutine()
    {
        _isDashing = true;

        DashGauge.Instance.DrainGauge();

        yield return new WaitForSeconds(_dashDuration);

        _isDashing = false;
    }
}