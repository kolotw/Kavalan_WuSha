using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void button1()
    {
        print("hello");
    }
    public void Ch1_WuSha()
    {
        SceneManager.LoadScene("Chapter1");
    }
    public void Ch2_澤蘭宮()
    {
        SceneManager.LoadScene("Chapter2");
    }
    public void Ch3_龍潭湖社區()
    {
        SceneManager.LoadScene("Chapter3");
    }
    public void Ch4_哲思書棧()
    {
        SceneManager.LoadScene("Chapter4");
    }
    public void lv1() 
    {
        SceneManager.LoadScene("試做第四關(整理到可以玩)");
    }
    public void 宜蘭地形() 
    {
        SceneManager.LoadScene("0820_3D地形展演");
    }
    public void button8() { }
    public void button9() { }
}
