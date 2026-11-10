using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [Header("Health Settings")]
    public float maxHealth = 100;
    public float currentHealth;

    [Header("UI Reference")]
    public Slider healthSlider;
    public TextMeshProUGUI healthText;

    void Start()
    {
        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        UpdateHealthUI();

    }

    public void TakeDamage(float damage)
    {
        //Subtract damage and ensure health doesn't go below 0 or above maxHealth
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        //This updates healthBar UI
        healthSlider.value = currentHealth;
        UpdateHealthUI();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        //Add heal amount and ensure health doesn't go below 0 or above maxHealth
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        //This updates healthBar UI
        healthSlider.value = currentHealth;
        UpdateHealthUI();
    }

    void Die()
    {
        //Handle player death 
        Debug.Log("Player has died.");
    }
    public void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString("F0") + "/" + maxHealth.ToString("F0");
        }
    }

    void Update()
    {

        //I'm just gonna throw some stuff in here to test the health system. This will be removed later.
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Heal(10);
        }

    }

}
