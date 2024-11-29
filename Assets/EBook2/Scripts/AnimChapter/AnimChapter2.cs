using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimChapter2 : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject StopButton;
    public GameObject ScrollView;
    public GameObject Content;
    public GameObject Anim;

    public Image Chapter21;
    public Image Chapter22;
    public Image Chapter23;
    public Image Chapter24;
    public Image Chapter25;
    public Image Chapter26;
    public Image Chapter27;
    public Image Chapter28;
    public Image Chapter29;
    public Image Chapter210;
    public Image Chapter211;
    public Image Chapter212;
    public Image Chapter213;
    public Image Chapter214;
    public Image Chapter215;

    public float Time1;
    public float Time2;
    public float Time3;
    public float Time4;
    public float Time5;
    public float Time6;
    public float Time7;
    public float Time8;
    public float Time9;
    public float Time10;
    public float Time11;
    public float Time12;
    public float Time13;
    public float Time14;
    public float Time15;
    public float Time16;

    float ContextX;
    public float TotalTime;
    public int ImageCount;
    public float ColorCount;
    void Start()
    {
        ContextX = Content.transform.position.x;
    }
    void Update()
    {
        
    }
    public void ClosePlayButton()
    {
        StopButton.SetActive(true);
        Anim.SetActive(true);
        PlayButton.SetActive(false);
        ScrollView.SetActive(false);
        PlayAnim();
        Invoke("AddTime", 1);
    }
    public void CloseStopButton()
    {
        PlayButton.SetActive(true);
        ScrollView.SetActive(true);
        Content.transform.localPosition = new Vector2(ContextX, 0);
        StopButton.SetActive(false);
        Anim.SetActive(false);
        End();
    }
    public void AddTime()
    {
        TotalTime += 1;
        Invoke("AddTime", 1);
    }
    public void PlayAnim()
    {
        ImageCount = 1;
        AddColor();
        Invoke("ChangeImage", Time1);
        Invoke("ChangeImage", Time2);
        Invoke("ChangeImage", Time3);
        Invoke("ChangeImage", Time4);
        Invoke("ChangeImage", Time5);
        Invoke("ChangeImage", Time6);
        Invoke("ChangeImage", Time7);
        Invoke("ChangeImage", Time8);
        Invoke("ChangeImage", Time9);
        Invoke("ChangeImage", Time10);
        Invoke("ChangeImage", Time11);
        Invoke("ChangeImage", Time12);
        Invoke("ChangeImage", Time13);
        Invoke("ChangeImage", Time14);
        Invoke("ReduceColor", Time15);
        Invoke("CloseStopButton", Time16);
    }
    public void ChangeImage()
    {
        ReduceColor();
        Invoke("AddColor", 1);
    }
    public void AddColor()
    {
        if (ColorCount <= 1)
        {
            ColorCount += 0.1f;
            ImageDetection();
            Invoke("AddColor", 0.05f);
        }
        print(ColorCount);
    }
    public void ReduceColor()
    {
        if (ColorCount >= 0)
        {
            ColorCount -= 0.1f;
            ImageDetection();
            Invoke("ReduceColor", 0.05f);
        }
        else
        {
            ImageCount += 1;
        }
        print(ColorCount);
    }
    void ImageDetection()
    {
        if (ImageCount == 1)
            Chapter21.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 2)
            Chapter22.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 3)
            Chapter23.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 4)
            Chapter24.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 5)
            Chapter25.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 6)
            Chapter26.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 7)
            Chapter27.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 8)
            Chapter28.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 9)
            Chapter29.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 10)
            Chapter210.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 11)
            Chapter211.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 12)
            Chapter212.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 13)
            Chapter213.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 14)
            Chapter214.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 15)
            Chapter215.color = new Color(1, 1, 1, ColorCount);
    }
    void End()
    {
        CancelInvoke();
        TotalTime = 0;
        ImageCount = 0;
        ColorCount = 0;
        Chapter21.color = new Color(1, 1, 1, 0);
        Chapter22.color = new Color(1, 1, 1, 0);
        Chapter23.color = new Color(1, 1, 1, 0);
        Chapter24.color = new Color(1, 1, 1, 0);
        Chapter25.color = new Color(1, 1, 1, 0);
        Chapter26.color = new Color(1, 1, 1, 0);
        Chapter27.color = new Color(1, 1, 1, 0);
        Chapter28.color = new Color(1, 1, 1, 0);
        Chapter29.color = new Color(1, 1, 1, 0);
        Chapter210.color = new Color(1, 1, 1, 0);
        Chapter211.color = new Color(1, 1, 1, 0);
        Chapter212.color = new Color(1, 1, 1, 0);
        Chapter213.color = new Color(1, 1, 1, 0);
        Chapter214.color = new Color(1, 1, 1, 0);
        Chapter215.color = new Color(1, 1, 1, 0);

    }
}
