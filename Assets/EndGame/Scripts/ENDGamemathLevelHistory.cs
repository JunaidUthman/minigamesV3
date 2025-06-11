using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System;

public class EndGameMathLevelLogger : MonoBehaviour
{
    [SerializeField] private MonoBehaviour prerequisiteScript;

    // ✅ Make Start public so Unity can detect and call it automatically
    public IEnumerator Start()
    {
        yield return new WaitUntil(() => FirebaseManager.Instance != null && FirebaseManager.Instance.IsFirebaseReady);

        if (prerequisiteScript is EndGame_mathLevelManager prereq)
        {
            yield return new WaitUntil(() => prereq.isCompleted);
        }

        if (PlayerGlobalData.Instance == null)
        {
            Debug.LogError("[Logger] PlayerGlobalData is null.");
            yield break;
        }

        string userId = PlayerGlobalData.Instance.id;

        // 🔍 Retrieve mathLevel from Firebase
        DatabaseReference mathLevelRef = FirebaseManager.Instance.DbReference
            .Child("users").Child(userId).Child("mathLevel");

        var getTask = mathLevelRef.GetValueAsync();
        yield return new WaitUntil(() => getTask.IsCompleted);

        if (!getTask.IsCompletedSuccessfully || !getTask.Result.Exists)
        {
            Debug.LogError("[Logger] Failed to fetch mathLevel from Firebase.");
            yield break;
        }

        if (!int.TryParse(getTask.Result.Value.ToString(), out int currentMathLevel))
        {
            Debug.LogError("[Logger] Invalid mathLevel value in Firebase.");
            yield break;
        }

        string currentDate = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

        // 📤 Log the math level history
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
                Debug.Log("[Logger] Math level " + currentMathLevel + " logged successfully.");
            }
            else
            {
                Debug.LogError("[Logger] Failed to log math level: " + task.Exception);
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
