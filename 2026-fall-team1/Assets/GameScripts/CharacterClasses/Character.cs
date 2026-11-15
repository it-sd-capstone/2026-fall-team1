using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

    [SerializeField]
    private float maxHealth = 10f;
    [SerializeField]
    private int height;
    [SerializeField]
    private int width;
    [SerializeField]
    private int weight;
    [SerializeField]
    public float speed = 5.0f;

    [Header("UI Reference")]
    public Slider healthSlider;
    public TextMeshProUGUI healthText;


    private float currentHealth;
    public float CurrentHealth => currentHealth;


    protected virtual void Awake()
    {
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        UpdateHealthUI();
    }

    public void Attack() { }
    public void AdjustHealth(int damage) 
    {
        //Subtract damage and ensure health doesn't go below 0 or above maxHealth
        Debug.Log("Starting Health: " + currentHealth);
        Debug.Log(damage + " damage taken");
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        //update UI and HealthBar
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
        UpdateHealthUI();
        Debug.Log(currentHealth + " health left");

        //Kill if below 0 health
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Character Is Dead");
    }
    public void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString("F0") + "/" + maxHealth.ToString("F0");
        }
    }

    public void Knockback() { }
}


