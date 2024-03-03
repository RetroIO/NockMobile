using UnityEngine;
using Cinemachine;

public class TargetGroupManager : MonoBehaviour
{
    public CinemachineTargetGroup targetGroup; // Reference to the Cinemachine Target Group
    public Transform lateInstantiatedTarget;   // Reference to the dynamically instantiated target
	public MatchController matchController;
	
	public bool targetAdded = false;

    private void Update()
    {
		if (targetAdded)
		{
			return;
		}
        // Add the late-instantiated target to the Target Group
        else if (targetGroup != null && matchController.IsBallSpawned())
        {
            targetGroup.AddMember(lateInstantiatedTarget, 2f, 0f);
			targetAdded = true;
        }
    }
}
