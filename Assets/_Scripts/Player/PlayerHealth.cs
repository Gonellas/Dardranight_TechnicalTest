using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Components")]
    [SerializeField] Slider _healthBar;
    [SerializeField] TextMeshProUGUI _livesTxt;
    [SerializeField] GameObject _explosion;

    [Header("Values")]
    private int _lives;
    public float maxHealth = 100f;
    public float currentHealth;
    public bool canTakeDamage;

    private void Start()
    {
        _lives = PlayerPrefs.GetInt("Player_Lives", 3);
        currentHealth = maxHealth;
        UpdateHealthBar();
        UpdateLivesUI();
    }

    public void UpdateHealthBar()
    {
        if (canTakeDamage)
        {
            currentHealth = Mathf.Max(0, currentHealth);
            _healthBar.value = ((float)currentHealth / maxHealth) * 100f;
        }
        else canTakeDamage = false;
    }

    private void UpdateLivesUI()
    {
        if (_livesTxt != null)
        {
            _livesTxt.text = " " + _lives + " ";
        }
    }

    public void TakeDamage(float damage)
    {
        if(canTakeDamage)
        {
            currentHealth -= damage;

            UpdateHealthBar();
        }

        if (currentHealth <= 0) LoseLife();
    }

    private void LoseLife()
    {
        _lives--;
        PlayerPrefs.SetInt("Player_Lives", _lives);
        PlayerPrefs.Save();
        UpdateLivesUI();

        if (_lives > 0)
        {
            currentHealth = maxHealth;
            UpdateHealthBar();
        }
        else Die();
    }

    private void Die()
    {
        GameObject explosionInstance = Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(explosionInstance, .3f);
        GameManager.Instance.LoseGame();
        Destroy(gameObject);
        Debug.Log("El player murió");
    }
}
