using UnityEngine;

public class marker_movement : MonoBehaviour
{
    public Transform player;          // Assign your player transform in Inspector
    public Camera miniMapCamera;      // Assign your mini-map camera in Inspector

    void Update()
    {
        // Get the player's world position
        Vector3 playerPos = player.position;

        // Convert player's world position to mini-map camera viewport position (0..1 range)
        Vector3 viewportPos = miniMapCamera.WorldToViewportPoint(playerPos);

        // Convert viewport position back to world point relative to mini-map camera
        Vector3 markerPos = miniMapCamera.ViewportToWorldPoint(new Vector3(viewportPos.x, viewportPos.y, miniMapCamera.nearClipPlane + 1));

        // Update marker position
        transform.position = new Vector3(markerPos.x, markerPos.y, transform.position.z);


        transform.rotation = Quaternion.Euler(0, 0, player.eulerAngles.z);
    }
}
