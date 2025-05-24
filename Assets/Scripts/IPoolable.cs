using UnityEngine;

public interface IPoolable
{
    public void SetPool(IPool pool);
    public void Spawn();
    public void Despawn();
}
