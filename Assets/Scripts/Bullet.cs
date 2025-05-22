using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IInteractable, IPoolable
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _lifetime = 3f;

    private Vector2 _direction;
    private ObjectPool<Bullet> _bullets;
    private Coroutine _despawnCoroutine;
    
    public BulletOwner Owner { get; private set; }

    public void Launch(Vector2 direction,  BulletOwner owner)
    {
        _direction = direction;
        Owner = owner;
        StartDespawnTimer();
    }

    private void Update()
    {
        transform.Translate(_direction * (_speed * Time.deltaTime));
    }

    public void Despawn()
    {
        if (_despawnCoroutine != null)
        {
            StopCoroutine(_despawnCoroutine);

            _despawnCoroutine = null;
        }
    }

    public void SetPool<T>(ObjectPool<T> pool) where T : MonoBehaviour, IPoolable
    {
        _bullets = pool as ObjectPool<Bullet>;
    }

    public void Spawn() { }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Owner == BulletOwner.Enemy == false)
        {
            _bullets?.ReturnToPool(this);
        }
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

        _bullets?.ReturnToPool(this);
    }
}
