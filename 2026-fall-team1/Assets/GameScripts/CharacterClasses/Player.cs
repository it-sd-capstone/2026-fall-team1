using System;
using TMPro;
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
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CharacterController controller;

    private float groundedSlopeForce = -2f;
    private Vector3 playerVelocity;

    // Need to add arrays for unlocked items and equiped items after EquipItem is completed

     protected override void Awake()
    {
         base.Awake();

        if (cameraTransform == null)
        { cameraTransform = Camera.main.transform; }
        if (controller == null)
        { controller = GetComponent<CharacterController>(); }
    }
    void Update()
    {

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
    public void OnMove(InputValue value)
    {
        Debug.Log("OnMove called");
        moveInput = value.Get<Vector2>();
        Debug.Log("Move Received: " + moveInput);

    }

    public void OnJump()
    {
        Debug.Log("OnJump called");
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

    
}
