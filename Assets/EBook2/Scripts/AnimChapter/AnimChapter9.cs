using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnimChapter9 : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject StopButton;
    public GameObject ScrollView;
    public GameObject Content;
    public GameObject Anim;

    public Image Chapter91;
    public Image Chapter92;
    public Image Chapter93;
    public Image Chapter94;
    public Image Chapter95;
    public Image Chapter96;
    public Image Chapter97;
    public Image Chapter98;
    public Image Chapter99;
    public Image Chapter910;
    public Image Chapter911;

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
        AddColor();//夹肈
        Invoke("ChangeImage", Time1);//场だ1
        Invoke("ChangeImage", Time2);//场だ2
        Invoke("ChangeImage", Time3);//场だ3
        Invoke("ChangeImage", Time4);//场だ4
        Invoke("ChangeImage", Time5);//场だ5
        Invoke("ChangeImage", Time6);//场だ6
        Invoke("ChangeImage", Time7);//场だ7
        Invoke("ChangeImage", Time8);//场だ8
        Invoke("ChangeImage", Time9);//场だ9
        Invoke("ChangeImage", Time10);//场だ10
        Invoke("ReduceColor", Time11);//场だ11
        Invoke("CloseStopButton", Time12);//挡
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
            Chapter91.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 2)
            Chapter92.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 3)
            Chapter93.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 4)
            Chapter94.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 5)
            Chapter95.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 6)
            Chapter96.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 7)
            Chapter97.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 8)
            Chapter98.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 9)
            Chapter99.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 10)
            Chapter910.color = new Color(1, 1, 1, ColorCount);
        if (ImageCount == 11)
            Chapter911.color = new Color(1, 1, 1, ColorCount);
    }
    void End()
    {
        CancelInvoke();
        TotalTime = 0;
        ImageCount = 0;
        ColorCount = 0;
        Chapter91.color = new Color(1, 1, 1, 0);
        Chapter92.color = new Color(1, 1, 1, 0);
        Chapter93.color = new Color(1, 1, 1, 0);
        Chapter94.color = new Color(1, 1, 1, 0);
        Chapter95.color = new Color(1, 1, 1, 0);
        Chapter96.color = new Color(1, 1, 1, 0);
        Chapter97.color = new Color(1, 1, 1, 0);
        Chapter98.color = new Color(1, 1, 1, 0);
        Chapter99.color = new Color(1, 1, 1, 0);
        Chapter910.color = new Color(1, 1, 1, 0);
        Chapter911.color = new Color(1, 1, 1, 0);
    }
}
