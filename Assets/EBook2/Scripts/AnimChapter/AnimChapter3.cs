using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimChapter3 : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject StopButton;
    public GameObject ScrollView;
    public GameObject Content;
    public GameObject Anim;

    public Image Chapter31;
    public Image Chapter32;
    public Image Chapter33;
    public Image Chapter34;
    public Image Chapter35;
    public Image Chapter36;
    public Image Chapter37;
    public Image Chapter38;
    public Image Chapter39;
    public Image Chapter310;

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
        Invoke("ReduceColor", Time10);//����10
        Invoke("CloseStopButton", Time11);//����
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
            Chapter31.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 2)
            Chapter32.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 3)
            Chapter33.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 4)
            Chapter34.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 5)
            Chapter35.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 6)
            Chapter36.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 7)
            Chapter37.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 8)
            Chapter38.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 9)
            Chapter39.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 10)
            Chapter310.color = new Color(1, 1, 1, ColorCount);
    }
    void End()
    {
        CancelInvoke();
        TotalTime = 0;
        ImageCount = 0;
        ColorCount = 0;
        Chapter31.color = new Color(1, 1, 1, 0);
        Chapter32.color = new Color(1, 1, 1, 0);
        Chapter33.color = new Color(1, 1, 1, 0);
        Chapter34.color = new Color(1, 1, 1, 0);
        Chapter35.color = new Color(1, 1, 1, 0);
        Chapter36.color = new Color(1, 1, 1, 0);
        Chapter37.color = new Color(1, 1, 1, 0);
        Chapter38.color = new Color(1, 1, 1, 0);
        Chapter39.color = new Color(1, 1, 1, 0);
        Chapter310.color = new Color(1, 1, 1, 0);
    }
}
