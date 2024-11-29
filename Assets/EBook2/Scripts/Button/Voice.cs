using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voice : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject PauseButton;
    public GameObject Content;
    public bool PlayVoice;
    public float InitialCount = 0;
    public float AddCount = 0;

    public float StartX;
    public float EndX;

    void Start()
    {
        StartX = Content.transform.position.x;
    }
    void Update()
    {
        if (Content.transform.position.x >= EndX)
        {
            PlayVoice = false;
            ChapterStop();
            ClosePauseButton();
        }
    }
    public void ClosePlayButton()
    {
        PauseButton.SetActive(true);
        PlayButton.SetActive(false);
        PlayVoice = true;
        Invoke("ChapterMove", 1);
    }
    public void ClosePauseButton()
    {
        PlayButton.SetActive(true);
        PauseButton.SetActive(false);
        PlayVoice = false;
    }
    public void ChapterMove()
    {
        if (PlayVoice == true)
        {
            Content.transform.localPosition = new Vector2(StartX + InitialCount, 0);
            InitialCount += AddCount;
            Invoke("ChapterMove", 0.01f);
        }                
    }
    public void ChapterStop()
    {
        Content.transform.localPosition = new Vector2(StartX, 0);
        InitialCount = 0;
    }
}
