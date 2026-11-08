using UnityEngine;

public class Enemy : Character
{
    [SerializeField]
    private int aggroRange;
    [SerializeField]
    string patrolPath;
    // Pickup[] dropTable

    public void aggro() { }
    public void death() { }
    public void patrol() { }
}
