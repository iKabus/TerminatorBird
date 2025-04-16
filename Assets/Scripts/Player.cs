using System;
using UnityEngine;

[RequireComponent (typeof(InputReader))]
[RequireComponent (typeof(PlayerMover))]
[RequireComponent (typeof(PlayerCollisionHandler))]
[RequireComponent (typeof(ScoreCounter))]
public class Player : MonoBehaviour
{
    private InputReader _inputReader;
    private PlayerMover _playerMover;
    private PlayerCollisionHandler _handler;
    private ScoreCounter _scoreCounter;

    public event Action GameOver;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _playerMover = GetComponent<PlayerMover>(); 
        _handler = GetComponent<PlayerCollisionHandler>();
        _scoreCounter = GetComponent<ScoreCounter>();
    }

    private void OnEnable()
    {
        _inputReader.Jumping += Jump;
        _handler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _inputReader.Jumping -= Jump;
        _handler.CollisionDetected -= ProcessCollision;
    }

    private void Jump()
    {
        _playerMover.MoveUp();
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Bullet || interactable is Ground)
        {
            Debug.Log("Ground!");

            GameOver?.Invoke();
        }

        //else if (interactable is ScoreZone)
        //{
        //    _scoreCounter.Add();
        //}
    }

    public void Reset()
    {
        _scoreCounter.Reset();
        _playerMover.Reset();
    }
}
