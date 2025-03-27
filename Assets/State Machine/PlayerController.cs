using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float duckSpeedMultiplier = 0.5f;
    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private float duckingHeight = 1f;

    [SerializeField] private float groundPoundForce = 15f;

    private Rigidbody rb;
    private CapsuleCollider playerCollider;
    private bool isGrounded;
    private bool isDucking;
    private bool isGroundPounding;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        standingHeight = playerCollider.height;

    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        moveDirection = moveDirection.normalized;

        HandleDucking();
        HandleJumping();
        HandleGroundPounding();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void CheckGrounded()
    {
        isGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            groundCheckDistance,
            groundLayer
            );
    }

    void MovePlayer()
    {
        float currentMoveSpeed = moveSpeed;

        if (isDucking)
        {
            currentMoveSpeed *= duckSpeedMultiplier;
        }

        Vector3 moveDirection = transform.forward * Input.GetAxis("Vertical") +
            transform.right * Input.GetAxis("Horizontal");

        moveDirection = moveDirection.normalized * currentMoveSpeed;
        moveDirection.y = rb.velocity.y;
    }

    void HandleDucking()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isDucking = !isDucking;

            if (isDucking)
            {
                playerCollider.height = duckingHeight;
                playerCollider.center = new Vector3(0, duckingHeight / 2, 0);
            }
            else
            {
                playerCollider.height = standingHeight;
                playerCollider.center = Vector3.zero;
            }
        }
    }

    void HandleGroundPounding()
    {
        if (!isGrounded && Input.GetKeyDown(KeyCode.X))
        {
            isGroundPounding = true;
            rb.AddForce(Vector3.down * groundPoundForce, ForceMode.Impulse);
        }

        if (isGroundPounding && isGrounded)
        {
            isGroundPounding = false;
        }
    }

    void HandleJumping()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

}
