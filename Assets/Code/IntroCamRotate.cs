using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IntroCamRotate : MonoBehaviour
{
    public CinemachineVirtualCamera vCam;
    public float rotationSpeed = 30.0f;
    public float rotationDuration = 5.0f;

    private CinemachineOrbitalTransposer orbitalTransposer;

    private void Start()
    {
        if (vCam != null)
        {
            orbitalTransposer = vCam.GetCinemachineComponent<CinemachineOrbitalTransposer>();
            if (orbitalTransposer != null)
            {
                StartCoroutine(AutoRotate());
            }
            else
            {
                Debug.LogError("CinemachineOrbitalTransposer not found in the Virtual Camera.");
            }
        }
        else
        {
            Debug.LogError("CinemachineVirtualCamera not assigned.");
        }
    }

    private IEnumerator AutoRotate()
    {
        float elapsedTime = 0.0f;
        float startAngle = orbitalTransposer.m_XAxis.Value;

        while (elapsedTime < rotationDuration)
        {
            float angle = Mathf.LerpAngle(startAngle, startAngle + 360.0f, elapsedTime / rotationDuration);
            orbitalTransposer.m_XAxis.Value = angle;
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
