using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int chapernum;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Menu()
    {
        SceneManager.LoadScene("eBooks/Scenes/Menu");
    }
    public void LastChapter()
    {
        SceneManager.LoadScene("eBooks/Scenes/Chapter" + (chapernum - 1));
    }
    public void NextChapter()
    {
        SceneManager.LoadScene("eBooks/Scenes/Chapter" + (chapernum + 1));
    }
    public void Chapter1()
    {
        SceneManager.LoadScene("eBooks/Scenes/Chapter1");
    }

}
