using UnityEngine;

public class minicamera_movement : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset = new Vector3(0, 0, -10);
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        Vector3 targetPosition = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }

}
