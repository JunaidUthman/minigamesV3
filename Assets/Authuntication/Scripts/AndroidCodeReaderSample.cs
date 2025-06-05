using UnityEngine;
using ZXing;
using System.Collections;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

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
        result = barcodeReader.Decode(cameraColorData, width, height);

        if (result != null)
        {
            isProcessing = true;
            string scannedId = result.Text;
            Debug.Log("Scanned QR code: " + scannedId);
            StartCoroutine(CheckTestInFirebase(scannedId));
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

        DataSnapshot testsSnapshot = testsRequest.Result;
        bool found = false;

        foreach (var test in testsSnapshot.Children)
        {
            var group1 = test.Child("groups").Child("group_1");
            var studentList = group1.Child("students");

            foreach (var student in studentList.Children)
            {
                if (student.Value.ToString() == scannedUid)
                {
                    Debug.Log("Found matching test: " + test.Key);
                    lastResult = "Matched test: " + test.Key;

                    // Store user and test ID
                    if (PlayerGlobalData.Instance != null)
                    {
                        PlayerGlobalData.Instance.id = scannedUid;
                        PlayerGlobalData.Instance.testId = test.Key;
                    }

                    // Save game configuration
                    var games = group1.Child("configuredGames");
                    GameConfigManager.Instance.findComposition = ExtractGameConfig(games.Child("find_compositions"));
                    GameConfigManager.Instance.chooseAnswer = ExtractGameConfig(games.Child("choose_answer"));
                    GameConfigManager.Instance.verticalOperations = ExtractGameConfig(games.Child("vertical_operations"));

                    found = true;
                    hasScannedSuccessfully = true;
                    break;
                }
            }

            if (found) break;
        }

        if (!found)
        {
            lastResult = "User not found in any test.";
            Debug.LogWarning(lastResult);
        }

        isProcessing = false;
    }

    private GameConfig ExtractGameConfig(DataSnapshot snapshot)
    {
        var config = new GameConfig
        {
            maxNumberRange = int.Parse(snapshot.Child("maxNumberRange").Value.ToString()),
            numOperations = int.Parse(snapshot.Child("numOperations").Value.ToString()),
            order = int.Parse(snapshot.Child("order").Value.ToString()),
            requiredCorrectAnswersMinimumPercent = int.Parse(snapshot.Child("requiredCorrectAnswersMinimumPercent").Value.ToString())
        };

        Debug.Log("Game config loaded: " + snapshot.Key);
        return config;
    }
}
