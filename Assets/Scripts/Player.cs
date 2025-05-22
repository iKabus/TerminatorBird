using System;
using UnityEngine;

[RequireComponent (typeof(InputReader))]
[RequireComponent (typeof(PlayerMover))]
[RequireComponent (typeof(PlayerCollisionHandler))]
[RequireComponent(typeof(Shooter))]
public class Player : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private int _bulletPoolSize = 30;

    private InputReader _inputReader;
    private PlayerMover _playerMover;
    private PlayerCollisionHandler _handler;
    private IShooter _shooter;

    private ObjectPool<Bullet> _bulletPool;

    public event Action GameOver;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _playerMover = GetComponent<PlayerMover>(); 
        _handler = GetComponent<PlayerCollisionHandler>();
        _shooter = GetComponent<Shooter>();

        _bulletPool = new ObjectPool<Bullet>(_bulletPrefab, _bulletPoolSize);

        _shooter?.Init(_bulletPool, _firePoint);
    }

    private void OnEnable()
    {
        _inputReader.Jumping += Jump;
        _handler.CollisionDetected += ProcessCollision;
        _inputReader.Shooting += Shoot;
    }

    private void OnDisable()
    {
        _inputReader.Jumping -= Jump;
        _handler.CollisionDetected -= ProcessCollision;
        _inputReader.Shooting -= Shoot;
    }

    public void Reset()
    {
        _playerMover.Reset();
    }

    private void Jump()
    {
        _playerMover.MoveUp();
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Bullet || interactable is GameZone)
        {
            GameOver?.Invoke();
        }
    }

    private void Shoot()
    {
        _shooter?.Shooting(Vector2.right, BulletOwner.Player);
    }
}
