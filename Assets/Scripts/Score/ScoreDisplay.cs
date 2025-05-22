using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private ScoreData _scoreData;
    
    private TMP_Text _scoreText;

    private void Awake()
    {
        _scoreText =  GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _scoreData.ScoreChanged += UpdateScoreText;
        
        UpdateScoreText(_scoreData.Score);
    }

    private void OnDisable()
    {
        _scoreData.ScoreChanged -= UpdateScoreText;
    }

    private void UpdateScoreText(int newScore)
    {
        _scoreText.text = "Score: " + newScore;
    }
}
