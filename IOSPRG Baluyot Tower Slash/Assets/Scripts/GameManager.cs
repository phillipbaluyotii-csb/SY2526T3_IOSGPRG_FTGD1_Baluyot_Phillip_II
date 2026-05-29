using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject _gameOverPanel;
    
    private bool _isGameOver;

    public bool IsGameOver => _isGameOver;

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
}
