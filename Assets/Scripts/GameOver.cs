using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Scenes _scene;
    
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.GameOver += SwitchScene;
    }

    private void OnDisable()
    {
        _player.GameOver -= SwitchScene;
    }

    private void SwitchScene()
    {
        SceneManager.LoadScene(_scene.ToString());
    }
}
