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
    漢人武器,
    噶瑪蘭武器,
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

    private GameObject[] 敵人;
    private GameObject[] 中繼點;
    public float 射程範圍 = 4f;
    private float 目前距離;
    private float 最短距離 = 50f;
    public Transform 攻擊目標;
    private Vector3 dir;
    private Vector3 發射點RAY;

    private Quaternion 旋轉;

    private bool 開火 = false;
    public float 開火間距 = 2f;
    private float fireTime;

    public int hp = 10;
    private int OrigHP;
    private GameObject 血條;
    private Text 血量文字;

    // 弓箭手
    private NavMeshAgent 導航;
    private Animator 動畫;
    public Rig rig;
    public Transform 最近敵人;

    void Start()
    {
        OrigHP = hp;
        fireTime = 開火間距;
        血條 = transform.Find("Canvas/血條").gameObject;
        血量文字 = transform.Find("Canvas/血量").gameObject.GetComponent<Text>();
        血量文字.text = hp.ToString();
        動畫 = GetComponent<Animator>();

        if (攻方或守方 != OptionsA.守方)  // 攻方、救援者(也算是攻方)、被救援者(回家) 都會移動，只有守方不會移動
        {
            導航 = GetComponent<NavMeshAgent>();
            搜索目標();
        }

        if (this.tag == "漢人" && rig != null)
        {
            rig.weight = 1;
        }
    }

    void Update()
    {
        if (攻方或守方 == OptionsA.攻方)
        {
            處理攻方邏輯();
        }
        else if (攻方或守方 == OptionsA.守方)
        {
            處理守方邏輯();
        }
    }

    private void 處理攻方邏輯()
    {
        if (GameObject.Find("/target") == null)
        {
            導航.SetDestination(this.transform.position);
            導航.isStopped = true;
            動畫.SetTrigger("WIN");
            return;
        }

        if (最近敵人 != null)
        {
            if (最近敵人.tag == "被救援者")
            {
                最近敵人 = null;
            }

            if (攻擊目標 != null && Vector3.Distance(this.transform.position, 攻擊目標.position) < 射程範圍)
            {
                停止移動並攻擊();
            }
        }
        else
        {
            最短距離 = 50f;
            搜索目標();
        }

        if (攻擊目標 != null && (攻擊目標.tag == "中繼點" || 攻擊目標.name == "target"))
        {
            處理中繼點目標();
        }
    }

    private void 處理守方邏輯()
    {
        if (敵人.Length == 0) return;

        if (開火 && Time.time > fireTime)
        {
            rig.weight = 1;
            動畫.SetTrigger("FIRE");
            fireTime = Time.time + 開火間距;
        }
    }

    private void 停止移動並攻擊()
    {
        動畫.SetBool("Run", false);
        導航.isStopped = true;
        this.transform.LookAt(最近敵人);
        this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);

        if (Time.time > fireTime)
        {
            動畫.SetTrigger("FIRE");
            fireTime = Time.time + 開火間距;
        }
    }

    private void 處理中繼點目標()
    {
        if (Vector3.Distance(this.transform.position, 攻擊目標.position) < 0.4f)
        {
            Destroy(攻擊目標.gameObject);
        }
    }

    private void 傳攻擊目標給角色()
    {
        if (this.tag == "病毒")
        {
            return;
        }
        print("傳攻擊目標給角色" + 攻擊目標.gameObject.name);
        switch (目標敵人)
        {
            case OptionsB.漢人:
                傳漢人目標();
                break;
            case OptionsB.噶瑪蘭:
                傳噶瑪蘭目標();
                break;
            default:
                break;
        }
    }

    private void 傳漢人目標()
    {
        if (攻擊目標 == null) return;

        if (攻擊目標.transform.name == "噶瑪蘭-女火球(Clone)")
        {
            GetComponent<噶瑪蘭_攻擊_巫術火球>().目標 = 攻擊目標;
        }
        else if (攻擊目標.transform.name == "噶瑪蘭-男弓箭(Clone)")
        {
            GetComponent<噶瑪蘭_攻擊_射箭>().目標 = 攻擊目標;
        }
    }

    private void 傳噶瑪蘭目標()
    {
        if (攻擊目標.transform.name == "漢人_農夫_鋤頭(Clone)")
        {
            print("HERE 農夫");
            GetComponent<漢人砍劈>().目標 = 攻擊目標;
        }
        else if (攻擊目標.transform.name == "漢人-弓箭手 F1(Clone)")
        {
            print("HERE 弓箭手");
            GetComponent<漢人射箭>().目標 = 攻擊目標;
        }

        var headAim = transform.Find("Rig 1/HeadAim").gameObject.GetComponent<MultiAimConstraint>();
        headAim.data.sourceObjects = new WeightedTransformArray { new WeightedTransform(攻擊目標, 1) };
    }

    private void 傳給救援者目標()
    {
        GetComponent<漢人砍劈>().目標.position = 攻擊目標.position;

        var headAim = transform.Find("Rig 1/HeadAim").gameObject.GetComponent<MultiAimConstraint>();
        headAim.data.sourceObjects = new WeightedTransformArray { new WeightedTransform(攻擊目標, 1) };
    }

    private void 搜索目標()
    {
        // 先尋找所有中繼點
        中繼點 = GameObject.FindGameObjectsWithTag("中繼點");

        if (中繼點.Length > 0)
        {
            // 如果有中繼點，隨機選擇一個作為攻擊目標
            int r = Random.Range(0, 中繼點.Length);
            攻擊目標 = 中繼點[r].transform;
        }
        else
        {
            // 如果沒有中繼點，則尋找敵人
            敵人 = GameObject.FindGameObjectsWithTag(目標敵人.ToString());

            if (敵人.Length > 0)
            {
                最短距離 = 射程範圍;
                foreach (GameObject e in 敵人)
                {
                    目前距離 = Vector3.Distance(this.transform.position, e.transform.position);
                    if (目前距離 < 最短距離)
                    {
                        最近敵人 = e.transform;
                        最短距離 = 目前距離;
                        攻擊目標 = e.transform;
                    }
                }
            }
            else
            {
                // 如果既沒有中繼點也沒有敵人，且場景中存在“target”，將其設為目標
                攻擊目標 = GameObject.Find("/target")?.transform;
            }
        }

        // 如果找到了攻擊目標，設置導航目標並更新動畫狀態
        if (攻擊目標 != null)
        {
            if (攻方或守方 == OptionsA.攻方)
            {
                導航.isStopped = false;
                導航.SetDestination(攻擊目標.position);
                動畫.SetBool("Run", true);
            }

            傳攻擊目標給角色();
        }
        else
        {
            if (攻方或守方 == OptionsA.攻方)
            {
                // 沒有找到目標時停止角色
                導航.isStopped = true;
                動畫.SetBool("Run", false);
            }
            搜索目標();
        }
    }


    private void 旋轉至攻擊目標()
    {
        if (dir != Vector3.zero)
        {
            旋轉 = Quaternion.LookRotation(dir * -1, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, 旋轉, 20 * Time.deltaTime);
            this.transform.eulerAngles = new Vector3(0f, this.transform.eulerAngles.y, 0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(會損血的武器.ToString()))
        {
            減血(other.gameObject);
        }
    }

    private void 減血(GameObject tt)
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
