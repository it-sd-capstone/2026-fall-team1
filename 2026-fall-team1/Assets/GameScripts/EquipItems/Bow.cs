using UnityEngine;
using UnityEngine.InputSystem;

public class Bow : MonoBehaviour
{
    [Header("References")]
    public Transform playerRoot; // Assign your Player's root Transform here
    public GameObject arrowPrefab;
    public Transform spawnPoint;

    [Header("Firing Settings")]
    public float cooldown = 1f;
    public float minForce = 10f;
    public float maxForce = 40f;
    public float maxChargeTime = 1.5f;
    public float turnSpeed = 720f; // Degrees per second (set very high like 3000 for instant snap)

    private Vector2 aimInput;
    private Vector3 lastAimDirection;
    private float chargeTimer;
    private float nextUseTime;
    private bool isDrawing;

    private void Awake()
    {
        if (playerRoot == null)
        {
            playerRoot = transform.root;
        }
    }

    // Matches an Input Action named "BowAim" (Vector2 Composite for I, J, K, L)
    private void OnAttack(InputValue value)
    {
        aimInput = value.Get<Vector2>();

        // 1. Player is holding at least one I/J/K/L key
        if (aimInput.sqrMagnitude > 0.01f)
        {
            // Store the 3D world direction (supports diagonals like I+L)
            lastAimDirection = new Vector3(aimInput.x, 0f, aimInput.y).normalized;

            if (!isDrawing && Time.time >= nextUseTime)
            {
                isDrawing = true;
                chargeTimer = 0f;
            }
        }
        // 2. Player released all I/J/K/L keys
        else if (isDrawing)
        {
            isDrawing = false;

            // Snap rotation to the exact final aim direction before firing
            if (lastAimDirection.sqrMagnitude > 0.01f)
            {
                playerRoot.rotation = Quaternion.LookRotation(lastAimDirection);
            }

            FireArrow();
            nextUseTime = Time.time + cooldown;
        }
    }

    private void Update()
    {
        if (isDrawing)
        {
            chargeTimer = Mathf.Min(chargeTimer + Time.deltaTime, maxChargeTime);

            // Continuously turn the player toward the held I/J/K/L direction while charging
            if (lastAimDirection.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lastAimDirection);
                playerRoot.rotation = Quaternion.RotateTowards(
                    playerRoot.rotation,
                    targetRotation,
                    turnSpeed * Time.deltaTime
                );
            }
        }
    }

    private void FireArrow()
    {
        if (arrowPrefab == null)
        {
            Debug.LogError("Hey! You forgot to assign the arrowPrefab!", this);
            return;
        }

        float chargePercent = Mathf.Clamp01(chargeTimer / maxChargeTime);
        float launchForce = Mathf.Lerp(minForce, maxForce, chargePercent);

        Transform firingPoint = spawnPoint != null ? spawnPoint : playerRoot;
        GameObject arrow = Instantiate(arrowPrefab, firingPoint.position, firingPoint.rotation);

        if (arrow.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.AddForce(firingPoint.forward * launchForce, ForceMode.Impulse);
        }
    }
}