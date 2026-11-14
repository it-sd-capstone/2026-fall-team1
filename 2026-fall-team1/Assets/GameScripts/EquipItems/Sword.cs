using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerCharacter;
    [SerializeField]
    private GameObject ItemHolder;
    [SerializeField]
    private float attackRange = 1f;
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private LayerMask enemyLayers;
    [SerializeField]
    private int attackDamage = 5;
    [SerializeField]
    private float attackDelay = 2f;
    [SerializeField]
    private Transform cameeraTransform;

    private Vector2 attackInput;
    private bool attacking = false;
    private float attackTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // When Sword is created, place it at the position of the ItemHolder
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 attackDirection = new Vector3(attackInput.x, 0f, attackInput.y);

        if (attackDirection.sqrMagnitude > 1f)
        {
            attackDirection.Normalize();
        }

        if (attackDirection.sqrMagnitude > 0f)
        {
            if (!attacking && attackTime <=0)
            {
                Debug.Log("Attack() called");
                Attack(attackDirection);
            }

        }

        if (attackTime > 0f)
        {
            attackTime -= Time.deltaTime;
            if (attackTime < 0f)
            {
                attacking = false;
            }
        }
    }

    private void OnAttack(InputValue input)
    {
        attackInput = input.Get<Vector2>();
    }

    private void Attack(Vector3 attackDirection)
    {
        // Delay Time between attacks
        attacking = true;
        attackTime = attackDelay;

        // rotate the player toward attack
        Vector3 forward = cameeraTransform.forward;
        Vector3 right = cameeraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 newAttackDirection = (forward * attackDirection.z + right * attackDirection.x).normalized;


        Quaternion targetRotation = Quaternion.LookRotation(newAttackDirection, Vector3.up);
        PlayerCharacter.transform.rotation = targetRotation;

        // Play the attack animation

        // Detect Enemies in range
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        // Damage all Enemies 
        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("enemy hit");
            enemy.GetComponent<Enemy>().AdjustHealth(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
