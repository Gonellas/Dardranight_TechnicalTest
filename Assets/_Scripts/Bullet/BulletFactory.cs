using UnityEngine;
using UnityEngine.Pool;

public class BulletFactory : MonoBehaviour
{
    public static BulletFactory Instance { get; private set; }

    [SerializeField] private Bullet _bulletPrefab;
    private Pool<Bullet> _bulletPool;
    [SerializeField] private int _initialAmount;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _bulletPool = new Pool<Bullet>(CreateBullet, Bullet.TurnOn, Bullet.TurnOff, _initialAmount);
    }

    Bullet CreateBullet()
    {
        return Instantiate(_bulletPrefab);
    }

    public Bullet GetObjectFromPool()
    {
        return _bulletPool.GetObject();
    } 

    public void ReturnObjectToPool(Bullet bullet)
    {
        _bulletPool.ReturnObjectToPool(bullet);
    }
}
