using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int chapernum;
    string menu = "01_¨«Åª§d¨F";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Menu()
    {
        SceneManager.LoadScene(menu);
    }
    public void LastChapter()
    {
        SceneManager.LoadScene("Chapter" + (chapernum - 1));
    }
    public void NextChapter()
    {
        SceneManager.LoadScene("Chapter" + (chapernum + 1));
    }
    public void Chapter1()
    {
        SceneManager.LoadScene("Chapter1");
    }

}
