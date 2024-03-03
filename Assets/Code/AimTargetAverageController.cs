using UnityEngine;
using Cinemachine;

public class AimTargetAverageController : MonoBehaviour
{
    public Transform playerTransform;
    public Transform ballTransform;
    public CinemachineVirtualCamera virtualCamera;

    private void Update()
    {
        if (playerTransform == null || ballTransform == null || virtualCamera == null)
            return;

        // Calculate the average position between player and ball
        Vector3 averagePosition = (playerTransform.position + ballTransform.position) / 2f;

        // Set the virtual camera's aim target to the calculated average position
        virtualCamera.GetCinemachineComponent<CinemachineComposer>().m_TrackedObjectOffset = averagePosition - virtualCamera.transform.position;
    }
}
