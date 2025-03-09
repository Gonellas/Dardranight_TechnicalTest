using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _initialLifeTime;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage = 10f;

    private float _currentSpeed;
    private float _currentDamage;
    private Enemy _enemy;

    private void Update()
    {
        BulletMovement();

        _initialLifeTime -= Time.deltaTime;

        if(_initialLifeTime <= 0) BulletFactory.Instance.ReturnObjectToPool(this);
    }

    private void BulletMovement()
    {
        transform.position += Vector3.up * _currentSpeed * Time.deltaTime; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _enemy = other.GetComponent<Enemy>();

            if (_enemy != null) _enemy.TakeDamage(_damage);

            BulletFactory.Instance.ReturnObjectToPool(this);
        }
        else if (!other.gameObject.CompareTag("Player"))
        {
            BulletFactory.Instance.ReturnObjectToPool(this);
        }
    }

    private void Reset()
    {
        _initialLifeTime = 2f;
    }

    public static void TurnOn(Bullet b)
    {
        b.Reset();
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(Bullet b)
    {
        b.gameObject.SetActive(false);
    }

    public void SetBulletStats(float speed, float damage)
    {
        _currentSpeed = speed;
        _currentDamage = damage;
    }

    public void ResetStats()
    {
        _currentSpeed = _speed;
        _currentDamage = _damage;
    }
}
