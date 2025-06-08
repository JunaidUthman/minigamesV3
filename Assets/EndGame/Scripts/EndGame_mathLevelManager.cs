using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class EndGame_mathLevelManager : MonoBehaviour
{
    private string userId;
    private string testId;
    private int currentMathLevel;

    private void Start()
    {
        userId = PlayerGlobalData.Instance.id;
        testId = PlayerGlobalData.Instance.testId;
        currentMathLevel = PlayerGlobalData.Instance.mathLevel;

        if (FirebaseManager.Instance.IsFirebaseReady)
        {
            StartCoroutine(CheckScoresAndUpdateLevel());
        }
        else
        {
            Debug.LogWarning("Firebase not ready.");
        }
    }

    private IEnumerator CheckScoresAndUpdateLevel()
    {
        string testPath = $"tests/{testId}";
        var testRequest = FirebaseManager.Instance.DbReference.Child(testPath).GetValueAsync();
        yield return new WaitUntil(() => testRequest.IsCompleted);

        if (testRequest.Exception != null)
        {
            Debug.LogError("Failed to fetch test config: " + testRequest.Exception);
            yield break;
        }

        DataSnapshot testSnapshot = testRequest.Result;
        var groupSnapshot = testSnapshot.Child("groups").Children.FirstOrDefault();


        if (groupSnapshot == null)
        {
            Debug.LogError("Group data not found in test.");
            yield break;
        }

        var configuredGames = groupSnapshot.Child("configuredGames");

        // Get scores from GameConfigManager
        int userFindScore = GameConfigManager.Instance.findCompositionScore;
        int userChooseScore = GameConfigManager.Instance.chooseAnswerScore;
        int userVerticalScore = GameConfigManager.Instance.verticalOperationsScore;

        // Get required scores from DB
        int requiredFind = int.Parse(configuredGames.Child("find_compositions").Child("requiredCorrectAnswers").Value.ToString());
        int requiredChoose = int.Parse(configuredGames.Child("choose_answer").Child("requiredCorrectAnswers").Value.ToString());
        int requiredVertical = int.Parse(configuredGames.Child("vertical_operations").Child("requiredCorrectAnswers").Value.ToString());

        // Debug logs
        Debug.Log($"User Scores: Find={userFindScore}, Choose={userChooseScore}, Vertical={userVerticalScore}");
        Debug.Log($"Required: Find={requiredFind}, Choose={requiredChoose}, Vertical={requiredVertical}");

        // Check conditions
        bool passedAll = userFindScore >= requiredFind &&
                         userChooseScore >= requiredChoose &&
                         userVerticalScore >= requiredVertical;

        if (passedAll)
        {
            if (currentMathLevel < 6)
            {
                int newLevel = currentMathLevel + 1;
                string userLevelPath = $"users/{userId}/playerProfile/mathLevel";

                // Update on Firebase
                var updateTask = FirebaseManager.Instance.DbReference.Child(userLevelPath).SetValueAsync(newLevel);
                yield return new WaitUntil(() => updateTask.IsCompleted);

                if (updateTask.Exception != null)
                {
                    Debug.LogError("Failed to update math level: " + updateTask.Exception);
                }
                else
                {
                    Debug.Log($"Math level updated to {newLevel}");
                    PlayerGlobalData.Instance.mathLevel = newLevel;
                }
            }
        }
        else
        {
            Debug.Log("Player did not meet the required scores for level up.");
        }
    }
}
