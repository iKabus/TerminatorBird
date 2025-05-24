using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour, IShooter
{
    [SerializeField] private float _fireRate = 1f;

    private Transform _firePoint;
    private ObjectPool<Bullet> _bulletPool;
    private Coroutine _autoShooting;

    public void Init(ObjectPool<Bullet> bulletPool, Transform firePoint)
    {
        StopAutoShooting();
        
        _bulletPool = bulletPool;
        _firePoint = firePoint;
    }

    public void Shooting(Vector2 direction, BulletOwner owner)
    {
        var bullet = _bulletPool.Get();
        bullet.transform.position = _firePoint.position;
        bullet.Launch(direction,  owner);
    }

    public void StartAutoShooting(Vector2 direction, BulletOwner owner)
    {
        if (_autoShooting == null)
        {
            _autoShooting = StartCoroutine(AutoShoot(direction, owner));
        }
    }

    public void StopAutoShooting()
    {
        if (_autoShooting != null)
        {
            StopCoroutine(_autoShooting);

            _autoShooting = null;
        }
    }

    private IEnumerator AutoShoot(Vector2 direction, BulletOwner owner)
    {
        var wait = new WaitForSeconds(_fireRate);

        while (enabled)
        {
            Shooting(direction, owner);
            yield return wait;
        }
    }
}
