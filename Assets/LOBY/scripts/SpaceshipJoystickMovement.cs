using UnityEngine;

public class LobyPlayer_movementforspaceship_movementFor : MonoBehaviour
{
    public float maxSpeed = 15f;
    public float rotSpeed = 360f;
    float shipBoundaryRadius = 0.5f;

    public Joystick joystick;

    void Update()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        Vector2 inputDirection = new Vector2(horizontal, vertical);

        if (inputDirection.magnitude > 0.1f)
        {
            // Normalize the input direction to prevent speed boost when diagonally moving
            inputDirection = inputDirection.normalized;

            // Movement
            Vector3 movement = new Vector3(inputDirection.x, inputDirection.y, 0) * maxSpeed * Time.deltaTime;
            Vector3 newPos = transform.position + movement;

            // Clamp position
            float minX = -60f + shipBoundaryRadius;
            float maxX = 60f - shipBoundaryRadius;
            float minY = -30f + shipBoundaryRadius;
            float maxY = 30f - shipBoundaryRadius;
            newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
            newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

            transform.position = newPos;

            // Rotate the ship to face the movement direction
            float angle = Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
        }
    }
}