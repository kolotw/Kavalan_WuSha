using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimChapter8 : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject StopButton;
    public GameObject ScrollView;
    public GameObject Content;
    public GameObject Anim;

    public Image Chapter81;
    public Image Chapter82;
    public Image Chapter83;
    public Image Chapter84;
    public Image Chapter85;
    public Image Chapter86;
    public Image Chapter87;
    public Image Chapter88;
    public Image Chapter89;
    public Image Chapter810;
    public Image Chapter811;
    public Image Chapter812;

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
        AddColor();//���D
        Invoke("ChangeImage", Time1);//����1
        Invoke("ChangeImage", Time2);//����2
        Invoke("ChangeImage", Time3);//����3
        Invoke("ChangeImage", Time4);//����4
        Invoke("ChangeImage", Time5);//����5
        Invoke("ChangeImage", Time6);//����6
        Invoke("ChangeImage", Time7);//����7
        Invoke("ChangeImage", Time8);//����8
        Invoke("ChangeImage", Time9);//����9
        Invoke("ChangeImage", Time10);//����10
        Invoke("ChangeImage", Time11);//����11
        Invoke("ReduceColor", Time12);//����12
        Invoke("CloseStopButton", Time13);//����
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
            Chapter81.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 2)
            Chapter82.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 3)
            Chapter83.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 4)
            Chapter84.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 5)
            Chapter85.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 6)
            Chapter86.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 7)
            Chapter87.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 8)
            Chapter88.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 9)
            Chapter89.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 10)
            Chapter810.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 11)
            Chapter811.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 12)
            Chapter812.color = new Color(1, 1, 1, ColorCount);
    }
    void End()
    {
        CancelInvoke();
        TotalTime = 0;
        ImageCount = 0;
        ColorCount = 0;
        Chapter81.color = new Color(1, 1, 1, 0);
        Chapter82.color = new Color(1, 1, 1, 0);
        Chapter83.color = new Color(1, 1, 1, 0);
        Chapter84.color = new Color(1, 1, 1, 0);
        Chapter85.color = new Color(1, 1, 1, 0);
        Chapter86.color = new Color(1, 1, 1, 0);
        Chapter87.color = new Color(1, 1, 1, 0);
        Chapter88.color = new Color(1, 1, 1, 0);
        Chapter89.color = new Color(1, 1, 1, 0);
        Chapter810.color = new Color(1, 1, 1, 0);
        Chapter811.color = new Color(1, 1, 1, 0);
        Chapter812.color = new Color(1, 1, 1, 0);
    }
}
