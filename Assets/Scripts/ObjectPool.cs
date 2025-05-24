using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : IPool where T : MonoBehaviour, IPoolable
{
    private T _prefab;
    private Transform _parent;
    private Queue<T> _pool = new();

    public ObjectPool(T prefab, int initialCount, Transform parent = null)
    {
        _prefab = prefab;
        _parent = parent;

        for (int i = 0; i < initialCount; i++)
        {
            _pool.Enqueue(CreateNewObject());
        }
    }

    public T Get()
    {
        var obj = _pool.Count > 0 ? _pool.Dequeue() : CreateNewObject();
        
        obj.gameObject.SetActive(true);
        
        obj.Spawn();
        
        return obj;
    }

    public void ReturnToPool(MonoBehaviour obj)
    {
        if (obj is T poolable)
        {
            poolable.Despawn();
            poolable.gameObject.SetActive(false);
            _pool.Enqueue(poolable);
        }
    }

    private T CreateNewObject()
    {
        var instance = Object.Instantiate(_prefab, _parent);
        instance.SetPool(this);
        instance.gameObject.SetActive(false);
        
        return instance;
    }
}
