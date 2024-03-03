using UnityEngine;

public class HitFlash : MonoBehaviour
{
    public ParticleSystem hitParticles; // Reference to the particle system component on the same prefab

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision detected");
        if (collision.gameObject.CompareTag("Arrow"))
        {
            Debug.Log("Arrow hit ball");
            // Play the particle system
            if (hitParticles != null)
            {
                Debug.Log("particle playing");
                hitParticles.Play();
            }
            else
            {
                Debug.Log("hit particle was null");
            }

            // Add any other logic you want to happen when the ball is hit by an arrow

            // Destroy the arrow
            //Destroy(collision.gameObject);
        }
    }
}
