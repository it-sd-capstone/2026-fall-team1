using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] private float moveSpeed;

    [Header("Jump settings")]
    [SerializeField] private float jumpHeight;

    [Header("Ground detection")]
    [SerializeField] private float groundDistance;

    private Rigidbody rb;
    private Vector2 moveInput;
    private bool jumpPush;

    private void Awake()
    { rb = GetComponent<Rigidbody>(); }

    private void Update()
    { moveInput = Vector2.zero;
        if (Keyboard.current != null)
        { if (Keyboard.current.wKey.isPressed) moveInput.y += 1;
          if (Keyboard.current.aKey.isPressed) moveInput.x -= 1;
          if (Keyboard.current.sKey.isPressed) moveInput.y -= 1;
          if (Keyboard.current.dKey.isPressed) moveInput.x += 1;
          if (Keyboard.current.spaceKey.wasPressedThisFrame) jumpPush = true;
        }
    }

    private void FixedUpdate()
    { Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

        Vector3 velocity = rb.linearVelocity;
        velocity.x = movement.x * moveSpeed;
        velocity.z = movement.z * moveSpeed;
        rb.linearVelocity = velocity;

        if (jumpPush && IsGrounded())
            { rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse); }
            jumpPush = false;
    }

    private bool IsGrounded()
    { return Physics.Raycast(transform.position, Vector3.down, groundDistance); }

}
