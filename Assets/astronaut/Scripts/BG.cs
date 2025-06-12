using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    public float scrollSpeed = 0.1f; // Vitesse de d�filement
    private Material mat;
    private Vector2 offset;

    void Start()
    {
        // R�cup�re le mat�riel du Quad
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // D�filement horizontal
        offset.x += scrollSpeed * Time.deltaTime;

        // Applique le d�calage de texture
        mat.mainTextureOffset = offset;
    }
}
