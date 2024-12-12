using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCamera : MonoBehaviour
{
    public Transform player; 
    public Transform hoop; 
    public Transform gameCamera; 
    public float speed = 10f;  
    public const float rotationSpeed = 6f;
    private float currentRotationSpeed;

    private bool currentlyRotating; // Flag to track if rotation is in progress

    void LateUpdate()

    {
        if (player != null)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // Calculate the movement direction relative to the player's forward direction
            Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal;

            // Normalize the movement direction and apply speed
            moveDirection.Normalize();
            moveDirection = moveDirection.normalized * speed * Time.deltaTime;

            // Move the player
            transform.Translate(moveDirection, Space.World);
            UpdateCameraRotation(player.position);
        }
    }

    void UpdateCameraRotation(Vector3 playerPosition)
    {
        if ((player.position.x <= 10f && player.position.x >= -10f && player.position.z <= 20f && player.position.z >= -10f) ||
        (player.position.z <= 28f && (player.position.x > 10f || player.position.x < -10f))) //IN CORNERS 
        {
            currentRotationSpeed = rotationSpeed;
            currentlyRotating = true;  // Start rotation
        }
        else
        {
            currentRotationSpeed = rotationSpeed/5;
            currentlyRotating = true;  // Stop rotation
        }

        if (currentlyRotating)
        {
            Vector3 targetPosition = new Vector3(hoop.position.x, 0f, hoop.position.z - 2f);
            Vector3 direction = targetPosition - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, currentRotationSpeed * Time.deltaTime);
        }
    }
}
