using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System;

public class EndGameMathLevelLogger : MonoBehaviour
{
    [SerializeField] private MonoBehaviour prerequisiteScript; // drag the required script here in the Inspector

    private IEnumerator Start()
    {
        Debug.Log("🟨 [Logger] Start called. Waiting for Firebase...");

        yield return new WaitUntil(() => FirebaseManager.Instance != null && FirebaseManager.Instance.IsFirebaseReady);

        Debug.Log("✅ [Logger] Firebase is ready.");

        if (prerequisiteScript != null)
        {
            Debug.Log("🟨 [Logger] Waiting for prerequisite script to finish: " + prerequisiteScript.GetType().Name);
            var prereq = prerequisiteScript as EndGame_mathLevelManager;
            if (prereq != null)
            {
                yield return new WaitUntil(() => prereq.isCompleted);
                Debug.Log("✅ [Logger] Prerequisite script completed via flag.");
            }
            else
            {
                Debug.LogWarning("⚠️ [Logger] prerequisiteScript is not of expected type.");
            }
            Debug.Log("✅ [Logger] Prerequisite script finished.");
        }
        else
        {
            Debug.Log("⚠️ [Logger] No prerequisite script assigned.");
        }

        if (PlayerGlobalData.Instance == null)
        {
            Debug.LogError("❌ [Logger] PlayerGlobalData.Instance is null. Cannot proceed.");
            yield break;
        }

        string userId = PlayerGlobalData.Instance.id;
        int currentMathLevel = PlayerGlobalData.Instance.mathLevel;
        string currentDate = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

        Debug.Log("🟩 [Logger] Logging math level " + currentMathLevel + " at " + currentDate + " for user: " + userId);

        DatabaseReference userRef = FirebaseManager.Instance.DbReference
            .Child("users").Child(userId).Child("historyMathLevel");

        userRef.Push().SetRawJsonValueAsync(JsonUtility.ToJson(new MathLevelEntry
        {
            date = currentDate,
            mathLevel = currentMathLevel
        })).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                Debug.Log(" [Logger] Math level history updated successfully.");

            }
            else
            {
                Debug.LogError(" [Logger] Failed to update math level history: " + task.Exception);
            }
        });
    }

    [Serializable]
    private class MathLevelEntry
    {
        public string date;
        public int mathLevel;
    }
}
