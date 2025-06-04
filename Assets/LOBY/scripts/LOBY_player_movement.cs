using UnityEngine;
using UnityEngine.InputSystem; // <- Required for new input system

public class player_Movement : MonoBehaviour
{
    public float maxSpeed = 15f;
    public float rotSpeed = 180f;
    float shipBoundaryRadius = 0.5f;

    Vector2 moveInput; // Will store the input from the new system

    private void OnEnable()
    {
        // Enable input system
        controls = new PlayerControls();
        controls.Gameplay.Enable();

        // Register the movement callback
        controls.Gameplay.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        controls.Gameplay.Move.performed -= ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled -= ctx => moveInput = Vector2.zero;
        controls.Gameplay.Disable();
    }

    PlayerControls controls;

    void Update()
    {
        // Rotation
        float z = transform.rotation.eulerAngles.z;
        z += -moveInput.x * rotSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, z);

        // Movement
        Vector3 direction = transform.up;
        float inputY = moveInput.y;
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
