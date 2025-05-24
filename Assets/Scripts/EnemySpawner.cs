using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private int _enemyPoolSize = 10;
    [SerializeField] private int _bulletPoolSize = 20;
    [SerializeField] private float _spawnInterval = 3f;
    [SerializeField] private float _lowerBound;
    [SerializeField] private float _upperBound;


    private ObjectPool<Enemy> _enemyPool;
    private ObjectPool<Bullet> _bulletPool;
    private Coroutine _spawning;

    private void Start()
    {
        _bulletPool = new ObjectPool<Bullet>(_bulletPrefab, _bulletPoolSize);
        _enemyPool = new ObjectPool<Enemy>(_enemyPrefab, _enemyPoolSize);

        _spawning = StartCoroutine(SpawnEnemies());
    }
    
    private void OnDestroy()
    {
        if (_spawning != null)
        {
            StopCoroutine(_spawning);
        }
    }

    private IEnumerator SpawnEnemies()
    {
        var wait = new WaitForSeconds(_spawnInterval);

        while (enabled)
        {
            SpawnEnemy();
            yield return wait;
        }
    }

    private void SpawnEnemy()
    {
        var enemy = _enemyPool.Get();

        float spawnPositionY = Random.Range(_upperBound, _lowerBound);

        Vector2 spawnPoint = new Vector2(transform.position.x, spawnPositionY);

        enemy.Init(_bulletPool);

        enemy.transform.position = spawnPoint;

        enemy.Spawn();
    }
}
