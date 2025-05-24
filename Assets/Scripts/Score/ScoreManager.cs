using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance {get; private set;}

    public event Action<int> Changed;

    private int _score;
    
    public int Score => _score;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Reset()
    {
        _score = 0;
        Changed?.Invoke(_score);
    }
    
    public void Add(int value)
    {
        _score += value;
        Changed?.Invoke(_score);
    }
}
