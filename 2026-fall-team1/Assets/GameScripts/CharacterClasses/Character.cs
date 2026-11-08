using System.Runtime.CompilerServices;
using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField]
    private int Health;
    [SerializeField]
    private int height;
    [SerializeField]
    private int width;
    [SerializeField]
    private int weight;
    [SerializeField]
    private float speed;


    // Unsure if we will need Start and Update in the base class, or just the final subclasses.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void move()
    {
        
    }
    public void attack() { }
    public void adjustHealth() { }
    public void knockback() { }
}


