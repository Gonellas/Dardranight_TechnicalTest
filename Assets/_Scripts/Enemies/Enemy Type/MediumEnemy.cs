using System.Collections;
using UnityEngine;

public class MediumEnemy : Enemy
{
    [Header("Components")]
    [SerializeField] private Transform _player;
    [SerializeField] LayerMask _playerLayerMask;
    [SerializeField] float _speed = 5f;
    [SerializeField] float _detectionRadius = 5f;

    [Header("Shooting Values")]
    [SerializeField] GameObject _bomb;
    [SerializeField] float _bombDamage = 25f;
    [SerializeField] bool _canSpawnBomb = true;
    [SerializeField] float _bombCooldown = 10f;
    private float _lastBombSpawnTime;
    [SerializeField] float _minDistanceToPlayer = .8f;

    private void Start()
    {
        _player = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        if(_player != null && IsPlayerDetected() && _canSpawnBomb)
        {
            _canSpawnBomb = false;
            StartCoroutine(ActiveBomb());
        }
    }

    private IEnumerator ActiveBomb()
    {
        SpawnBomb();

        yield return new WaitForSeconds(1.5f);

        _canSpawnBomb = true;
    }

    private void SpawnBomb()
    {
        GameObject bomb = Instantiate(_bomb, transform.position, Quaternion.identity);
        Bomb bombScript = bomb.GetComponent<Bomb>();

        if(bombScript != null)
        {
            bombScript.SetTarget(_player.position);
        }
    }

    private void ChasePlayer()
    {
        float distance = Vector2.Distance(transform.position, _player.position);

        if (distance > _minDistanceToPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
        }
    }

    bool IsPlayerDetected()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _detectionRadius, _playerLayerMask);

        foreach(Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                _player = collider.transform;
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
}
