using System.Collections;
using UnityEditor.VersionControl;
using UnityEngine;

public class Duck : Enemy
{

    [SerializeField]
    private int flyHeight;
    [SerializeField]
    private GameObject ground;
    [SerializeField]
    private float attackRange = 2.5f;
    private bool attacking;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        attacking = false;
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        if (!attacking)
        {
            if (chasePlayer)
            {
                Chase();
            }
            else if (returnToSpawnTimer > 0)
            {
                ReturnToSpawnTimerUpdate();
            } else if (returnToSpawn)
            {
                ReturnToSpawn();
            }
        } else
        {
            AttackTimerUpdate();
        }
    }

    public void Chase()
    {
        Vector3 playerLocation = playerCharacter.transform.position;
        Vector3 lookDirection = (playerLocation - transform.position);
        if (Vector3.Distance(transform.position, playerLocation) < attackRange)
        {
            Debug.Log("attack called");
            Attack();
        }
        else if (lookDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(playerLocation, Vector3.up);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime
            transform.rotation = targetRotation;
            playerLocation = new Vector3(playerLocation.x, (playerLocation.y + flyHeight), playerLocation.z);
            transform.position = Vector3.MoveTowards(transform.position, playerLocation, speed * Time.deltaTime);
            
        }
    }

    public void Attack()
    {
        attacking=true;
        attackTime = attackDelay;
        float attackStartup = .5f;
        float launchForce = 10f;
        Debug.Log("Attack Started");

        // Play attack animation

        // Perform attack movement

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        Vector3 targetDirection = (playerCharacter.transform.position - transform.position).normalized;
        Vector3 launchVector = (targetDirection * launchForce);
        rb.AddForce(launchVector, ForceMode.Impulse);

        //Vector3 targetPlayer = playerCharacter.transform.position;
        //Quaternion targetRotation = Quaternion.LookRotation(targetPlayer, Vector3.up);
        //transform.rotation = targetRotation;
        ////yield return new WaitForSeconds(attackStartup);
        //transform.position = Vector3.MoveTowards(transform.position, targetPlayer, speed * 2 * Time.deltaTime);

        // Fly back up

        //yield return new WaitForSeconds(attackStartup);
        //targetRotation = Quaternion.LookRotation(-targetPlayer, Vector3.up);
        //transform.rotation = targetRotation;
        //transform.position = Vector3.MoveTowards(transform.position, -targetPlayer, speed * Time.deltaTime);

    }

    public void AttackTimerUpdate()
    {
        attackTime -= Time.deltaTime;
        if (attackTime <= attackDelay/2)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;
        }
        if (attackTime <= 0)
        {
            attacking = false;
            Debug.Log("Attack Finished");
        }
    }

    public void ReturnToSpawnTimerUpdate()
    {
        returnToSpawnTimer -= Time.deltaTime;
        if (returnToSpawnTimer <= 0)
        {
            returnToSpawn = true;
        }
    }

    public void swim() { }
    public void fly() { }
}
