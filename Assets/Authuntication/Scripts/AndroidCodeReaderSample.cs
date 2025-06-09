using UnityEngine;
using ZXing;
using System.Collections;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine.SceneManagement;

public class StandaloneEasyReaderSample : MonoBehaviour
{
    [SerializeField]
    private string lastResult;
    [SerializeField]
    private bool logAvailableWebcams;
    [SerializeField]
    private int selectedWebcamIndex;

    private WebCamTexture camTexture;
    private Color32[] cameraColorData;
    private int width, height;
    private Rect screenRect;

    private bool isProcessing = false;
    private bool hasScannedSuccessfully = false;

    private int test = 0;

    private IBarcodeReader barcodeReader = new BarcodeReader
    {
        AutoRotate = false,
        Options = new ZXing.Common.DecodingOptions
        {
            TryHarder = false
        }
    };

    private Result result;

    private void Start()
    {
        LogWebcamDevices();
        SetupWebcamTexture();
        PlayWebcamTexture();

        lastResult = "";
        cameraColorData = new Color32[width * height];
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
    }

    private void OnEnable()
    {
        PlayWebcamTexture();
    }

    private void OnDisable()
    {
        if (camTexture != null)
        {
            camTexture.Pause();
        }
    }

    private void Update()
    {
        if (hasScannedSuccessfully || isProcessing || !camTexture.isPlaying)
            return;

        camTexture.GetPixels32(cameraColorData);
        //Debug.Log($"Decode input array size: {cameraColorData.Length} ({width}x{height})");

        result = barcodeReader.Decode(cameraColorData, width, height);

        if (result != null)
        {
            isProcessing = true;
            string scannedId = result.Text;
            Debug.Log("Scanned QR code: " + scannedId);
            StartCoroutine(CheckTestInFirebase(scannedId));
        }
        else
        {
            //Debug.Log("there is no result");
        }
    }

    private void OnGUI()
    {
        GUI.DrawTexture(screenRect, camTexture, ScaleMode.ScaleToFit);
        GUI.TextField(new Rect(10, 10, 256, 25), lastResult);
    }

    private void OnDestroy()
    {
        if (camTexture != null)
            camTexture.Stop();
    }

