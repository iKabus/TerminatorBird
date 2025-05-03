using UnityEngine;

public interface IShooter
{
    public void Init(ObjectPool<Bullet> bulletPool, Transform firePoint);
    public void Shooting(Vector2 direction);
    public void StartAutoShooting(Vector2 direction);
    public void StopAutoShooting();
}
