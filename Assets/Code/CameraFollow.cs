using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTarget; // The player target GameObject to follow
    public string ballTag = "Ball"; // Tag assigned to the ball GameObject
    public Vector3 offset = new Vector3(0f, 5f, -10f); // Offset from the target
    public float smoothSpeed = 0.125f; // Smoothing factor for camera movement

    private Transform ballTarget; // Reference to the dynamically found ball target

    private void Start()
    {
        // Find the ball GameObject based on its tag
        GameObject ballObject = GameObject.FindGameObjectWithTag(ballTag);
        if (ballObject != null)
        {
            ballTarget = ballObject.transform;
        }
        else
        {
            Debug.LogWarning("Ball GameObject not found!");
        }
    }

    private void LateUpdate()
    {
        // Calculate the average position between player and ball targets
        Vector3 averagePosition = playerTarget.position;
        if (ballTarget != null)
        {
            averagePosition = (playerTarget.position + ballTarget.position) / 2f;
        }

        Vector3 desiredPosition = averagePosition + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(playerTarget);
    }
}
