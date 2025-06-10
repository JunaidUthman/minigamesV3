using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;

public class totalScoreManager : MonoBehaviour
{
    // Replace this with the actual ID of the user (e.g. obtained from Firebase Authentication)
    private string currentUserId = "HSEpJ76oEcPK2R9oZsRFsvC4ke43";

    void Start()
    {

        // Calculate score
        int findCompositionScore = GameConfigManager.Instance.findCompositionScore;
        Debug.Log("find compo score is " + findCompositionScore);

        int chooseAnswerScore = GameConfigManager.Instance.chooseAnswerScore;
        Debug.Log("choose answer is " + chooseAnswerScore);

        int verticalOperationsScore = GameConfigManager.Instance.verticalOperationsScore;
        Debug.Log("vertical score is " + verticalOperationsScore);

        int totalScore = (int)(findCompositionScore + chooseAnswerScore + verticalOperationsScore) / 3;

        // Save to Firebase
        if (FirebaseManager.Instance.IsFirebaseReady)
        {
            SaveTotalScoreToFirebase(currentUserId, totalScore);
        }
        else
        {
            Debug.LogError("Firebase is not ready.");
        }
    }

    private void SaveTotalScoreToFirebase(string userId, int score)
    {
        string path = $"users/{userId}/playerProfile/score";
        FirebaseManager.Instance.DbReference.Child(path).SetValueAsync(score).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log($" Total score {score} successfully saved for user {userId}.");
            }
            else
            {
                Debug.LogError(" Failed to save score to Firebase: " + task.Exception);
            }
        });
    }
}
