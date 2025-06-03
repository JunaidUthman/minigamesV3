using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance { get; private set; }

    public bool IsFirebaseReady { get; private set; } = false;
    public DatabaseReference DbReference { get; private set; }

    void Awake()
    {
        // Singleton setup
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Stays alive between scenes

        // Initialize Firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;
                DbReference = FirebaseDatabase.DefaultInstance.RootReference;
                IsFirebaseReady = true;
                Debug.Log(" Firebase initialized successfully.");
            }
            else
            {
                Debug.LogError(" Firebase initialization failed: " + task.Exception);
            }
        });
    }
}
