using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public enum OptionsA
{
    攻方,
    守方
}
public enum OptionsB
{
    漢人,
    噶瑪蘭,
    救援者,
    被救援者,
    病毒
}
public enum OptionsC
{
    攻擊方武器,
    防守方武器,
    病毒
}
public enum OptionsD
{
    中繼點,
    target
}

public class 整合行動 : MonoBehaviour
{
    public OptionsA 攻方或守方;
    public OptionsB 目標敵人;
    public OptionsC 會損血的武器;
    public OptionsD 行動目標;

    //---------------------------------------------------

    GameObject[] 敵人;
    GameObject[] 中繼點;
    public float 射程範圍 = 4f;
    public float 搜尋範圍 = 10f;
    float 目前距離;
    public Transform 目標;
    Vector3 dir;
    Vector3 發射點RAY;

    Quaternion 旋轉;

    bool 開火 = false;
    public float 開火間距 = 2f;
    float fireTime;

    public int hp = 10;
    int OrigHP;
    GameObject 血條;
    Text 血量文字;

    //弓箭手
    NavMeshAgent 導航;
    Animator 動畫;
    public Rig rig;
    public Transform 最近敵人;

    // Start is called before the first frame update
    void Start()
    {
        OrigHP = hp;
        fireTime = 開火間距;
        血條 = transform.Find("Canvas/血條").gameObject;
        血量文字 = transform.Find("Canvas/血量").gameObject.GetComponent<Text>();
        血量文字.text = hp.ToString();
        動畫 = GetComponent<Animator>();

        // 攻方、病毒、救援者(也算是攻方)、被救援者(回家) 都會移動，只有守方不會移動
        if (攻方或守方.ToString() != "攻方")
        {
            //守方
            會損血的武器 = OptionsC.攻擊方武器;
        }
        else
        {
            //攻方
            導航 = GetComponent<NavMeshAgent>();
            會損血的武器 = OptionsC.防守方武器;
            搜尋目標或敵人();
        }

        if (this.tag == "漢人")
        {
            rig.weight = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (攻方或守方)
        {
            case OptionsA.攻方:
                if (GameObject.Find("/target") == null)
                {
                    導航.SetDestination(this.transform.position);
                    導航.isStopped = true;
                    動畫.SetTrigger("WIN");
                    return;
                }

                // 新增檢查目標是否已被銷毀或 tag 被更改
                if (目標 == null || 目標.gameObject == null || 目標.tag == "被救援者")
                {
                    最近敵人 = null;
                    搜尋目標或敵人();
                    return;
                }

                // 檢查目標距離以觸發攻擊行為或摧毀中繼點
                if (目標 != null)
                {
                    float 目標距離 = Vector3.Distance(this.transform.position, 目標.position);
                    if (目標.name == "target" && 目標距離 < 0.4f)
                    {
                        Destroy(目標.gameObject);
                        return;
                    }
                    if (目標.tag == "中繼點" && 目標距離 < 0.4f)
                    {
                        Destroy(目標.gameObject);
                        搜尋目標或敵人(); // 中繼點被摧毀後立即尋找下一個目標
                    }
                    else if (目標.tag == 目標敵人.ToString() && 目標距離 < 射程範圍)
                    {
                        動畫.SetBool("Run", false);
                        導航.isStopped = true;
                        this.transform.LookAt(目標);
                        this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);

                        // 開火間距
                        if (Time.time > fireTime)
                        {
                            動畫.SetTrigger("FIRE");
                            fireTime = Time.time + 開火間距;
                        }
                    }
                    else
                    {
                        // 向目標移動
                        動畫.SetBool("Run", true);
                        導航.isStopped = false;
                        導航.SetDestination(目標.position);
                    }
                }
                else
                {
                    搜尋目標或敵人();
                }
                break;

            case OptionsA.守方:
                搜尋目標或敵人();
                if (敵人.Length == 0) return;
                if (開火)
                {
                    if (Time.time > fireTime)
                    {
                        rig.weight = 1;
                        動畫.SetTrigger("FIRE");
                        fireTime = Time.time + 開火間距;
                    }
                }
                break;

            default:
                break;
        }
    }

