using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class Player : MonoBehaviour
{
    public float GaugeReward
    {
        get
        {
            if (_characterType == CharacterType.Speed)
                return 10f;

            return 5f;
        }
    }

    [Header("Dash Mechanic")]
    [SerializeField] private bool _isDashing;

    [SerializeField] private GameObject _dashHitbox;

    [Header("Player Lives")]
    [SerializeField] private int _maxLives = 3;
    [SerializeField] private int _currentLives;

    [SerializeField] private bool _isInvincible;
    [SerializeField] private float _invincibleDuration = 1f;

    [SerializeField] private TMP_Text _livesText;

    [SerializeField] private Animator _animator;

    [SerializeField] private RuntimeAnimatorController _defaultController;
    [SerializeField] private RuntimeAnimatorController _tankController;
    [SerializeField] private RuntimeAnimatorController _speedController;

    public static Player Instance;

    private Enemy _currentEnemy;
    private CharacterType _characterType;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ApplyCharacter();

        _currentLives = _maxLives;

        UpdateLivesUI();
    }

    private void Update()
    {
        CheckEnemy();
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
            if (_currentEnemy != enemy)
            {
                TouchInput.Instance.swipeType = SwipeType.NONE;
            }

            enemy.EnableKill();
            _currentEnemy = enemy;
        }
    }

    public void TakeDamage()
    {
        if (_isInvincible)
            return;

        StartCoroutine(CO_Invincibility());

        _currentLives--;

        UpdateLivesUI();

        Debug.Log("Lives left: " + _currentLives);

        if (_currentLives <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    public void AddLife()
    {
        _currentLives++;

        _currentLives = Mathf.Clamp(_currentLives, 0, _maxLives);

        UpdateLivesUI();

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

    private void ApplyCharacter()
    {
        _characterType =
            CharacterManager.SelectedCharacter;

        switch (_characterType)
        {
            case CharacterType.Default:

                _animator.runtimeAnimatorController =
                    _defaultController;
                _maxLives = 3;
                break;

            case CharacterType.Tank:

                _animator.runtimeAnimatorController =
                    _tankController;
                _maxLives = 5;
                break;

            case CharacterType.Speed:

                _animator.runtimeAnimatorController =
                    _speedController;
                _maxLives = 3;
                break;
        }
    }

    private void UpdateLivesUI()
    {
        _livesText.text = "Lives: " + _currentLives;
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

            GameManager.Instance.AddScore(1);

            Spawner.Instance.RemoveEnemyFromList(_currentEnemy);

            Destroy(_currentEnemy.gameObject);

            DashGauge.Instance.AddGauge(GaugeReward);

            TrySpawnExtraLife();
        }
        else
        {
            Debug.Log("Wrong Swipe");

            TakeDamage();
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

        _dashHitbox.SetActive(true);

        DashGauge.Instance.StartDrain();

        while (DashGauge.Instance.IsDraining())
        {
            yield return null;
        }

        _dashHitbox.SetActive(false);

        _isDashing = false;
    }

    private IEnumerator CO_Invincibility()
    {
        _isInvincible = true;

        yield return new WaitForSeconds(_invincibleDuration);

        _isInvincible = false;
    }
}