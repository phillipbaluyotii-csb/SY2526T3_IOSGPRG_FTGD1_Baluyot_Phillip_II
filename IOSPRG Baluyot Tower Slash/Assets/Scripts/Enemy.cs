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
    [SerializeField] private bool _isRotating;

    [SerializeField] private SpriteRenderer _arrowRenderer;
    [SerializeField] private Transform _arrowTransform;
    [SerializeField] private float _rotationSpeed = 300f;

    [SerializeField] private Sprite _upArrow;
    [SerializeField] private Sprite _downArrow;
    [SerializeField] private Sprite _leftArrow;
    [SerializeField] private Sprite _rightArrow;

    [SerializeField] private GameObject _blackBox;  // black box

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
        RotateArrow();
    }

    /*private void Awake()
    {
        if (_arrowTran6sform == null)
            _arrowTransform = transform.Find("Arrow");

        if (_arrowRenderer == null)
            _arrowRenderer = GetComponentInChildren<SpriteRenderer>();
    }*/

    private void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    public void InitializeEnemy()
    {
        _swipeType = (SwipeType)Random.Range(0, 4);

        _isReverse = Random.Range(0, 2) == 0;
        _isRotating = Random.Range(0, 4) == 0;

        SetArrowSprite();

        if (_isRotating)
        {
            _arrowRenderer.color = Color.yellow;
        }
        else if (_isReverse)
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

        if (_blackBox != null)
        {
            _blackBox.SetActive(true);
        }

        if (_isRotating)
        {
            DetermineRotatingDirection();
        }
    }

    public void DisableKill()
    {
        _canBeKilled = false;

        if (_blackBox != null)
        {
            _blackBox.SetActive(false);
        }
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

    private void RotateArrow()
    {
        if (!_isRotating)
            return;

        if (_canBeKilled)
            return;

        _arrowTransform.Rotate(0f, 0f, -_rotationSpeed * Time.deltaTime);
    }

    private void DetermineRotatingDirection()
    {
        float zRotation = (_arrowTransform.eulerAngles.z + 360f) % 360f;

        if (zRotation >= 315 || zRotation < 45)
        {
            _swipeType = SwipeType.UP;
            _arrowTransform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (zRotation >= 45 && zRotation < 135)
        {
            _swipeType = SwipeType.LEFT;
            _arrowTransform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (zRotation >= 135 && zRotation < 225)
        {
            _swipeType = SwipeType.DOWN;
            _arrowTransform.eulerAngles = new Vector3(0, 0, 180);
        }
        else
        {
            _swipeType = SwipeType.RIGHT;
            _arrowTransform.eulerAngles = new Vector3(0, 0, 270);
        }

        SetArrowSprite();
    }

    private void OnBecameInvisible()
    {
        if (Spawner.Instance != null)
        {
            Spawner.Instance.RemoveEnemyFromList(this);
        }

        Destroy(gameObject);
    }
}