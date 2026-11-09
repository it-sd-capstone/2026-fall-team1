using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] private float moveSpeed;

    [Header("Jump settings")]
    [SerializeField] private float jumpHeight;

    //Uses CharacterController instead of Rigidbody for movement, as it allows for better control over the player's movement and collision detection.
    [Header("Camera Settings")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CharacterController controller;

    private float groundedSlopeForce = -2f;
    private Vector3 playerVelocity;



    private void Awake()
    { 

        if (cameraTransform == null)
        { cameraTransform = Camera.main.transform; }
        if (controller == null)
        { controller = GetComponent<CharacterController>(); }

    }

    private void Update()
    {
        //Check if the player is grounded and apply a small downward force to keep them grounded on slopes
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = groundedSlopeForce;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;



        //Move the player based on the camera's forward and right direction, allowing for movement relative to the camera's orientation
        if (inputDirection.magnitude >= 0.1f)
        {

            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            camForward.y = 0f;
            camRight.y = 0f;

            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDirection = (camForward * inputDirection.z) + (camRight * inputDirection.x);

            controller.Move(moveDirection * moveSpeed * Time.deltaTime);



        }

        //Code for jumping, uses GetButtonDown instead of Keyboard.current.spaceKey.wasPressedThisFrame to allow for controller input as well
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }

        playerVelocity.y += Physics.gravity.y * Time.deltaTime;

        controller.Move(playerVelocity * Time.deltaTime);



    }



}
