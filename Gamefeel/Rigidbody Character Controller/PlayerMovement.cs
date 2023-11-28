using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float moveMultiplier = 10f;
    [SerializeField] float airMultiplier = 0.4f;
    [SerializeField] Transform orientation;
    float xMovement;
    float zMovement;
    Vector3 moveDirection;

    [Header("Drag")]
    [SerializeField] float groundDrag = 6f;
    [SerializeField] float airDrag = 2f;

    [Header("Jump")]
    [SerializeField] float jumpForce = 20f;
    [SerializeField] float extraJumpForce = 5f;
    [SerializeField] float jumpInputTime = 0.2f;
    [SerializeField] float gravityForce = 10f;
    float jumpTimer = 0f;

    [Header("Coyote Time")]
    [SerializeField] float coyoteTime = 0.05f;
    float coyoteTimer = 0;

    [Header("Ground Detection")]
    [SerializeField] Transform groundPos;
    [SerializeField] LayerMask groundCheckLayerMask;
    [SerializeField] float groundCheckRadius = 0.45f;
    bool isGrounded;

    [Header("Slope Detection")]
    RaycastHit slopeHit;
    Vector3 slopeMoveDirection;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundPos.position, groundCheckRadius, groundCheckLayerMask);

        GetMoveInput();
        ControlDrag();
        SetJumpTimer();
        CoyoteTime();
        HandleJump();

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }
    void HandleJump()
    {
        if (isGrounded)
        {
            //print("Testing jump");
            if (jumpTimer > 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpTimer = 0;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
        else if (coyoteTimer > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    void Gravity()
    {
        float clampedGravity = Mathf.Clamp(rb.velocity.y, -1.5f, 0);

        if (!isGrounded && rb.velocity.y < 0)
        {
            rb.AddForce(Vector3.down * gravityForce * Mathf.Pow(clampedGravity / -1.5f, 1.5f), ForceMode.Force);
        }
    }

    void CoyoteTime()
    {
        if (isGrounded)
        {
            coyoteTimer = coyoteTime;
        }
        else if (coyoteTimer > 0)
        {
            coyoteTimer -= Time.deltaTime;
            if (coyoteTimer < 0)
            {
                coyoteTimer = 0;
            }
        }
    }

    void SetJumpTimer()
    {
        if (!isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            jumpTimer = jumpInputTime;
        }

        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
            if (jumpTimer < 0) jumpTimer = 0;
        }
    }

    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    void GetMoveInput()
    {
        xMovement = Input.GetAxisRaw("Horizontal");
        zMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * zMovement + orientation.right * xMovement;
    }

    bool IsOnSlope()
    {
        if (Physics.Raycast(groundPos.position, Vector3.down, out slopeHit, groundCheckRadius))
        {
            return !(slopeHit.normal == Vector3.up);
        }
        else
        {
            return false;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        Gravity();

        float clampedJumpForce = Mathf.Clamp(rb.velocity.y, 0, 4);
        if (Input.GetKey(KeyCode.Space) && rb.velocity.y > 0)
        {
            rb.AddForce(Vector3.up * extraJumpForce * Mathf.Pow(clampedJumpForce / 4f, 1.2f), ForceMode.Acceleration);
        }
    }

    void MovePlayer()
    {
        if (isGrounded)
        {
            if (IsOnSlope())
            {
                GetComponent<Rigidbody>().AddForce(slopeMoveDirection.normalized * moveSpeed * moveMultiplier, ForceMode.Acceleration);
            }
            else
            {
                GetComponent<Rigidbody>().AddForce(moveDirection.normalized * moveSpeed * moveMultiplier, ForceMode.Acceleration);
            }
        }
        else
        {
            GetComponent<Rigidbody>().AddForce(moveDirection.normalized * moveSpeed * airMultiplier * moveMultiplier, ForceMode.Acceleration);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundPos.position, groundCheckRadius);
    }
}
