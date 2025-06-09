using UnityEngine;
using Firebase.Extensions;
using Firebase.Database;


public class ASTScoreDelivring : MonoBehaviour
{
    public void deliverScore(int lastScore)
{
    // storing the score in the firebase database
    if (FirebaseManager.Instance != null && FirebaseManager.Instance.IsFirebaseReady)
    {
        Debug.Log("fire base instance exist");
        string userId = PlayerGlobalData.Instance.id;

        // Reference to the user's find_compositions progress
        DatabaseReference gameProgressRef = FirebaseManager.Instance.DbReference
            .Child("users")
            .Child(userId)
            .Child("gameProgress")
            .Child("choose_answer");

        // Read current bestScore from Firebase first
        gameProgressRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to retrieve existing gameProgress: " + task.Exception);
                return;
            }

            DataSnapshot snapshot = task.Result;
            int bestScore = 0;

            if (snapshot.Exists && snapshot.HasChild("bestScore"))
            {
                Debug.Log(" found the best score for the choose answer !!!!!!!!!!!!!!");
                
                bestScore = int.Parse(snapshot.Child("bestScore").Value.ToString());
            }
            else
            {
                Debug.Log("i cant find bestScore :" + bestScore);
            }

            if (lastScore > bestScore)
            {
                bestScore = lastScore;
            }

            string timestamp = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
            Debug.Log("the current score is : " + lastScore);
            Debug.Log("the best score is : " + bestScore);

            // Set values in Firebase
            gameProgressRef.Child("lastScore").SetValueAsync(lastScore);
            gameProgressRef.Child("bestScore").SetValueAsync(bestScore);
            gameProgressRef.Child("completedAt").SetValueAsync(timestamp);


            // storing the currentScore(lastScore) into the playerGlobalData

            if (PlayerGlobalData.Instance != null)
            {
                PlayerGlobalData.Instance.ChooseAnswerScore = lastScore;

                Debug.Log("the last score of choose answer game is stored correctly :" + PlayerGlobalData.Instance.ChooseAnswerScore);
            }


        });
    }
    // storing the score in the GameConfigSingletons.findCompositionScore
    if (GameConfigManager.Instance != null)
    {

        GameConfigManager.Instance.chooseAnswerScore = lastScore;

        Debug.Log("the last score of choose answer game is stored correctly in the GameConfigSingletons :" + GameConfigManager.Instance.chooseAnswerScore);
    }
    else
    {
        Debug.LogError("GameConfigManager instance is not available.");
    }
}
}
