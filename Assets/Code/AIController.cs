using UnityEngine;

public class AIController : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float fireInterval = 2.0f;
    public MatchController matchController;
	
    private float lastFireTime;

    private void Start()
    {
        lastFireTime = Time.time;
    }

    private void Update()
    {
		if (matchController.EnableAI == false)
        {
            return;
        }
		else if (Time.time - lastFireTime >= fireInterval)
		{
			FireArrow();
			lastFireTime = Time.time;
		}
	}

    private void FireArrow()
		{
			// Instantiate an arrow towards the adjusted aim spot
			GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
			Vector2 ballPosition = matchController.GetBallPosition();
			
			// Apply an offset to the ball's position to raise the aim spot
			Vector2 offset = new Vector2(0f, 0.5f); // Adjust the offset as needed
			Vector2 adjustedAimSpot = ballPosition + offset;
			
			Vector2 direction = (adjustedAimSpot - (Vector2)transform.position).normalized;

			// Apply force to the arrow
			Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();

			float additionalForce = Random.Range(15f, 20f);
			arrowRb.velocity = direction * arrowRb.velocity.magnitude;

			// Apply additional random force
			arrowRb.AddForce(direction * additionalForce, ForceMode2D.Impulse);
		}



}
