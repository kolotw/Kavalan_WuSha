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
    public void Ch2_�A���c()
    {
        SceneManager.LoadScene("Chapter2");
    }
    public void Ch3_�s������()
    {
        SceneManager.LoadScene("Chapter3");
    }
    public void Ch4_����Ѵ�()
    {
        SceneManager.LoadScene("Chapter4");
    }
    public void lv1() 
    {
        SceneManager.LoadScene("�հ��ĥ|��(��z��i�H��)");
    }
    public void �y���a��() 
    {
        SceneManager.LoadScene("0820_3D�a�ήi�t");
    }
    public void button8() { }
    public void button9() { }
}
