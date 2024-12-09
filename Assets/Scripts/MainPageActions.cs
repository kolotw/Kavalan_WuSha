using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPageActions : MonoBehaviour
{
   
    public void 走讀吳沙() {
        SceneManager.LoadScene("01_走讀吳沙");
    }
    public void 塔防遊戲() {
        SceneManager.LoadScene("02_塔防遊戲");
    }
    public void 工作人員()
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
    public void TD_Level1() { SceneManager.LoadScene("第一關");}
    public void TD_Level2() { SceneManager.LoadScene("第二關"); }
    public void TD_Level3() { SceneManager.LoadScene("第三關"); }
    public void TD_Level4() { SceneManager.LoadScene("第四關"); }
    public void TD_Level5() { SceneManager.LoadScene("第五關"); }
    public void TD_Level6() { SceneManager.LoadScene("第六關"); }
    public void TD_Back() { SceneManager.LoadScene("02_塔防遊戲"); }

    public void BackHome() { SceneManager.LoadScene("00_HomePage"); }
}
