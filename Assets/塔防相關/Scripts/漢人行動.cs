using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class 漢人行動 : MonoBehaviour
{
    NavMeshAgent 導航;
    Animator 動畫;

    public Transform 攻擊目標;
    GameObject[] 合併目標;

    public int hp = 30;

    public float 射程距離 = 2f;
    float 最短距離 = 50f;

    public float 開火間距 = 1.6f;
    float fireTime;

    Text 血量;
    GameObject 血條;
    int OriHP;

    public Rig rig;

    void Start()
    {
        血量 = transform.Find("Canvas/血量").gameObject.GetComponent<Text>();
        血量.text = hp.ToString();
        血條 = transform.Find("Canvas/血條").gameObject;
        OriHP = hp;
        rig.weight = 1;

        fireTime = 開火間距;
        動畫 = GetComponent<Animator>();
        導航 = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (GameObject.Find("/target") == null)
        {
            停止導航並播放勝利動畫();
            return;
        }

        搜目標();

        if (攻擊目標 == null)
        {
            重置最短距離();
            return;
        }

        if (攻擊目標 != null && (攻擊目標.tag == "中繼點" || 攻擊目標.name == "target"))
        {
            如果目標靠近則銷毀();
        }
        else if (攻擊目標 != null && 攻擊目標.tag != "中繼點" && 攻擊目標.name != "target")
        {
            如果在射程內則攻擊();
        }
    }

    void 停止導航並播放勝利動畫()
    {
        導航.SetDestination(this.transform.position);
        導航.isStopped = true;
        動畫.SetTrigger("WIN");
    }

    void 重置最短距離()
    {
        最短距離 = 50f;
    }

    void 如果目標靠近則銷毀()
    {
        if (Vector3.Distance(攻擊目標.position, this.transform.position) < 0.3f)
        {
            Destroy(攻擊目標.gameObject);
        }
    }

    void 如果在射程內則攻擊()
    {
        if (Vector3.Distance(this.transform.position, 攻擊目標.transform.position) < 射程距離)
        {
            動畫.SetBool("Run", false);
            導航.isStopped = true;
            面向目標();
            嘗試開火();
        }
    }

    void 面向目標()
    {
        this.transform.LookAt(攻擊目標);
        this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);
    }

    void 嘗試開火()
    {
        if (Time.time > fireTime)
        {
            動畫.SetTrigger("FIRE");
            fireTime = Time.time + 開火間距;
        }
    }

    void 搜目標()
    {
        GameObject[] 中繼點 = GameObject.FindGameObjectsWithTag("中繼點");
        GameObject[] 噶瑪蘭 = GameObject.FindGameObjectsWithTag("噶瑪蘭");

        合併目標 = 中繼點.Concat(噶瑪蘭).ToArray();

        if (合併目標.Length == 0)
        {
            設置攻擊目標為Target();
        }
        else
        {
            選擇最近的目標();
        }

        如果有攻擊目標則導航();
    }

    void 設置攻擊目標為Target()
    {
        if (GameObject.Find("/target") != null)
        {
            攻擊目標 = GameObject.Find("/target").transform;
        }
    }

    void 選擇最近的目標()
    {
        float 目前距離;
        foreach (GameObject 目標 in 合併目標)
        {
            目前距離 = Vector3.Distance(目標.transform.position, this.transform.position);
            if (目前距離 < 最短距離)
            {
                最短距離 = 目前距離;
                攻擊目標 = 目標.transform;
            }
        }
    }

    void 如果有攻擊目標則導航()
    {
        if (攻擊目標 != null)
        {
            導航.isStopped = false;
            導航.SetDestination(攻擊目標.position);
            動畫.SetBool("Run", true);
            如果目標不是Target則傳遞目標給角色();
        }
    }

    void 如果目標不是Target則傳遞目標給角色()
    {
        if (攻擊目標.name != "target")
        {
            傳目標給角色();
        }
    }

    void 傳目標給角色()
    {
        if (this.transform.name == "漢人_農夫_鋤頭(Clone)")
        {
            GetComponent<漢人砍劈>().目標 = 攻擊目標;
        }
        else if (this.transform.name == "漢人-弓箭手 F1(Clone)")
        {
            GetComponent<漢人射箭>().目標 = 攻擊目標;
        }

        transform.Find("Rig 1/HeadAim").gameObject.GetComponent<MultiAimConstraint>().data.sourceObjects
            = new WeightedTransformArray { new WeightedTransform(攻擊目標, 1) };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "噶瑪蘭武器")
        {
            受到攻擊(other);
        }
    }

    void 受到攻擊(Collider other)
    {
        Destroy(other.gameObject);
        hp--;
        更新血量顯示();
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void 更新血量顯示()
    {
        血量.text = hp.ToString();
        float blood = (float)hp / (float)OriHP;
        血條.transform.localScale = new Vector3(blood, 1, 1);
    }
}
