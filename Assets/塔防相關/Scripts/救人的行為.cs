using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.UIElements;
public class 救人的行為 : MonoBehaviour
{
    NavMeshAgent 導航;
    //public Transform 目標;
    //GameObject[] 中繼點;

    public int hp = 30;

    public float 射程距離 = 2f;
    public Transform 最近敵人;
    Animator anim;

    public float 開火間距 = 1.6f;
    float fireTime;

    Text 血量;
    GameObject 血條;
    int OriHP;

    public Rig rig;
    float 最短距離 = 50f;
    bool 正在急救中 = false;
    public float 目標距離;
    public GameObject[] 合併目標;
    // Start is called before the first frame update
    void Start()
    {
        血量 = transform.Find("Canvas/血量").gameObject.GetComponent<Text>();
        血量.text = hp.ToString();
        血條 = transform.Find("Canvas/血條").gameObject;
        OriHP = hp;
        rig.weight = 1;

        fireTime = 開火間距;
        anim = GetComponent<Animator>();
        導航 = GetComponent<NavMeshAgent>();
        //搜目標();

    }
    // Update is called once per frame
    void Update()
    {

        搜目標();

        if (最近敵人 != null)
        {
            if(最近敵人.transform.tag == "病毒")
            {
                if (Vector3.Distance(this.transform.position, 最近敵人.transform.position) < 射程距離)
                {
                    anim.SetBool("Run", false);
                    導航.isStopped = true;
                    this.transform.LookAt(最近敵人);
                    this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);
                    //開火間距
                    if (Time.time > fireTime)
                    {
                        anim.SetTrigger("FIRE");
                        fireTime = Time.time + 開火間距;
                    }
                }
            }
            else if(最近敵人.transform.tag == "被救援者") 
            {
                if (最近敵人.tag != "漢人")
                {
                    目標距離 = Vector3.Distance(this.transform.position, 最近敵人.transform.position);
                    if (目標距離 < 0.8f)
                    {
                        anim.SetBool("Run", false);
                        anim.SetBool("HEALING", true);
                        導航.isStopped = true;
                        this.transform.LookAt(最近敵人);
                        this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);
                    }
                    else
                    {
                        anim.SetBool("HEALING", false);
                        導航.isStopped = false;
                        最近敵人 = null;
                        最短距離 = 50f;
                        return;
                        //導航.SetDestination(目標.position);
                        //anim.SetBool("Run", true);
                    }
                }
                
            }
            傳目標給角色();
        }
    }



    void 搜目標()
    {
        GameObject[] 病毒 = GameObject.FindGameObjectsWithTag("病毒");
        GameObject[] 被救者 = GameObject.FindGameObjectsWithTag("被救援者");
        // 合併目標
        合併目標 = 病毒.Concat(被救者).ToArray();

        if (合併目標.Length == 0)
        {
            anim.SetTrigger("WIN");
            導航.isStopped = true;
            anim.SetBool("Run", false);
        }
        else
        {
            float 目前距離;
            foreach (GameObject ee in 合併目標)
            {
                // 確保目標不是已被救援的「漢人」
                if (ee.tag == "漢人")
                {
                    continue;
                }

                目前距離 = Vector3.Distance(ee.transform.position, this.transform.position);
                if (目前距離 < 最短距離)
                {
                    最短距離 = 目前距離;
                    最近敵人 = ee.transform;
                }
            }
        }

        if (最近敵人 != null)
        {
            if (!正在急救中)
            {
                導航.isStopped = false;
                導航.SetDestination(最近敵人.position);
                anim.SetBool("Run", true);
            }
        }
        else
        {
            最短距離 = 50f;
        }
    }

    void 傳目標給角色()
    {
        GetComponent<漢人砍劈>().目標 = 最近敵人;
        transform.Find("Rig 1/HeadAim").gameObject.GetComponent<MultiAimConstraint>().data.sourceObjects
            = new WeightedTransformArray { new WeightedTransform(最近敵人, 1) };
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "病毒武器")
        {
            Destroy(other.gameObject);
            hp--;
            血量.text = hp.ToString();
            float blood = (float)hp / (float)OriHP;
            血條.transform.localScale = new Vector3(blood, 1, 1);

            if (hp <= 0) { Destroy(this.gameObject); }
        }
    }
    public void 急救中()
    {
        正在急救中 = true;
        導航.isStopped = true;
        anim.SetBool("Run", false);
    }
    public void 急救完成()
    {
        最近敵人.tag = "漢人";

        最近敵人.GetComponent<Collider>().enabled = false;
        最近敵人.GetComponent<Animator>().SetTrigger("HEAL");
        導航.isStopped = true;
        最短距離 = 50f;
        最近敵人 = null;
        最近敵人 = null;
        正在急救中 = false;
        anim.SetBool("HEALING", false);
        搜目標();
    }
}
