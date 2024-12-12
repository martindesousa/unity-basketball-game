using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    public Transform playerHand; // Reference to the player character's transform
    public Vector3 offset = new Vector3(1f, 0f, 0f); // Offset from the player
    public float dribbleStartingHeight = 2f; // The height the ball will dribble
    public float dribbleDuration = 1f; // The duration of each dribble
    public float dribblesPerSecond = 1f; // Number of dribbles per second

    private Vector3 startPosition; // The starting position of the ball
    private float timer = 0f; // Timer to track the time since the last dribble
    private Coroutine dribbleCoroutine;

    internal static Rigidbody rb;
    private Collider ballCollider;

    private readonly Vector3 shootingOffset = new Vector3(0f, 2.5f, 0f); // Offset from the player for shooting


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        ballCollider = GetComponent<Collider>();
        

        if (playerHand == null)
        {
            Debug.LogError("Player hand transform is not assigned.");
            return;
        }

        // Save the starting position of the ball
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        Vector3 newPosition = playerHand.position + offset;
        if (PlayerBehavior.onBall)
        {
            ballCollider.enabled = false;
            rb.useGravity = false;
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 10f);
            transform.position = newPosition;
        }
        else
        {
            ballCollider.enabled = true;
            rb.useGravity = true;
        }

        if (PlayerBehavior.shooting)
        {
            newPosition = playerHand.position + offset + shootingOffset;
        }

        if (PlayerBehavior.dribbling)
        {
            // Increment the timer
            timer += Time.deltaTime;

            // Check if it's time to dribble
            if (timer >= 1 / dribblesPerSecond)
            {
                // Reset the timer
                timer = 0f;

                // Stop any existing dribble coroutine to ensure only one is running at a time
                if (dribbleCoroutine != null)
                {
                    StopCoroutine(dribbleCoroutine);
                }

                // Start a new dribble coroutine
                dribbleCoroutine = StartCoroutine(Dribble());
            }
        }
        
        
    }

    IEnumerator Dribble()
    {
        ballCollider.enabled = false;
        Vector3 initialPosition = transform.position;

        // Downward movement
        float elapsedTime = 0f;
        Vector3 groundPosition = new Vector3(playerHand.position.x, 0f, playerHand.position.z);
        while (elapsedTime < dribbleDuration / 2)
        {
            groundPosition = new Vector3(playerHand.position.x, 0f, playerHand.position.z);

            transform.position = Vector3.Lerp(initialPosition, groundPosition, elapsedTime / (dribbleDuration / 2));

            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the ball reaches the ground position
        transform.position = groundPosition;

        // Update the player's position again for the upward movement
        Vector3 adjustedBallPosition = playerHand.position + offset;
        Vector3 handPosition = new Vector3(adjustedBallPosition.x, dribbleStartingHeight, adjustedBallPosition.z);

        // Perform the upward movement
        elapsedTime = 0f;
        while (elapsedTime < dribbleDuration / 2)
        {
            adjustedBallPosition = playerHand.position + offset;
            handPosition = new Vector3(adjustedBallPosition.x, dribbleStartingHeight, adjustedBallPosition.z);
            // Update the position of the ball
            transform.position = Vector3.Lerp(groundPosition, handPosition, elapsedTime / (dribbleDuration / 2));

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the ball reaches the hand position
        transform.position = handPosition;
        ballCollider.enabled = true;
    }
}
