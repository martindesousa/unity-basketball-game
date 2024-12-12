using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;

    public float jumpForce = 10f; // Jump force
    private bool isGrounded;

    public float speed = 5f; // Movement speed

    internal static bool dribbling = true; // if or not the player is dribbling
    internal static bool shooting = false; // if or not the player is shooting
    internal static bool onBall = true; // if or not the player is on the ball

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        
        //// Get input values
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");

        //// Calculate the movement vector
        //Vector3 movement = new Vector3(horizontal, 0f, vertical) * speed * Time.fixedDeltaTime;

        //// Move the player
        //rb.MovePosition(rb.position + movement);

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            onBall = true;
            dribbling = true;
            animator.SetTrigger("dribble");
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            onBall = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Check if the player is on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Check if the player left the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void Jump()
    {
        //Animate jump flag
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
