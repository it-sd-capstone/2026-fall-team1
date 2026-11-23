using System;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class Player : Character
{
    private int lives;
    private int coins;
    [SerializeField]
    private float jumpForce = 5;
    [SerializeField] public float playerTurnSpeed = 120f;

    //private Vector2 moveVector;
    private Vector2 moveInput;
    [Header("References")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CharacterController controller;
    [SerializeField]
    private GameObject ItemHolder;
    [SerializeField] private Animator animator;

    private WeaponManager weaponManager;

    private float groundedSlopeForce = -2f;
    private Vector3 playerVelocity;
    private float moveMagnituede;

    // Need to add arrays for unlocked items and equiped items after EquipItem is completed

    protected override void Awake()
    {
        base.Awake();

        if (cameraTransform == null)
        { cameraTransform = Camera.main.transform; }
        if (controller == null)
        { controller = GetComponent<CharacterController>(); }
        weaponManager = ItemHolder.GetComponent<WeaponManager>();
        if (animator == null)
        { animator = GetComponentInChildren<Animator>(); }
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

        moveMagnituede = inputDirection.magnitude;

        if (inputDirection.sqrMagnitude > 0f)
        {
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            camForward.y = 0f;
            camRight.y = 0f;

            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDirection = (camForward * inputDirection.z) + (camRight * inputDirection.x);

            if (moveDirection.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    targetRotation,
                    playerTurnSpeed * Time.deltaTime 
                    );
            }

            controller.Move(moveDirection * speed * Time.deltaTime);
        }

        playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);


        if (animator != null)
        {
            animator.SetFloat("Speed", moveInput.magnitude);
            // add in if we use sprint like the animator
            // animator.SetBool("Sprint", sprinting);
            animator.SetBool("Grounded", controller.isGrounded);
            animator.SetFloat("VerticalSpeed", playerVelocity.y);
        }
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

    public void OnEquip1()
    {
        weaponManager.EquipWeapon(0);
    }
    public void OnEquip2()
    {
        weaponManager.EquipWeapon(1);
    }
    public void OnEquip3()
    {
        weaponManager.EquipWeapon(2);
    }
    public void OnEquip4()
    {
        weaponManager.EquipWeapon(3);
    }

    public void DoubleJump() { }

    public void OnCameraFocus() 
    {
        cameraTransform.GetComponent<CinemachineOrbitalFollow>().HorizontalAxis.Value = transform.eulerAngles.y;
    }
    public void Respawn() { }
    public void CheckCoins() { }


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

    // added public constructors for save access
    public int Lives { get { return lives; } set { lives = value; } }
    public int Coins { get { return coins; } set { coins = value; } }

}
