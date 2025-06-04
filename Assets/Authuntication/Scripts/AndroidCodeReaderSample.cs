using UnityEngine;
using ZXing;
using System.Collections;

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

    // create a reader with a custom luminance source
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

        lastResult = "http://www.google.com";

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
        if (camTexture.isPlaying)
        {
            // decoding from camera image
            camTexture.GetPixels32(cameraColorData); // -> performance heavy method 
            result = barcodeReader.Decode(cameraColorData, width, height); // -> performance heavy method
            if (result != null)
            {
                lastResult = result.Text + " " + result.BarcodeFormat;
                print(lastResult);
            }
        }
    }

    private void OnGUI()
    {
        // show camera image on screen
        GUI.DrawTexture(screenRect, camTexture, ScaleMode.ScaleToFit);
        // show decoded text on screen
        GUI.TextField(new Rect(10, 10, 256, 25), lastResult);
    }

    private void OnDestroy()
    {
        camTexture.Stop();
    }

    private void LogWebcamDevices()
    {
        if (logAvailableWebcams)
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            for (int i = 0; i < devices.Length; i++)
            {
                Debug.Log(devices[i].name);
            }
        }
    }

    private void SetupWebcamTexture()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length > 0)
        {
            string camName = devices[0].name; // Use first available camera
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
        // Wait until camera is initialized
        while (camTexture.width < 100)
            yield return null;

        width = camTexture.width;
        height = camTexture.height;
        Debug.Log("Camera started with resolution: " + width + "x" + height);
    }

}