    void 傳目標給角色()
    {
        switch (目標敵人)
        {
            case OptionsB.漢人:
                if (目標 == null) return;
                if (transform.name == "噶瑪蘭-女火球(Clone)")
                {
                    GetComponent<噶瑪蘭_攻擊_巫術火球>().目標 = 目標;
                }
                else if (transform.name == "噶瑪蘭-男弓箭(Clone)")
                {
                    GetComponent<噶瑪蘭_攻擊_射箭>().目標 = 目標;
                }
                break;
            case OptionsB.噶瑪蘭:
                if (this.transform.name == "漢人_農夫_鋤頭(Clone)")
                {
                    GetComponent<漢人砍劈>().目標 = 目標;
                }
                else if (this.transform.name == "漢人-弓箭手 F1(Clone)")
                {
                    GetComponent<漢人射箭>().目標 = 目標;
                }
                transform.Find("Rig 1/HeadAim").gameObject.GetComponent<MultiAimConstraint>().data.sourceObjects
                    = new WeightedTransformArray { new WeightedTransform(目標, 1) };
                break;
            case OptionsB.救援者:
                GetComponent<漢人砍劈>().目標.position = 目標.position;
                transform.Find("Rig 1/HeadAim").gameObject.GetComponent<MultiAimConstraint>().data.sourceObjects
                    = new WeightedTransformArray { new WeightedTransform(目標, 1) };
                break;
            case OptionsB.被救援者:
                break;
            case OptionsB.病毒:
                break;
            default:
                break;
        }
    }

    // 整合的搜尋目標或敵人方法
    void 搜尋目標或敵人()
    {
        Transform 最近中繼點 = null;
        Transform 最近敵人 = null;
        float 最短距離 = float.MaxValue;

        // 尋找最近的中繼點
        中繼點 = GameObject.FindGameObjectsWithTag("中繼點");
        foreach (GameObject 中繼 in 中繼點)
        {
            float 距離 = Vector3.Distance(this.transform.position, 中繼.transform.position);
            if (距離 < 最短距離)
            {
                最近中繼點 = 中繼.transform;
                最短距離 = 距離;
            }
        }

        // 尋找最近的敵人，忽略 tag 為 "被救援者" 的敵人
        敵人 = GameObject.FindGameObjectsWithTag(目標敵人.ToString());
        foreach (GameObject 敵 in 敵人)
        {
            // 忽略 tag 變為 "被救援者" 的敵人
            if (敵.tag == "被救援者")
                continue;

            float 距離 = Vector3.Distance(this.transform.position, 敵.transform.position);
            if (距離 < 最短距離)
            {
                最近敵人 = 敵.transform;
                最短距離 = 距離;
            }
        }

        // 比較最近的中繼點和敵人，選擇距離最近的作為目標
        if (最近中繼點 != null && 最近敵人 != null)
        {
            if (Vector3.Distance(this.transform.position, 最近中繼點.position) < Vector3.Distance(this.transform.position, 最近敵人.position))
            {
                目標 = 最近中繼點;
            }
            else
            {
                目標 = 最近敵人;
            }
        }
        else if (最近中繼點 != null)
        {
            目標 = 最近中繼點;
        }
        else if (最近敵人 != null)
        {
            目標 = 最近敵人;
        }
        else
        {
            目標 = GameObject.Find("/target")?.transform;
        }

        // 設定導航目的地
        if (目標 != null)
        {
            導航.isStopped = false;
            導航.SetDestination(目標.position);
            動畫.SetBool("Run", true);
            傳目標給角色();
        }
        else
        {
            導航.isStopped = true;
            動畫.SetBool("Run", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == 會損血的武器.ToString())
        {
            減血(other.gameObject);
        }
    }

    void 減血(GameObject tt)
    {
        Destroy(tt.gameObject);
        hp--;
        float 血量 = (float)hp / (float)OrigHP;
        血條.transform.localScale = new Vector3(血量, 1f, 1f);
        血量文字.text = hp.ToString();
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
