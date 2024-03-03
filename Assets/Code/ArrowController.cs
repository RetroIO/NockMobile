using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float power = 5f;
    public float maxDrag = 4f;
    public Rigidbody2D rb;
    public float maxLineLength = 2f; // Maximum length of the direction indicator line
    public GameObject arrowPrefab; // Reference to the arrow prefab
	public GameObject arrowDirectionIndicator;
	public GameObject arrowDirectionIndicatorSprite;
	public Color startColor;
	public Color endColor;
	public Vector3 rotationOffset = Vector3.zero;
	
	
    Rect leftSide;
    Rect rightSide;
    Vector3 dragStartPos;
    Touch touch;

    private GameObject spawnedArrow; // Reference to the spawned arrow
    

    private void Start()
    {
        leftSide = new Rect(0, 0, Screen.width / 2, Screen.height);
        rightSide = new Rect(Screen.width / 2, 0, Screen.width / 2, Screen.height);
		
		arrowDirectionIndicatorSprite.transform.localScale = new Vector3(0f, 0.0f, 1f);
		
    }
	
	
	

    private void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            touch = Input.GetTouch(i);

            if (touch.phase == TouchPhase.Began)
            {
                if (rightSide.Contains(touch.position))
                {
                    DragStart();
                }
                else if (leftSide.Contains(touch.position))
                {
                    // Handle joystick input here (left side touch)
                }
            }

            if (touch.phase == TouchPhase.Moved)
            {
                if (rightSide.Contains(touch.position))
                {
                    Dragging();
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                if (rightSide.Contains(touch.position))
                {
                    DragRelease();
                }
                else if (leftSide.Contains(touch.position))
                {
                    // Handle joystick input release here (left side touch)
                }
            }
        }
		bool isPlayerFlipped = transform.localScale.x < 0;
		arrowDirectionIndicator.transform.localScale = new Vector3(isPlayerFlipped ? -1 : 1, 1, 1);
    }

    void DragStart()
    {
        dragStartPos = Camera.main.ScreenToWorldPoint(touch.position);
        dragStartPos.z = 0f;

        
    }

    void Dragging()
    {
    Vector3 draggingPos = Camera.main.ScreenToWorldPoint(touch.position);
	draggingPos.z = 0f;

	Vector3 force = dragStartPos - draggingPos;
	Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag);

    // Update the position of the pivot point (parent of the indicator)
    arrowDirectionIndicator.transform.position = transform.position;
	
    // Calculate the rotation angle based on the clamped force direction
    float angle = Mathf.Atan2(clampedForce.y, clampedForce.x) * Mathf.Rad2Deg;

    // Apply rotation offset
    angle += rotationOffset.z;

    // Set the rotation of the pivot point
    arrowDirectionIndicator.transform.localRotation = Quaternion.Euler(0f, 0f, angle);

    // Calculate the distance of the drag (magnitude of the clampedForce)
    float dragDistance = clampedForce.magnitude;

    // Calculate the t value for color interpolation based on drag distance and maxDrag
    float t = Mathf.Clamp01(dragDistance / maxDrag);

    // Interpolate between startColor and endColor based on t
    Color lerpedColor = Color.Lerp(startColor, endColor, t);

    // Apply the interpolated color to the SpriteRenderer
    arrowDirectionIndicatorSprite.GetComponent<SpriteRenderer>().color = lerpedColor;

    // Calculate the scale factor based on the drag distance and maxDrag
    float scaleFactor = Mathf.Clamp01(dragDistance / maxDrag) * 0.8f; // Adjust this multiplier

    // Set the local scale of the indicator's transform
    arrowDirectionIndicatorSprite.transform.localScale = new Vector3(scaleFactor, 0.05f, 1f);
	}






    void DragRelease()
    {
        

        Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(touch.position);
        dragReleasePos.z = 0f;

        Vector3 force = dragStartPos - dragReleasePos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;

        //rb.AddForce(clampedForce, ForceMode2D.Impulse);

        // Destroy the previously spawned arrow
        if (spawnedArrow != null)
        {
            Destroy(spawnedArrow.gameObject);
        }

        // Instantiate a new arrow and set its position
        spawnedArrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);

        // Get the Rigidbody2D of the spawned arrow
        Rigidbody2D arrowRb = spawnedArrow.GetComponent<Rigidbody2D>();

        // Calculate the direction of the arrow's velocity
        Vector3 velocityDirection = arrowRb.velocity.normalized;

        // Calculate the rotation angle based on the velocity direction
        float angle = Mathf.Atan2(velocityDirection.y, velocityDirection.x) * Mathf.Rad2Deg;

        // Set the rotation of the arrow's tail game object
        spawnedArrow.GetComponentInChildren<Transform>().localRotation = Quaternion.Euler(0, 0, angle);

        // Apply an initial force to the arrow to make it move forward
        arrowRb.AddForce(clampedForce, ForceMode2D.Impulse);
		
		arrowDirectionIndicatorSprite.transform.localScale = new Vector3(0f, 0.0f, 1f);
		
    }
}
