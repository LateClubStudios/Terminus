using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutSceneEnd : MonoBehaviour
{
    VideoPlayer vid;

    void Start()
    {
        vid = GameObject.Find("VideoPlayer").GetComponent<VideoPlayer>();
        vid.loopPointReached += CheckOver;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            print("Video Playback Skiped");
            SceneManager.LoadScene("000-TutorialScene");
        }
    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        print("Video Playback Ended");
        SceneManager.LoadScene("000-TutorialScene");
    }
}