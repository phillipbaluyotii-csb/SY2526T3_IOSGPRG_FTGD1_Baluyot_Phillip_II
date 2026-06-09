using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject _gameOverPanel;
    
    private bool _isGameOver;

    public bool IsGameOver => _isGameOver;

    [Header("Score")]   // scoring
    [SerializeField] private TMP_Text _scoreText;

    private int _score;

    private void Start()
    {
        UpdateScoreUI();
    }

    public void GameOver()
    {
        if (_isGameOver)
            return;

        _isGameOver = true;

        Time.timeScale = 0f;

        _gameOverPanel.SetActive(true);

        Debug.Log("GAME OVER!");
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddScore(int amount)
    {
        _score += amount;

        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (_scoreText != null)
        {
            _scoreText.text = "Score: " + _score;
        }
    }
}
