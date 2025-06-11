using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;

public class totalScoreManager : MonoBehaviour
{
    private string currentUserId = "HSEpJ76oEcPK2R9oZsRFsvC4ke43";

    void Start()
    {
        int findCompositionScore = GameConfigManager.Instance.findCompositionScore;
        Debug.Log("find compo score is " + findCompositionScore);

        int chooseAnswerScore = GameConfigManager.Instance.chooseAnswerScore;
        Debug.Log("choose answer is " + chooseAnswerScore);

        int verticalOperationsScore = GameConfigManager.Instance.verticalOperationsScore;
        Debug.Log("vertical score is " + verticalOperationsScore);

        int totalScore = (int)(findCompositionScore + chooseAnswerScore + verticalOperationsScore) / 3;

        if (FirebaseManager.Instance.IsFirebaseReady)
        {
            UpdateTotalScoreInFirebase(currentUserId, totalScore);
        }
        else
        {
            Debug.LogError("Firebase is not ready.");
        }
    }

    private void UpdateTotalScoreInFirebase(string userId, int newScore)
    {
        string path = $"users/{userId}/playerProfile/score";
        DatabaseReference scoreRef = FirebaseManager.Instance.DbReference.Child(path);

        scoreRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                int currentScore = 0;

                if (snapshot.Exists && int.TryParse(snapshot.Value.ToString(), out int existingScore))
                {
                    currentScore = existingScore;
                }

                int updatedScore = currentScore + newScore;

                scoreRef.SetValueAsync(updatedScore).ContinueWithOnMainThread(saveTask =>
                {
                    if (saveTask.IsCompleted)
                    {
                        Debug.Log($"Updated total score to {updatedScore} for user {userId}.");
                    }
                    else
                    {
                        Debug.LogError("Failed to save updated score to Firebase: " + saveTask.Exception);
                    }
                });
            }
            else
            {
                Debug.LogError("Failed to read current score from Firebase: " + task.Exception);
            }
        });
    }
}
