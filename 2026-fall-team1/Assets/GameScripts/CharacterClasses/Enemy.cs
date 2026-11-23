using UnityEngine;

public class Enemy : Character
{
    [Header("Enemy Reference")]
    [SerializeField]
    private int aggroRange;
    [SerializeField]
    string patrolPath;
    [SerializeField]
    private GameObject playerCharacter;
    // Pickup[] dropTable

    public void aggro() { }
    public override void Die() 
    {
        Destroy(gameObject);
    }
    public void patrol() { }
}
