using UnityEngine;

public class player_movement : MonoBehaviour
{
    //----------- Game attributes -------------
    float player_speed = 11f;
    float playerHalfLength = 1.2f;

    public float tiltAmount = 15f;      // Max tilt angle
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
        //Debug.Log(input);
        input = Input.GetAxis("Horizontal");
        if (moveRight) { input = 1f; Debug.Log("input dkhlaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaat");}
        if (moveLeft) { input = -1f; Debug.Log("input dkhlaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaat"); }

        //Debug.Log(input);

        posX.x += input * player_speed * Time.deltaTime;
        
        float screenRation = (float)Screen.width / (float)Screen.height; // this gonna result some wierd shit
        float widthOrtho = Camera.main.orthographicSize * screenRation;

        float targetZRotation = -input * tiltAmount;
        Quaternion targetRotation = Quaternion.Euler(2, 0, targetZRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);

        if (posX.x + playerHalfLength > widthOrtho)
        {
            posX.x = widthOrtho - playerHalfLength;
        }
        else if (posX.x - playerHalfLength < -widthOrtho)
        {
            posX.x = -widthOrtho + playerHalfLength;
        }
        transform.position = posX;

        
    }

    public void OnLeftDown() => moveLeft = true; 
    public void OnLeftUp() => moveLeft = false;

    public void OnRightDown() => moveRight = true;
    public void OnRightUp() => moveRight = false;
}
