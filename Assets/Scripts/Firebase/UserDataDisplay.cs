using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;

public class UserDataDisplay : MonoBehaviour
{
    private DatabaseReference dbRef;
    private string userId = "HSEpJ76oEcPK2R9oZsRFsvC4ke43";

    void Start()
    {
        StartCoroutine(WaitForFirebaseAndLoadData());
    }

    IEnumerator WaitForFirebaseAndLoadData()
    {
        // Wait until Firebase is ready
        while (FirebaseManager.Instance == null || !FirebaseManager.Instance.IsFirebaseReady)
        {
            yield return null; // Wait one frame
        }

        dbRef = FirebaseManager.Instance.DbReference;
        LoadUserData();
    }

    void LoadUserData()
    {
        dbRef.Child("users").Child(userId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                var snapshot = task.Result;
                if (snapshot.Exists)
                {
                    string email = snapshot.Child("email").Value?.ToString() ?? "N/A";
                    string dob = snapshot.Child("dateOfBirth").Value?.ToString() ?? "N/A";
                    string age = snapshot.Child("age").Value?.ToString() ?? "N/A";

                    Debug.Log("User Data:");
                    Debug.Log($" Email: {email}");
                    Debug.Log($" Date of Birth: {dob}");
                    Debug.Log($" Age: {age}");
                }
                else
                {
                    Debug.LogWarning("User data not found in database.");
                }
            }
            else
            {
                Debug.LogError("Failed to retrieve user data: " + task.Exception);
            }
        });
    }
}
