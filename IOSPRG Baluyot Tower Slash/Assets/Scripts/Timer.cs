using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    [SerializeField] private bool _detectedByPlayer = false; 
    
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private List<Sprite> _arrowSprites = new List<Sprite>();

    private void Start()
    {
        StartCoroutine(CO_ArrowRotation());
        StartCoroutine(CO_SpawnEnemyEveryXSeconds(5));
    }

    private IEnumerator CO_ArrowRotation()
    {
        int index = 0;

        while (!_detectedByPlayer)
        {
            _spriteRenderer.sprite = _arrowSprites[index % 4];
            index++;
            yield return new WaitForSecondsRealtime(1f);
        }
    }

    private IEnumerator CO_SpawnEnemyEveryXSeconds(float seconds)
    {
        float CurrentTime = 0;

        while(true)
        {
            CurrentTime += Time.deltaTime;
            Debug.Log($"Spawn Timer: {CurrentTime}");
            if (CurrentTime >= seconds)
            {
                Spawner.Instance.SpawnEnemy();
                CurrentTime = 0;
            }
            
            yield return new WaitForEndOfFrame();
        }
    }
    private IEnumerator CO_CountDownTimer(float startTime)
    {
        float currentTime = startTime;
        
        while (currentTime > 0)
        {
            Debug.Log($"Current Time: {currentTime}");
            yield return new WaitForSecondsRealtime(1f);
            currentTime--;
        }

        // do smth when the timer ends
        Debug.Log("Delayed Function goes here");
    }

    private IEnumerator CO_CountUpTimer()
    {
        float currentTime = 0;

        while (true)
        {
            Debug.Log($"Current Time: {currentTime}");
            yield return new WaitForSecondsRealtime(1f);
            currentTime++;
        }
    }
}
