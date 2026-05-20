using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _speed;

    public void Initialize()
    {
        _health = Random.Range(0, 100);
        _speed = Random.Range(1, 10);
    }
}