    private void LogWebcamDevices()
    {
        if (logAvailableWebcams)
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            foreach (var device in devices)
            {
                Debug.Log(device.name);
            }
        }
    }

    private void SetupWebcamTexture()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length > 0)
        {
            string camName = devices[selectedWebcamIndex].name;
            camTexture = new WebCamTexture(camName);
        }
        else
        {
            Debug.LogError("No webcam detected!");
        }
    }

    private void PlayWebcamTexture()
    {
        if (camTexture != null)
        {
            camTexture.Play();
            StartCoroutine(WaitForCameraStart());
        }
    }

    private IEnumerator WaitForCameraStart()
    {
        while (camTexture.width < 100)
            yield return null;

        width = camTexture.width;
        height = camTexture.height;
        cameraColorData = new Color32[width * height]; // update size here too
        Debug.Log("Camera started with resolution: " + width + "x" + height);
    }

    private IEnumerator CheckTestInFirebase(string scannedUid)
    {
        if (!FirebaseManager.Instance.IsFirebaseReady)
        {
            Debug.LogWarning("Firebase is not ready yet.");
            isProcessing = false;
            yield break;
        }

        var testsRequest = FirebaseManager.Instance.DbReference.Child("tests").GetValueAsync();
        yield return new WaitUntil(() => testsRequest.IsCompleted);

        if (testsRequest.Exception != null)
        {
            Debug.LogError("Failed to fetch tests: " + testsRequest.Exception);
            isProcessing = false;
            yield break;
        }

        var matchedTest = FindMatchingTestForStudent(testsRequest.Result, scannedUid);
        if (matchedTest == null)
        {
            lastResult = "User not found in any test.";
            isProcessing = false;
            yield break;
        }

        var (testId, testSnapshot) = matchedTest.Value;

        DataSnapshot groupSnapshot = testSnapshot.Child("groups").Child("group_1");

        yield return InitializeGlobalData(scannedUid, testId, testSnapshot);
        StoreGameConfig(groupSnapshot);
        Debug.Log("the StoreGameConfig is cdone wooooooooooooooooooooooow!!!");
        yield return StorePlayerProfile(scannedUid);
        Debug.Log("player profile is stored");

        hasScannedSuccessfully = true;
        camTexture.Stop();
        camTexture = null;
        Destroy(gameObject);
        Debug.Log("the game is about to switch to scean 1");
        SceneManager.LoadScene(1);

        isProcessing = false;
    }

    private (string, DataSnapshot)? FindMatchingTestForStudent(DataSnapshot testsSnapshot, string scannedUid)
    {
        foreach (var test in testsSnapshot.Children)
        {
            string testState = test.Child("state").Value?.ToString();
            if (testState != "published") continue;

            var students = test.Child("groups").Child("group_1").Child("students");
            foreach (var student in students.Children)
            {
                if (student.Value?.ToString() == scannedUid)
                {
                    Debug.Log("Found matching test: " + test.Key);
                    lastResult = "Matched test: " + test.Key;
                    return (test.Key, test);
                }
            }
        }

        return null;
    }


    private IEnumerator InitializeGlobalData(string uid, string testId, DataSnapshot testSnapshot)
    {
        if (PlayerGlobalData.Instance == null)
        {
            Debug.Log("PlayerGlobalData is null §§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§");
            GameObject prefab = Resources.Load<GameObject>("Prefabs/PlayerGlobalData");
            if (prefab != null) Instantiate(prefab);
            yield return null;
        }

        if (TimeManager.Instance == null)
        {
            Debug.Log("TimeManager is null §§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§");
            GameObject timePrefab = Resources.Load<GameObject>("Prefabs/TimeManager");
            if (timePrefab != null) Instantiate(timePrefab);
            yield return null;
        }

        PlayerGlobalData.Instance.id = uid;
        PlayerGlobalData.Instance.testId = testId;

        if (float.TryParse(testSnapshot.Child("duration").Value?.ToString(), out float duration))
        {
            PlayerGlobalData.Instance.gameDuration = duration;
            TimeManager.Instance.StartTimer(duration * 60f);
            Debug.Log("Stored player duration: " + duration);
        }
    }

    private IEnumerator StorePlayerProfile(string uid)
    {
        var userRequest = FirebaseManager.Instance.DbReference.Child("users").Child(uid).GetValueAsync();
        yield return new WaitUntil(() => userRequest.IsCompleted);

        if (userRequest.Exception != null)
        {
            Debug.LogError("Failed to fetch user data: " + userRequest.Exception);
            yield break;
        }

        var profile = userRequest.Result.Child("playerProfile");

        int.TryParse(profile.Child("coins").Value?.ToString(), out int coins);
        int.TryParse(profile.Child("mathLevel").Value?.ToString(), out int mathLevel);
        int.TryParse(profile.Child("score").Value?.ToString(), out int score);

        PlayerGlobalData.Instance.coins = coins;
        PlayerGlobalData.Instance.mathLevel = mathLevel;
        PlayerGlobalData.Instance.score = score;
    }

    private void StoreGameConfig(DataSnapshot groupSnapshot)
    {
        Debug.Log("StoreGameConfig started.");


        if (GameConfigManager.Instance == null)
        {
            Debug.Log("GameConfig is null §§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§");

            GameObject prefab = Resources.Load<GameObject>("Prefabs/GameConfigManager");
            if (prefab != null) Instantiate(prefab);
            return;
        }

        var games = groupSnapshot.Child("configuredGames");

        if (games == null || !games.Exists)
        {
            Debug.LogError("configuredGames not found in Firebase.");
            return;
        }

        GameConfigManager.Instance.findComposition = ExtractGameConfig(games.Child("find_compositions"));
        GameConfigManager.Instance.chooseAnswer = ExtractGameConfig(games.Child("choose_answer"));
        GameConfigManager.Instance.verticalOperations = ExtractGameConfig(games.Child("vertical_operations"));

        Debug.Log("Stored all game configurations.");
    }




    private GameConfig ExtractGameConfig(DataSnapshot snapshot)
    {
        if (snapshot == null || !snapshot.Exists)
        {
            Debug.LogError("GameConfig snapshot is null or does not exist: " + snapshot?.Key);
            return null;
        }

        try
        {
            var config = new GameConfig
            {
                maxNumberRange = int.Parse(snapshot.Child("maxNumberRange").Value?.ToString() ?? "0"),
                numOperations = int.Parse(snapshot.Child("numOperations").Value?.ToString() ?? "0"),
                numComposition = int.Parse(snapshot.Child("numComposition").Value?.ToString() ?? "0"),
                order = int.Parse(snapshot.Child("order").Value?.ToString() ?? "0"),
                requiredCorrectAnswersMinimumPercent = int.Parse(snapshot.Child("requiredCorrectAnswers").Value?.ToString() ?? "0"),
                minNumberRange = int.Parse(snapshot.Child("minNumberRange").Value?.ToString() ?? "0")
            };

            Debug.Log("Game config loaded successfully for: " + snapshot.Key);
            return config;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to parse game config for: " + snapshot.Key + " - " + e.Message);
            return null;
        }
    }

}
