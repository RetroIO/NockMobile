using UnityEngine;

public class GoalCelebration1 : MonoBehaviour
{
    [SerializeField] public ParticleSystem wireSphere;

    public void TriggerParticles()
    {
        Debug.Log("goal cele began");
        GetComponent<ParticleSystem>().Play();
        wireSphere.Emit(1);
    }
}
