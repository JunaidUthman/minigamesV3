using UnityEngine;

public class SpaceshipJoystickMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;
    public Joystick joystick;

    private Rigidbody2D rb;
    private float rotationInput;
    private float thrustInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (joystick == null)
        {
            Debug.LogError(" Joystick not assigned!");
        }
    }

    void Update()
    {
        if (joystick == null) return;

        // Get input from joystick
        rotationInput = -joystick.Horizontal; // negative to rotate clockwise/right
        thrustInput = joystick.Vertical; // forward/backward movement
    }

    void FixedUpdate()
    {
        // ROTATION (Z-axis)
        rb.MoveRotation(rb.rotation + rotationInput * rotationSpeed * Time.fixedDeltaTime);

        // FORWARD MOVEMENT (based on current facing direction)
        Vector2 direction = transform.up;
        rb.linearVelocity = direction * thrustInput * moveSpeed;
    }
}
