using UnityEngine;
using System.Collections;

public class BigEnemy : Enemy
{
    [Header("Components")]
    [SerializeField] private Transform _player;
    [SerializeField] LayerMask _playerLayer;
    [SerializeField] float _speed = 5f;
    [SerializeField] float _detectionRadius = 5f;
    [SerializeField] float _minDistanceToPlayer = 0.8f;
    [SerializeField] float _damage = 25f;


    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if(_player != null)
        {
            if (IsPlayerDetected())
            {
                ChasePlayer();
            }
        }
    }

    private void ChasePlayer()
    {
        if (_player != null)
        {
            float distance = Vector2.Distance(transform.position, _player.position);

            if (distance > _minDistanceToPlayer)
            {
                transform.position = Vector2.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
            }
        }
    }

    bool IsPlayerDetected()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _detectionRadius, _playerLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                _player = collider.transform;
                return true;
            }
        }

        return false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = _player.GetComponent<PlayerHealth>();

            if(playerHealth != null) playerHealth.TakeDamage(_damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
}
