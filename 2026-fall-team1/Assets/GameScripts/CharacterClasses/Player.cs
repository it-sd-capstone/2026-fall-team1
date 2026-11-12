using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class Player : Character
{
    private int lives;
    private int coins;
    [SerializeField]
    private float jumpForce = 5;

    //private Vector2 moveVector;
    private Vector2 moveInput;
    [Header("References")]
    //private Rigidbody rb;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CharacterController controller;

    private float groundedSlopeForce = -2f;
    private Vector3 playerVelocity;

    // Need to add arrays for unlocked items and equiped items after EquipItem is completed

    void Awake()
    {
        if (cameraTransform == null)
        { cameraTransform = Camera.main.transform; }
        if (controller == null)
        { controller = GetComponent<CharacterController>(); }
    }
    void Update()
    {
        //Vector3 movement = new Vector3(moveVector.x, 0f, moveVector.y);
        //movement.Normalize();
        ////transform.Translate(speed * movement * Time.deltaTime);
        ////transform.position += movement * Time.deltaTime;
        //Debug.Log("movement vector: " +  movement);

        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = groundedSlopeForce;
        }

        Vector3 inputDirection = new Vector3(moveInput.x, 0f, moveInput.y);

        if (inputDirection.sqrMagnitude > 1f)
        {
            inputDirection.Normalize();
        }

        if (inputDirection.sqrMagnitude > 0f)
        {
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            camForward.y = 0f;
            camRight.y = 0f;

            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDirection = (camForward * inputDirection.z) + (camRight * inputDirection.x);
            controller.Move(moveDirection * speed * Time.deltaTime);
        }

        playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump()
    {
        if (controller.isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
        }
    }

    public void DoubleJump() { }

    public void ChangeItem() { }


    public void MoveCamera() { }
    public void CenterCamera() { }
    public void Respawn() { }
    public void CheckCoins() { }
    //public void Move(InputAction.CallbackContext ctx, InputValue value)
    //{
    //    if (ctx.started)
    //    {
    //        moveInput = value.Get<Vector2>();

    //        float horizontal = moveInput.x;
    //        float vertical = moveInput.y;

    //        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

    //        if (inputDirection.magnitude >= 0.1f)
    //        {

    //            Vector3 camForward = cameraTransform.forward;
    //            Vector3 camRight = cameraTransform.right;

    //            camForward.y = 0f;
    //            camRight.y = 0f;

    //            camForward.Normalize();
    //            camRight.Normalize();

    //            Vector3 moveDirection = (camForward * inputDirection.z) + (camRight * inputDirection.x);

    //            controller.Move(moveDirection * speed * Time.deltaTime);



    //        }
    //    }
    //}
    
}
