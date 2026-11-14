using System.Runtime.CompilerServices;
using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField]
    private int maxHealth = 10;
    [SerializeField]
    private int height;
    [SerializeField]
    private int width;
    [SerializeField]
    private int weight;
    [SerializeField]
    public float speed;

    private int currentHealth;


    // Unsure if we will need Start and Update in the base class, or just the final subclasses.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentHealth = maxHealth;
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    //public void move()
    //{

    //}
    public void Attack() { }
    public void AdjustHealth(int damage) 
    {
        Debug.Log("Starting Health: " + currentHealth);
        Debug.Log(damage + " damage taken");
        currentHealth -= damage;
        Debug.Log(currentHealth + " health left");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Character Is Dead");
    }

    public void Knockback() { }
}


