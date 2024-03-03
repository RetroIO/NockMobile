using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public float lifetime = 1.5f;
    private float spawnTime;

    private void Start()
    {
        spawnTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - spawnTime >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
