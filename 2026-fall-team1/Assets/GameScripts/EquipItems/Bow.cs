using UnityEngine;
using UnityEngine.InputSystem;

public class Bow : MonoBehaviour
{
    [Header("Settings")]
    public GameObject arrowPrefab;
    public Transform spawnPoint;
    public float cooldown = 1f;

    [Header("Firing Settings")]
    public float minForce = 10f;
    public float maxForce = 40f;
    public float maxChargeTime = 1.5f;

    private float chargeTimer;
    private float nextUseTime = 0f;
    private bool isDrawing = false;

    private void OnBowUse(InputValue value)
    {
        bool isPressed = value.Get<float>() > 0.5f;

        if (isPressed)
        {
            if (!isDrawing && Time.time >= nextUseTime)
            {
                isDrawing = true;
                chargeTimer = 0f;
            }

            return;
        }

        if (isDrawing)
        {
            isDrawing = false;
            FireArrow();
            nextUseTime = Time.time + cooldown;
        }
    }

    private void FireArrow()
    {
        float chargePercent = Mathf.Clamp01(chargeTimer / maxChargeTime);
        float launchForce = Mathf.Lerp(minForce, maxForce, chargePercent);
        if (arrowPrefab == null)
        {
            Debug.LogError("Hey! you forgot to select the arrowPrefab!");
            return;
        }

        Transform firingPoint = spawnPoint != null ? spawnPoint : transform;
        GameObject arrow = Instantiate(arrowPrefab, firingPoint.position, firingPoint.rotation);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(firingPoint.forward * launchForce, ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDrawing)
        {
            chargeTimer = Mathf.Min(chargeTimer + Time.deltaTime, maxChargeTime);
        }
    }
}
