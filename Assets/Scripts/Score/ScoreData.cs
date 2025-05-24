using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ScoreData", menuName = "Game/Score Data")]
public class ScoreData : ScriptableObject
{
    [SerializeField] private int _score;
    
    public event Action<int> ScoreChanged;
    
    public int Score => _score;

    public void Reset()
    {
        _score = 0;
        
        ScoreChanged?.Invoke(_score);
    }

    public void Add(int value)
    {
        _score += value;
        
        ScoreChanged?.Invoke(_score);
    }
}