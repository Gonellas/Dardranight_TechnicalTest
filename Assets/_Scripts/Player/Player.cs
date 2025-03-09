using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Values")]
    [SerializeField] private float _speed;
    private float _currentSpeed;
    private Vector2 _direction;


    [Header("Shooting")]
    [SerializeField] private GameObject _bulletSpawn;

    [Header("Bullet PU")]
    private float _bulletSpeed = 5f;
    private float _bulletDamage = 10f;
    private bool _bulletPowerUpIsActive = false;
    private float _bulletPowerUpEndTime = 0f;

    [Header("SpeedPU")]
    private bool _speedPowerUpIsActive = false;
    private float _speedPowerUpEndTime = 0f;

    private void Start()
    {
        _currentSpeed =  _speed;
    }

    private void Update()
    {
        float aXH = Input.GetAxisRaw("Horizontal");
        float aXV = Input.GetAxisRaw("Vertical"); 
        
        _direction = new Vector2(aXH, aXV).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();           
        }

        if (_bulletPowerUpIsActive && Time.time >= _bulletPowerUpEndTime)
        {
            ResetBulletStats();
        }

        if(_speedPowerUpIsActive && Time.time >= _speedPowerUpEndTime)
        {
            ResetPlayerSpeed();
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(_direction * _currentSpeed * Time.fixedDeltaTime);
    }

    private void Shoot()
    {
        Bullet bullet = BulletFactory.Instance.GetObjectFromPool();

        if (bullet != null)
        {
            bullet.transform.position = _bulletSpawn.transform.position;
            bullet.SetBulletStats(_bulletSpeed, _bulletDamage);
        }
    }

    public void ApplyBulletPU(float newSpeed, float newDamage, float duration)
    {
        _bulletSpeed = Mathf.Max(_bulletSpeed + newSpeed, _bulletSpeed);
        _bulletDamage = newDamage;
        _bulletPowerUpIsActive = true;
        _bulletPowerUpEndTime = Time.time + duration;

        Debug.Log("power up applied");
    }

    private void ResetBulletStats()
    {
        _bulletSpeed = 5f;
        _bulletDamage = 10f;
        _bulletPowerUpIsActive = false;
    }

    public void ApplySpeedPU(float newSpeed, float duration)
    {
        _currentSpeed += newSpeed;
        _speedPowerUpIsActive = true;
        _speedPowerUpEndTime = Time.time + duration;
    }

    private void ResetPlayerSpeed()
    {
        _currentSpeed = _speed;
        _speedPowerUpIsActive = false;
    }
}
