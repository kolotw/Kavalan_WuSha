using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPageActions : MonoBehaviour
{
   
    public void ��Ū�d�F() {
        SceneManager.LoadScene("01_��Ū�d�F");
    }
    public void �𨾹C��() {
        SceneManager.LoadScene("02_�𨾹C��");
    }
    public void �u�@�H��()
    {
        SceneManager.LoadScene("03_CreditList");
    }
    public void eBookButton1()
    {
        SceneManager.LoadScene("Chapter1");
    }
    public void eBookButton2()
    {
        SceneManager.LoadScene("Chapter2");
    }
    public void eBookButton3() {
        SceneManager.LoadScene("Chapter3");
    }
    public void eBookButton4() {
        SceneManager.LoadScene("Chapter4");
    }
    public void eBookButton5() {
        SceneManager.LoadScene("Chapter5");
    }
    public void eBookButton6() {
        SceneManager.LoadScene("Chapter6");
    }
    public void eBookButton7() {
        SceneManager.LoadScene("Chapter7");
    }
    public void eBookButton8() {
        SceneManager.LoadScene("Chapter8");
    }   
    public void eBookButton9() {
        SceneManager.LoadScene("Chapter9");
    }
    public void TD_Level1() { SceneManager.LoadScene("�Ĥ@��");}
    public void TD_Level2() { SceneManager.LoadScene("�ĤG��"); }
    public void TD_Level3() { SceneManager.LoadScene("�ĤT��"); }
    public void TD_Level4() { SceneManager.LoadScene("�ĥ|��"); }
    public void TD_Level5() { SceneManager.LoadScene("�Ĥ���"); }
    public void TD_Level6() { SceneManager.LoadScene("�Ĥ���"); }
    public void TD_Back() { SceneManager.LoadScene("02_�𨾹C��"); }

    public void BackHome() { SceneManager.LoadScene("00_HomePage"); }
}
