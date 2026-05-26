using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    //[SerializeField] private int _health = 1;
    [SerializeField] private float _speed = 3f;

    [Header("Arrow Settings")]
    [SerializeField] private SwipeType _swipeType;
    [SerializeField] private bool _isReverse;

    [SerializeField] private SpriteRenderer _arrowRenderer;

    [SerializeField] private Sprite _upArrow;
    [SerializeField] private Sprite _downArrow;
    [SerializeField] private Sprite _leftArrow;
    [SerializeField] private Sprite _rightArrow;

    private bool _canBeKilled;

    public SwipeType SwipeType => _swipeType;

    public bool IsReverse => _isReverse;

    public bool CanBeKilled => _canBeKilled;

    private void Start()
    {
        InitializeEnemy();
    }

    private void Update()
    {
        MoveDown();
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    public void InitializeEnemy()
    {
        _swipeType = (SwipeType)Random.Range(0, 4);

        _isReverse = Random.Range(0, 2) == 0;

        SetArrowSprite();

        if (_isReverse)
{
    _arrowRenderer.color = Color.red;
}
else
{
    _arrowRenderer.color = Color.green;
}
    }

    public void EnableKill()
    {
        _canBeKilled = true;
    }

    public void DisableKill()
    {
        _canBeKilled = false;
    }

    private void SetArrowSprite()
    {
        switch (_swipeType)
        {
            case SwipeType.UP:
                _arrowRenderer.sprite = _upArrow;
                break;

            case SwipeType.DOWN:
                _arrowRenderer.sprite = _downArrow;
                break;

            case SwipeType.LEFT:
                _arrowRenderer.sprite = _leftArrow;
                break;

            case SwipeType.RIGHT:
                _arrowRenderer.sprite = _rightArrow;
                break;
        }
    }
}