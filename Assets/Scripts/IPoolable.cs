using UnityEngine;

public interface IPoolable
{
    public void Spawn();
    public void Despawn();
    public void SetPool<T>(ObjectPool<T> pool) where T: MonoBehaviour, IPoolable;
}
