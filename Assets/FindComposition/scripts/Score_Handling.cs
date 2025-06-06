using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class Score_Handling : MonoBehaviour
{
    //--------------Data base attributes ----------
    public int score = 0;
    //==============Game Data ==============
    int rigthAnswerLayerNumber = 7;
    public TMP_Text scoreText;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer == rigthAnswerLayerNumber)
        {
            score++;
            scoreText.text = "Score :" + score;
        }


    }
}
