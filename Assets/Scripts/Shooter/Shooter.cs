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
        _bulletPool = bulletPool;
        _firePoint = firePoint;
    }

    public void Shooting(Vector2 direction)
    {
        var bullet = _bulletPool.Get();
        bullet.transform.position = _firePoint.position;
        bullet.Launch(direction);
    }

    public void StartAutoShooting(Vector2 direction)
    {
        if (_autoShooting == null)
        {
            _autoShooting = StartCoroutine(AutoShoot(direction));
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

    private IEnumerator AutoShoot(Vector2 direction)
    {
        var wait = new WaitForSeconds(_fireRate);

        while (true)
        {
            Shooting(direction);
            yield return wait;
        }
    }
}
