using UnityEngine;

public class player_movement : MonoBehaviour
{
    float player_speed = 11f;
    float playerHalfLength = 1.2f;

    public float tiltAmount = 15f;
    public float tiltSpeed = 5f;

    private bool moveLeft = false;
    private bool moveRight = false;

    private float input = 0f;

    void Start()
    {
        Vector3 posY = transform.position;
        Vector3 cameraPosition = Camera.main.transform.position;
        posY.y += cameraPosition.y - 5 + playerHalfLength;
        transform.position = posY;
    }

    void Update()
    {
        Vector3 posX = transform.position;

        // Input comes only from buttons
        input = 0f;
        if (moveRight) input = 1f;
        else if (moveLeft) input = -1f;

        posX.x += input * player_speed * Time.deltaTime;

        float screenRatio = (float)Screen.width / (float)Screen.height;
        float widthOrtho = Camera.main.orthographicSize * screenRatio;

        float targetZRotation = -input * tiltAmount;
        Quaternion targetRotation = Quaternion.Euler(2, 0, targetZRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);

        // Clamp position to screen bounds
        if (posX.x + playerHalfLength > widthOrtho)
            posX.x = widthOrtho - playerHalfLength;
        else if (posX.x - playerHalfLength < -widthOrtho)
            posX.x = -widthOrtho + playerHalfLength;

        transform.position = posX;
    }

    // UI Button handlers
    public void OnLeftDown() => moveLeft = true;
    public void OnLeftUp() => moveLeft = false;

    public void OnRightDown() => moveRight = true;
    public void OnRightUp() => moveRight = false;
}
