using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IInteractable, IPoolable
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _lifetime = 3f;

    private Vector2 _direction;
    private Coroutine _despawnCoroutine;
    private IPool _pool;
    
    public BulletOwner Owner { get; private set; }
    
    private void Update()
    {
        transform.Translate(_direction * (_speed * Time.deltaTime));
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Owner != BulletOwner.Enemy)
        {
            _pool?.ReturnToPool(this);
        }
    }

    public void Despawn()
    {
        if (_despawnCoroutine != null)
        {
            StopCoroutine(_despawnCoroutine);

            _despawnCoroutine = null;
        }
    }

    public void SetPool(IPool pool)
    {
        _pool = pool;
    }

    public void Spawn() { }
    
    public void Launch(Vector2 direction,  BulletOwner owner)
    {
        _direction = direction;
        Owner = owner;
        StartDespawnTimer();
    }

    private void StartDespawnTimer()
    {
        if (_despawnCoroutine != null)
        {
            StopCoroutine(_despawnCoroutine);
        }

        _despawnCoroutine = StartCoroutine(DespawnAfterTime());
    }

    private IEnumerator DespawnAfterTime()
    {
        yield return new WaitForSeconds(_lifetime);

        _pool?.ReturnToPool(this);
    }
}
