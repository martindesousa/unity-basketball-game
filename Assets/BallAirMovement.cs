using UnityEngine;
using System.Collections;

public class BallAirMovement : MonoBehaviour
{
    public const float naturalParabolaHeight = 10.0f;
    internal float currentParabolaHeight = 10.0f;
    public Transform targetHoop;
    public Transform layupTarget;
    public float speed = 1.0f;
    public float arrivalThreshold = 0.1f;



    public void Layup()
    {
        if (layupTarget == null)
        {
            Debug.LogError("Layup Target is not assigned!");
            return;
        }

        currentParabolaHeight = 5.0f;

        Vector3 startPos = transform.position; // Initial position of the object
        Vector3 targetPos = layupTarget.position; // Target position of the layup

        // Calculate the distance between start and layup target positions
        float distance = Vector3.Distance(startPos, targetPos);

        // Calculate the midpoint between start and layup target positions
        Vector3 midPoint = (startPos + targetPos) / 2f;
        midPoint += Vector3.up * currentParabolaHeight; // Add height to create a parabolic motion

        // Move the object in a parabolic arc to the layup target
        StartCoroutine(BallToBackboard(startPos, midPoint, targetPos, distance));
    }

    public void Jumpshot()
    {
        MoveInParabola();
    }

    // Coroutine to move the object in a parabolic arc to the layup target and then linearly to the target hoop
    private IEnumerator BallToBackboard(Vector3 startPos, Vector3 midPoint, Vector3 targetPos, float distance)
    {
        float startTime = Time.time;

        while (transform.position != targetPos)
        {
            // Calculate the current time based on the total distance and speed
            float currentTime = (Time.time - startTime) * distance * 0.75f * speed;

            // Normalize the time to a value between 0 and 1
            float normalizedTime = currentTime / distance;

            // Calculate the position along the curve using Bezier curve formula
            Vector3 currentPos = CalculateBezierPoint(normalizedTime, startPos, midPoint, targetPos);

            // Move the object to the calculated position
            transform.position = currentPos;

            if (Vector3.Distance(transform.position, targetPos) < arrivalThreshold)
            {
                break; // Exit the loop if the ball has arrived at the target
            }

            yield return null;
        }

        // Move linearly towards the target hoop after reaching the layup target
        StartCoroutine(BackboardToHoop(targetPos, targetHoop.position));
    }

    // Coroutine to move the object linearly to the target hoop
    private IEnumerator BackboardToHoop(Vector3 startPos, Vector3 targetPos)
    {
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(startPos, targetPos);

        while (transform.position != targetPos)
        {
            // Calculate the distance covered based on the elapsed time and speed
            float distCovered = (Time.time - startTime) * 5f * speed;

            // Calculate the fraction of journey completed
            float fractionOfJourney = distCovered / journeyLength;

            // Set the position as a fraction of the distance between the start and end points
            transform.position = Vector3.Lerp(startPos, targetPos, fractionOfJourney);

            if (Vector3.Distance(transform.position, targetPos) < arrivalThreshold)
            {
                break; // Exit the loop if the ball has arrived at the target
            }

            yield return null;
        }
    }

    // Method to move the object in a parabolic motion towards the target transform
    public void MoveInParabola()
    {
        if (targetHoop == null)
        {
            Debug.LogError("Target Hoop is not assigned!");
            return;
        }

        currentParabolaHeight = naturalParabolaHeight;

        Vector3 startPos = transform.position; 
        Vector3 targetPos = targetHoop.position; 

        // Calculate the distance between start and target positions
        float distance = Vector3.Distance(startPos, targetPos);

        // Calculate the midpoint between start and target positions
        Vector3 midPoint = (startPos + targetPos) / 2f;
        midPoint += Vector3.up * currentParabolaHeight; // Add height to create a parabolic motion

        // Move the object in a parabolic arc
        StartCoroutine(MoveBall(startPos, midPoint, targetPos, distance));
    }

    // Coroutine to move the object in a parabolic arc
    private IEnumerator MoveBall(Vector3 startPos, Vector3 midPoint, Vector3 targetPos, float distance)
    {
        float startTime = Time.time;

        while (transform.position != targetPos)
        {
            // Calculate the current time based on the total distance and speed
            float currentTime = (Time.time - startTime) * distance * 0.5f * speed;

            // Normalize the time to a value between 0 and 1
            float normalizedTime = currentTime / distance;

            // Calculate the position along the curve using Bezier curve formula
            Vector3 currentPos = CalculateBezierPoint(normalizedTime, startPos, midPoint, targetPos);

            // Move the object to the calculated position
            transform.position = currentPos;

            if (Vector3.Distance(transform.position, targetPos) < arrivalThreshold)
            {
                break; // Exit the loop if the ball has arrived at the target
            }

            yield return null;
        }
    }

    // Calculate a point along a Bezier curve
    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        // B(t) = (1 - t)^2 * P0 + 2 * (1 - t) * t * P1 + t^2 * P2
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        return p;
    }
}
