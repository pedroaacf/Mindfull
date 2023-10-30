using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Transform orientantion;
    public float horizontalInput, verticalInput;
    public Vector3 moveDirection;
    public Rigidbody rb;

    public float playerHeight;
    public LayerMask isGround;
    public bool grounded;
    public float groundDrag;

    public float jumpForce, jumpCooldown, airMultiplier;
    public bool readyToJump;
    public KeyCode jumpKey = KeyCode.Space;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded){
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientantion.forward * verticalInput + orientantion.right * horizontalInput;

        if (grounded) {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }else if (!grounded) {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void SpeedControll()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        
        if (flatVel.magnitude > moveSpeed) {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, isGround);
        MyInput();
        SpeedControll();
        
        if (grounded) {
            rb.drag = groundDrag;
        }else {
            rb.drag = 0;
        }
    }
    

}
