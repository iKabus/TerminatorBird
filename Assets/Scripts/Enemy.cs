using System;
using UnityEngine;

[RequireComponent(typeof(Shooter))]
public class Enemy : MonoBehaviour, IPoolable
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private ScoreData _score;

    private IShooter _shooter;
    private ObjectPool<Enemy> _enemies;
    private PlayerCollisionHandler _handler;

    private void Awake()
    {
        _handler = GetComponent<PlayerCollisionHandler>();
    }

    public void Init(ObjectPool<Bullet> bullets)
    {
        _shooter = GetComponent<Shooter>();
        
        _shooter.Init(bullets, _firePoint);
    }

    private void OnEnable()
    {
        _handler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _handler.CollisionDetected -= ProcessCollision;
    }

    public void Spawn()
    {
        _shooter?.StartAutoShooting(Vector2.left, BulletOwner.Enemy);
    }

    public void Despawn()
    {
        _shooter?.StopAutoShooting();
    }

    public void SetPool<T>(ObjectPool<T> pool) where T : MonoBehaviour, IPoolable
    {
        _enemies = pool as ObjectPool<Enemy>;
    }
    
    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is GameZone)
        {
            _enemies.ReturnToPool(this);
        }

        if (interactable is Bullet bullet)
        {
            if (bullet.Owner == BulletOwner.Player)
            {
                gameObject.SetActive(false);
                
                _enemies.ReturnToPool(this);
                
                _score.AddScore(1);
            }
        }
    }
}
