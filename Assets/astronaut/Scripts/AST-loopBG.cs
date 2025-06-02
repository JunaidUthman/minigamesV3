using UnityEngine;

public class loopBG : MonoBehaviour
{
    public float loopSpeed;
    public Renderer bgRendrer;


    // Update is called once per frame
    void Update()
    {

        bgRendrer.material.mainTextureOffset += new Vector2(loopSpeed * Time.deltaTime, 0f);
        
    }
}
