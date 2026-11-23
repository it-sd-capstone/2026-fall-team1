using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float lifeTime = 15f;
    private Rigidbody rb;
    private bool hit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); 
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        if (!hit && rb.linearVelocity.sqrMagnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(rb.linearVelocity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hit) return;
        hit = true;

        //Hopefully stick arrow in target
        rb.isKinematic = true;
        transform.SetParent(collision.transform);

        collision.gameObject.GetComponent<Enemy>().AdjustHealth(-(int)rb.linearVelocity.magnitude);
    }
}
