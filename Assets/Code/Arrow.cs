using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float lifetime = 1.5f;
    private float spawnTime;
    

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        spawnTime = Time.time;
		
		
		
    }

    private void FixedUpdate()
    {
        // Calculate the rotation angle based on the velocity direction
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;

        // Set the rotation of the arrow based on the velocity direction
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (Time.time - spawnTime >= lifetime)
        {
            Destroy(gameObject);
        }
    }
	
	private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is on the "Player" layer
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Ignore collision with the player object
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);
        }

        
    }
	
}  
