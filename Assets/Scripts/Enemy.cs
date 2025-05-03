using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Shooter))]
public class Enemy : MonoBehaviour, IPoolable
{
    [SerializeField] private Transform _firePoint;

    private IShooter _shooter;
    private ObjectPool<Enemy> _enemies;

    public void Init(ObjectPool<Bullet> bullets)
    {
        _shooter = GetComponent<IShooter>();
        _shooter.Init(bullets, _firePoint);
    }

    public void Spawn()
    {
        _shooter?.StartAutoShooting(Vector2.left);
    }

    public void Despawn()
    {
        _shooter?.StopAutoShooting();
    }

    public void SetPool<T>(ObjectPool<T> pool) where T : MonoBehaviour, IPoolable
    {
        _enemies = pool as ObjectPool<Enemy>;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _enemies.ReturnToPool(this);
    }
}
