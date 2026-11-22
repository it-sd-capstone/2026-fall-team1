using UnityEngine;

public class WindGust : MonoBehaviour
{
    [SerializeField]
    public float riseSpeed = 3f;
    [SerializeField]
    public float duration = 1.0f;

    void Start()
    {
        Destroy(gameObject, duration);

    }

    void Update()
    {
        transform.Translate(Vector3.up * riseSpeed * Time.deltaTime);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Character"))
        {
            collision.transform.SetParent(transform);
        }
    }
 private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Character"))
        {
            // Only unparent if this wind gust is still the parent
            if (collision.transform.parent == transform)
            {
                collision.transform.SetParent(null);
            }
        }
    }
    private void OnDestroy()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Player"))
            {
                child.SetParent(null);
            }
        }

    }
}
