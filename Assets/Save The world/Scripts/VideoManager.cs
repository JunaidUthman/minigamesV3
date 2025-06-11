using UnityEngine;
using UnityEngine.Video;

public class VideoStarter : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.Play();
    }
}
