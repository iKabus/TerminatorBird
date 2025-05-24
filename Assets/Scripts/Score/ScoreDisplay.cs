using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    private TMP_Text _scoreText;

    private void Awake()
    {
        _scoreText =  GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        ScoreManager.Instance.Changed += UpdateScoreText;
        
        UpdateScoreText(ScoreManager.Instance.Score);
    }

    private void OnDisable()
    {
        ScoreManager.Instance.Changed -= UpdateScoreText;
    }

    private void UpdateScoreText(int newScore)
    {
        _scoreText.text = "Score: " + newScore;
    }
}
