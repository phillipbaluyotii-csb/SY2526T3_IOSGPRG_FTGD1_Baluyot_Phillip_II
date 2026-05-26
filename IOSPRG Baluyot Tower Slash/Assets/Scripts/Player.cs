using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    private Enemy _currentEnemy;

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
            Spawner.Instance.RemoveEnemyFromList(enemy);
            Destroy(enemy.gameObject);
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
}