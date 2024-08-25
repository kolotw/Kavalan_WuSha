using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voice : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject PauseButton;

    void Start()
    {

    }
    void Update()
    {
        
    }
    public void ClosePlayButton()
    {
        PauseButton.SetActive(true);
        PlayButton.SetActive(false);
    }
    public void ClosePauseButton()
    {
        PlayButton.SetActive(true);
        PauseButton.SetActive(false);
    }
}
