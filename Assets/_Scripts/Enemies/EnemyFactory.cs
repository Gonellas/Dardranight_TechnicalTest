using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public static EnemyFactory Instance { get; private set; }

    [SerializeField] private Enemy _bigEnemyPrefab;
    [SerializeField] private Enemy _mediumEnemyPrefab;
    [SerializeField] private Enemy _smallEnemyPrefab;
    [SerializeField] private int _initialAmount;

    private Pool<Enemy> _bigEnemyPool;
    private Pool<Enemy> _mediumEnemyPool;
    private Pool<Enemy> _smallEnemyPool;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _bigEnemyPool = new Pool<Enemy>(CreateBigEnemy, Enemy.TurnOn, Enemy.TurnOff, _initialAmount);
        _mediumEnemyPool = new Pool<Enemy>(CreateMediumEnemy, Enemy.TurnOn, Enemy.TurnOff, _initialAmount);
        _smallEnemyPool = new Pool<Enemy>(CreateSmallEnemy, Enemy.TurnOn, Enemy.TurnOff, _initialAmount);
    }

    Enemy CreateBigEnemy()
    {
        return Instantiate(_bigEnemyPrefab);
    }

    Enemy CreateMediumEnemy()
    {
        return Instantiate(_mediumEnemyPrefab);
    }

    Enemy CreateSmallEnemy()
    {
        return Instantiate(_smallEnemyPrefab);
    }

    public Enemy GetObjectFromPool(EnemyType enemyType)
    {
        switch(enemyType)
        {
            case EnemyType.Big:
                return _bigEnemyPool.GetObject();
            case EnemyType.Medium:
                return _mediumEnemyPool.GetObject();
            case EnemyType.Small:
                return _smallEnemyPool.GetObject();
            default:
                return null;
        }
    }

    public void ReturnObjectToPool(Enemy enemy)
    {
        switch (enemy.EnemyType)
        {
            case EnemyType.Big:
                _bigEnemyPool.ReturnObjectToPool(enemy);
                break;
            case EnemyType.Medium:
                _mediumEnemyPool.ReturnObjectToPool(enemy);
                break;
            case EnemyType.Small:
                _smallEnemyPool.ReturnObjectToPool(enemy);
                break;
        }
    }
}
