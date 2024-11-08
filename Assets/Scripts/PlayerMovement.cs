using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rbPlayer;
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 15f;
    [SerializeField] bool isJump;
    [SerializeField] bool inFloor = true;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    private void Awake()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        inFloor = Physics2D.Linecast(transform.position, groundCheck.position, groundLayer);
        Debug.DrawLine(transform.position, groundCheck.position, Color.blue);

        if (Input.GetButtonDown("Jump") && inFloor)
            isJump = true;
        else if (Input.GetButtonUp("Jump") && rbPlayer.velocity.y > 0)
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, rbPlayer.velocity.y * 0.5f);
    }
    private void FixedUpdate()
    {
        Move();
        JumpPlayer();
    }

    void Move()
    {
        float xMove = Input.GetAxis("Horizontal");
        rbPlayer.velocity = new Vector2(xMove * speed, rbPlayer.velocity.y);

        if (xMove > 0 )
        {
            transform.eulerAngles = new Vector2(0, 0);
        }else if (xMove < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }
    void JumpPlayer()
    {
        if (isJump)
        {
            rbPlayer.velocity = Vector2.up * jumpForce;
            isJump = false;
        }
    }
}

    
