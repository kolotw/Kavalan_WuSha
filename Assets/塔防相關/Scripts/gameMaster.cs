using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameMaster : MonoBehaviour
{
    //------------- 物件 -------------------
    public GameObject[] 砲台 = new GameObject[2];
    public GameObject[] 敵人 = new GameObject[2];

    //------------- 資源上限 -------------------
    public int 砲A上限 = 5;
    public int 砲B上限 = 5;

    //------------- 吳沙救護生成點 -------------------
    public GameObject 吳沙;


    //------------- 攻勢 -------------------
    public int 目前關卡 = 1;
    public float 每幾秒產生一波 = 10f;
    public float 每次產生敵人的間隔 = 1f;
    public int 每次生成敵人數 = 3;
    public int 攻勢上限 = 1;
    public int 目前第幾波攻勢 = 0;
    float 攻擊倒數;

    //------------- 產生敵人 -------------------
    GameObject[] 生成點;
    int 第幾個生成點;


    //------------- UI TEXT -------------------
    public Text 攻勢文字; // 第幾關，第幾波，倒數… 勝敗訊息
    public Text 資源文字;

    bool EndGame = false;
    bool 已熄火 = false;

    public GameObject 開始畫面;

    // Start is called before the first frame update
    void Start()
    {
        生成點 = GameObject.FindGameObjectsWithTag("生成點");
        攻擊倒數 = 每幾秒產生一波;
        
    }
    void 熄火() {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("噶瑪蘭");
        foreach (GameObject t in turrets)
        {
            if (t.GetComponent<Animator>() != null)
            {
                t.transform.Find("RigHead/HeadAim").gameObject.SetActive(false);
                //t.GetComponent<Animator>().SetBool("WIN", true);
            }
            t.GetComponent<噶瑪蘭_屬性設定>().enabled = false;
        }
        Camera.main.gameObject.GetComponent<deploy>().enabled = false;
        已熄火 = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (開始畫面.activeInHierarchy == true) return;

        if (EndGame) {
            if(!已熄火)
                熄火();
            return;
        }
       if (GameObject.Find("target") == null)
        {
            攻勢文字.text = "你輸了";
            EndGame = true;
        }
        else
        {
            攻勢文字.text = "第" + 目前關卡.ToString() + "關 第" + 目前第幾波攻勢.ToString()
               + "波攻勢 還有" + Mathf.RoundToInt(攻擊倒數).ToString() + "秒";
            資源文字.text = "資源：\n砲台A：" + 砲A上限.ToString() + "\n砲台B：" + 砲B上限.ToString();

            if (目前第幾波攻勢 <= 攻勢上限)
            {
                攻擊倒數 -= Time.deltaTime;
                if (攻擊倒數 < 0)
                {
                    StartCoroutine(一波敵人());
                    攻擊倒數 = 每幾秒產生一波;
                }
            }
            else
            {
                攻擊倒數 = 0;
                if (GameObject.FindGameObjectsWithTag("漢人").Length == 0)
                {
                    攻勢文字.text = "你贏了";
                    EndGame = true;
                }
            }
        }
    }
    void 生成敵人()
    {
        int e = Random.Range(0, 敵人.Length);
        Instantiate(敵人[e], 生成點[第幾個生成點].transform.position, Quaternion.identity);
    }

    IEnumerator 一波敵人()
    {
        目前第幾波攻勢++;
        第幾個生成點 = Random.Range(0, 生成點.Length);
        
        for (int i = 0; i < 每次生成敵人數; i++)
        {
            生成敵人();
            yield return new WaitForSeconds(每次產生敵人的間隔);
        }
    }

    public void 按下開始()
    {
        開始畫面.SetActive(false);
        StartCoroutine(一波敵人());
    }
}
