using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private int extraJumps;

    [Header("Groundcheck")]
    [SerializeField] private Transform groundcheck;
    [SerializeField] private float groundcheckRad;
    [SerializeField] private LayerMask whatIsGround;

    private float moveInput;
    private Rigidbody2D rb;

    private bool isGrounded;
    private int remainingJumps;

    // Start is called before the first frame update
    void Start()
    {
        remainingJumps = extraJumps;
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundcheck.position, groundcheckRad, whatIsGround);

        MovePlayer();
    }

    private void MovePlayer()
    {
        Debug.Log(isGrounded);
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            remainingJumps = extraJumps;
        }

        if (Input.GetKeyDown(KeyCode.Space) && remainingJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            remainingJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && remainingJumps == 0 && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }
}
