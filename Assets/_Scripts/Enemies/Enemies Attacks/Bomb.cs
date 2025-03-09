using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Vector2 _targetPos;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _explosionRadius = 3f;
    [SerializeField] private float _damage = 20f;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _lifeTime = 5f;

    private Transform _target;

    public void SetTarget(Vector2 target)
    {
        _targetPos = target;
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _targetPos, _speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _targetPos) < 0.2f)
        {
            Explode();
        }
    }

    private void Explode()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, _explosionRadius, _playerLayer);

        foreach (Collider2D player in hitPlayer)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

            playerHealth.TakeDamage(_damage);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(_damage);
            }

            Destroy(gameObject);
        }
    }
}
