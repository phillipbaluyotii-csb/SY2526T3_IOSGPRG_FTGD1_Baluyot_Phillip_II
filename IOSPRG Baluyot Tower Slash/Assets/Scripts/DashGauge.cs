using UnityEngine;
using UnityEngine.UI;

public class DashGauge : Singleton<DashGauge>
{
    [Header("UI")]
    [SerializeField] private Image _gaugeFill;
    [SerializeField] private Button _dashButton;

    [Header("Gauge")]
    [SerializeField] private float _maxGauge = 100f;
    [SerializeField] private float _currentGauge;

    [Header("Drain")]
    [SerializeField] private float _drainSpeed = 5f;

    private bool _isDraining;
    private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>();

        _dashButton.onClick.AddListener(() =>
        {
            FindObjectOfType<Player>().StartDash();
        });
    }

    private void Update()
    {
        UpdateUI();
        DrainGauge();
    }

    public void AddGauge(float amount)
    {
        if (_isDraining)
            return;

        _currentGauge += amount;

        _currentGauge = Mathf.Clamp(_currentGauge, 0f, _maxGauge);
    }

    public bool CanDash()
    {
        return _currentGauge >= _maxGauge;
    }

    public void StartDrain()
    {
        _isDraining = true;
    }

    public bool IsDraining()
    {
        return _isDraining;
    }

    private void UpdateUI()
    {
        _gaugeFill.fillAmount = _currentGauge / _maxGauge;

        _dashButton.interactable = CanDash();
    }

    private void DrainGauge()
    {
        if (!_isDraining)
            return;

        _currentGauge -= _drainSpeed * Time.deltaTime;

        if (_currentGauge <= 0f)
        {
            _currentGauge = 0f;
            _isDraining = false;
        }
    }
}