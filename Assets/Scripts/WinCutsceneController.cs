using UnityEngine;
using UnityEngine.Video;

public class WinCutsceneController : MonoBehaviour
{
   private VideoPlayer videoPlayer;

    void Awake () 
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void Update()
    {
        if ( videoPlayer.frame > 0 && (videoPlayer.isPlaying == false)){
            GameWin.Instance.Win();
        }
    }
}
