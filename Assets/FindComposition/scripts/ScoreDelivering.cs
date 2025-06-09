// thsi script is for storing the cerrent & the best score in the database for FindCompo game
using UnityEngine;
using Firebase.Extensions;
using Firebase.Database;

public class ScoreDelivering : MonoBehaviour
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
                .Child("find_compositions");

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
                    Debug.Log(" found the best score ");
                    bestScore = int.Parse(snapshot.Child("bestScore").Value.ToString());
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
                    PlayerGlobalData.Instance.FindCompositionScore = lastScore;

                    Debug.Log("the last score of Find Compo game is stored correctly :" + PlayerGlobalData.Instance.FindCompositionScore);
                }


            });
        }
        // storing the score in the GameConfigSingletons.findCompositionScore
        if (GameConfigManager.Instance != null)
        {
            GameConfigManager.Instance.findCompositionScore = lastScore;
            GameConfigManager.Instance.verticalOperationsScore = lastScore;
            Debug.Log("the last score of Find Compo game is stored correctly in the GameConfigSingletons :" + GameConfigManager.Instance.findCompositionScore);
        }
        else
        {
            Debug.LogError("GameConfigManager instance is not available.");
        }
    }
}
