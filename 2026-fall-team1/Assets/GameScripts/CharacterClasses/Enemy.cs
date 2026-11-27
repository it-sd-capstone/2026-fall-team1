using Unity.Mathematics;
using UnityEngine;

public class Enemy : Character
{
    [Header("Enemy Reference")]
    [SerializeField]
    private int aggroRange;
    [SerializeField]
    string patrolPath;
    [SerializeField]
    public GameObject playerCharacter;
    [SerializeField]
    public float attackDelay = 5f;
    [SerializeField]
    public float returnToSpawnDelay = 5f;
    // Pickup[] dropTable
    protected Vector3 SpawnLocation;
    private GameObject aggroSphere;
    protected bool chasePlayer;
    protected float attackTime;
    protected float turnSpeed;
    protected float returnToSpawnTimer;
    protected bool returnToSpawn;

    protected override void Awake()
    {
        chasePlayer = false;
        attackTime = 0;
        turnSpeed = 180f;
        returnToSpawnTimer = 0;
        returnToSpawn = false;
        SpawnLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Transform aggroTransform = transform.Find("Aggro");
        if ( aggroTransform != null )
        {
            Debug.Log("Aggro Transform found");
            aggroSphere = aggroTransform.gameObject;
            aggroSphere.transform.localScale *= aggroRange;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        chasePlayer = true;
    }

    public void OnTriggerExit(Collider other)
    { 
        chasePlayer = false;
        returnToSpawnTimer = returnToSpawnDelay;
    }

    public void ReturnToSpawn()
    {
        Vector3 lookDirection = (transform.position - SpawnLocation).normalized;
        if ( lookDirection != Vector3.zero)
        {
            quaternion targetRotation = Quaternion.LookRotation(SpawnLocation, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, SpawnLocation, speed * Time.deltaTime);
        }
    }

    public override void Die() 
    {
        Destroy(gameObject);
    }
    public void patrol() { }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(this.transform.position, aggroRange);
    }
}
