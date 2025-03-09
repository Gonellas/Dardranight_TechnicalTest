using UnityEngine;
using System;

public enum EnemyType
{
    Big,
    Medium,
    Small
}

public abstract class Enemy : MonoBehaviour, IDamageable
{

    [Header("Enemy Stats")]
    [SerializeField] private float _maxHp;
    [SerializeField] private float _currentHp;
    [SerializeField] GameObject _explosion;
    [SerializeField] int _points;

    Enemy _enemy;

    public Func<Enemy> instantiateMethod;

    [SerializeField] EnemyType _enemyType;
    public EnemyType EnemyType => _enemyType;

    private void Start()
    {
        _currentHp = _maxHp;
    }

    public void TakeDamage(float damage)
    {
        _currentHp -= damage;

        if (_currentHp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //Audio Manager
        //Random powerUp

        if (GameManager.Instance != null) GameManager.Instance.AddScore(_points);

        GameObject explosionInstance = Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(explosionInstance, .3f);

        EnemySpawner.enemiesKilled++;

        if(EnemySpawner.enemiesKilled % 5 == 0) FindFirstObjectByType<EnemySpawner>().SpawnPowerUp(transform.position);

        EnemySpawner.activeEnemies--;

        EnemyFactory.Instance.ReturnObjectToPool(this);
    }

    public static void TurnOn(Enemy e)
    {
        e.gameObject.SetActive(true);
    }

    public static void TurnOff(Enemy e)
    {
        e.gameObject.SetActive(false);
    }

}
