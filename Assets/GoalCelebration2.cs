using UnityEngine;

public class GoalCelebration2 : MonoBehaviour
{
    [SerializeField] public ParticleSystem p2WireSphere;

    public void TriggerParticles()
    {
        Debug.Log("goal2 cele began");
        GetComponent<ParticleSystem>().Play();
        p2WireSphere.Emit(1);
    }
}
