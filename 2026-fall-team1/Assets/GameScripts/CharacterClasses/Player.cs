using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    private int lives;
    private int coins;
    [SerializeField]
    private float jumpForce = 110f;
    private Rigidbody rb;
    private Vector2 moveVector;
    // Need to add arrays for unlocked items and equiped items after EquipItem is completed

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Vector3 movement = new Vector3(moveVector.x, 0f, moveVector.y);
        movement.Normalize();
        //transform.Translate(speed * movement * Time.deltaTime);
        //transform.position += movement * Time.deltaTime;
        Debug.Log("movement vector: " +  movement);
    }


    public void ChangeItem() { }
    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            rb.AddForce(new Vector3(0f, jumpForce, 0f));
        }
    }
    public void DoubleJump() { }
    public void MoveCamera() { }
    public void CenterCamera() { }
    public void Respawn() { }
    public void CheckCoins() { }
    public void Move(InputAction.CallbackContext ctx)
    {
        moveVector = ctx.ReadValue<Vector2>();
    }
    
}
