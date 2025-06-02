using UnityEngine;

public class player_Movement : MonoBehaviour
{
    float maxSpeed = 15f;
    public float rotSpeed = 180f;
    float shipBoundaryRadius = 0.5f;

    void Update()
    {
        // Rotation
        float z = transform.rotation.eulerAngles.z;
        z += -Input.GetAxisRaw("Horizontal") * rotSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, z);

        // Movement
        Vector3 direction = transform.up;
        float inputY = Input.GetAxisRaw("Vertical");
        Vector3 velocity = direction * maxSpeed * inputY;
        Vector3 newPos = transform.position + velocity * Time.deltaTime;

        // Clamp position to stay inside the 12x12 area
        float minX = -60f + shipBoundaryRadius;
        float maxX = 60f - shipBoundaryRadius;
        float minY = -30f + shipBoundaryRadius;
        float maxY = 30f - shipBoundaryRadius;

        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

        transform.position = newPos;
    }
}
