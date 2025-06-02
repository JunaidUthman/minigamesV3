using UnityEngine;

public class bullet_movement : MonoBehaviour
{
    float bulletSpeed = 11f;
    private float upperBound;

    void Start()
    {
        // Get top edge of the camera in world space
        upperBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    void Update()
    {
        // Move bullet up
        Vector3 bulletPos = transform.position;
        bulletPos.y += bulletSpeed * Time.deltaTime;
        transform.position = bulletPos;

        // Destroy bullet if it's above the screen
        if (transform.position.y > upperBound)
        {
            Destroy(gameObject);
        }
    }
}
