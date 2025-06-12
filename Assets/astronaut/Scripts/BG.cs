using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    public float scrollSpeed = 0.1f; // Vitesse de défilement
    private Material mat;
    private Vector2 offset;

    void Start()
    {
        // Récupère le matériel du Quad
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // Défilement horizontal
        offset.x += scrollSpeed * Time.deltaTime;

        // Applique le décalage de texture
        mat.mainTextureOffset = offset;
    }
}
