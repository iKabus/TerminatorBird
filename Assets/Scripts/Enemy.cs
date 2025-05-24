using System;
using UnityEngine;

[RequireComponent(typeof(Shooter))]
public class Enemy : MonoBehaviour, IPoolable
{
    [SerializeField] private Transform _firePoint;

    private IShooter _shooter;
    private PlayerCollisionHandler _handler;
    private IPool _pool;

    private void Awake()
    {
        _handler = GetComponent<PlayerCollisionHandler>();
    }
    
    private void OnEnable()
    {
        _handler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _handler.CollisionDetected -= ProcessCollision;
    }
    
    public void Init(ObjectPool<Bullet> bullets)
    {
        _shooter = GetComponent<Shooter>();
        
        _shooter.Init(bullets, _firePoint);
    }

    public void Spawn()
    {
        _shooter?.StartAutoShooting(Vector2.left, BulletOwner.Enemy);
    }

    public void Despawn()
    {
        _shooter?.StopAutoShooting();
    }

    public void SetPool(IPool pool)
    {
        _pool = pool;
    }
    
    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is GameZone)
        {
            _pool.ReturnToPool(this);
        }

        if (interactable is Bullet bullet)
        {
            if (bullet.Owner == BulletOwner.Player)
            {
                gameObject.SetActive(false);
                
                _pool?.ReturnToPool(this);
                
                ScoreManager.Instance.Add(1);
            }
        }
    }
}
