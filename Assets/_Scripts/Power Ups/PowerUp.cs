using UnityEngine;

public enum PowerUpType
{
    BulletPU,
    SpeedPU
}
public class PowerUp : MonoBehaviour
{
    [Header("Power Up Type")]
    [SerializeField] private PowerUpType powerUpType;

    [Header("BulletPU Values")]
    [SerializeField] float _newBulletSpeed = 3f;
    [SerializeField] float _newBulletDamage = 15f;

    [Header("SpeedPU Values")]
    [SerializeField] float _newPlayerSpeed = 3f;

    [SerializeField] float _powerUpDuration = 5f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                if(powerUpType == PowerUpType.BulletPU)
                {
                    player.ApplyBulletPU(_newBulletSpeed, _newBulletDamage, _powerUpDuration);
                }
                else if (powerUpType == PowerUpType.SpeedPU)
                {
                    player.ApplySpeedPU(_newPlayerSpeed, _powerUpDuration);
                }
            }

            Destroy(gameObject);
        }
            
    }
}
