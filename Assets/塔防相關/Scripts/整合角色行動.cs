using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class 整合角色行動 : MonoBehaviour
{
    public bool 是攻擊方; // true 表示是攻擊方，false 表示是防守方

    private GameObject[] 敵人;
    public float 射程範圍 = 4f;
    public float 停止距離 = 2f;
    private float 目前距離;
    private float 最短距離 = 50f;
    public Transform 目標;
    private Vector3 dir;
    private Vector3 發射點RAY;
    private Quaternion 旋轉;
    private bool 開火 = false;
    public float 火力間隔 = 2f;
    private float fireTime;

    public int hp = 30;
    private int OrigHP;
    private GameObject 血條;
    private Text 血量文字;

    private Animator anim;
    private NavMeshAgent 導航;
    public Rig rig;

    string enemyTag = string.Empty;

    void Start()
    {
        OrigHP = hp;
        fireTime = Time.time;
        血條 = transform.Find("Canvas/血條").gameObject;
        血量文字 = transform.Find("Canvas/血量").gameObject.GetComponent<Text>();
        血量文字.text = hp.ToString();
        anim = GetComponent<Animator>();
        導航 = GetComponent<NavMeshAgent>();

        if (this.tag == "攻擊方") 是攻擊方 = true;
        if (this.tag == "防守方") { 
            是攻擊方 = false; 
            導航.enabled = false;
        }

        if (rig != null)
        {
            rig.weight = 1;
        }
    }

    void Update()
    {
        if (是攻擊方)
        {
            this.tag = "攻擊方";
            enemyTag = "防守方";
            攻擊方行動();
        }
        else
        {
            this.tag = "防守方";
            enemyTag = "攻擊方";
            防守方行動();
        }
    }

    private void 攻擊方行動()
    {
        找敵人(enemyTag);

        if (目標 != null)
        {
            float 距離到目標 = Vector3.Distance(this.transform.position, 目標.position);

            if (距離到目標 < 停止距離)
            {
                if ((目標.tag == "中繼點") || (目標.name == "target"))
                {
                    if (距離到目標 < 0.25f)
                    {
                        Destroy(目標.gameObject);
                        目標 = null; // 目標被摧毀後設置為 null
                        尋找下一個目標(); // 確保在目標摧毀後繼續行動
                        return;
                    }
                }
                else
                {
                    
                    導航.SetDestination(this.transform.position);
                    if (導航.enabled) 導航.isStopped = true;
                    anim.SetBool("Run", false);

                    if (開火 && Time.time > fireTime + 火力間隔)
                    {
                        if (rig != null)
                        {
                            rig.weight = 1;
                        }
                        anim.SetTrigger("FIRE");
                        fireTime = Time.time;
                    }
                }
            }
            else
            {
                導航.SetDestination(目標.position);
                導航.isStopped = false;
                anim.SetBool("Run", true);
            }
        }
    }

    private void 尋找下一個目標()
    {
        // 如果目標被摧毀或不存在，尋找下一個目標
        找敵人(enemyTag);

        if (目標 != null)
        {
            導航.SetDestination(目標.position);
            導航.isStopped = false;
            anim.SetBool("Run", true);
        }
        else
        {
            // 沒有找到新目標，停止導航
            導航.isStopped = true;
            anim.SetBool("Run", false);
        }
    }


    private void 防守方行動()
    {
        if (GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().isWin)
        {
            anim.SetTrigger("WIN");
            return;
        }
        找敵人(enemyTag);
        //if (敵人.Length == 0)
        //{
        //    停止導航並播放勝利動畫();
        //    return;
        //}

        if (開火 && Time.time > fireTime + 火力間隔)
        {
            if (rig != null)
            {
                rig.weight = 1;
            }
            anim.SetTrigger("FIRE");
            fireTime = Time.time;
        }
    }

    private void 傳目標給角色()
    {
        if (目標 == null) return;

        整合攻擊行動 攻擊行動 = GetComponent<整合攻擊行動>();

        if (攻擊行動 != null)
        {
            攻擊行動.目標 = 目標;
            攻擊行動.是攻擊方 = 是攻擊方;

            if (transform.name.Contains("漢人"))
            {
                transform.Find("Rig 1/HeadAim").gameObject.GetComponent<MultiAimConstraint>().data.sourceObjects
                    = new WeightedTransformArray { new WeightedTransform(目標, 1) };
            }
        }
    }


    private void 找敵人(string 敵人標籤)
    {
        敵人 = GameObject.FindGameObjectsWithTag(敵人標籤);

        if (敵人標籤 == "防守方")
        {
            GameObject[] 中繼點 = GameObject.FindGameObjectsWithTag("中繼點");

            // 合併敵人和中繼點
            敵人 = 敵人.Concat(中繼點).ToArray();

            // 如果合併後仍然沒有敵人或中繼點，則檢查"/target"
            if (敵人.Length == 0)
            {
                GameObject 目標物 = GameObject.Find("/target");

                if (目標物 == null)
                {
                    //WIN
                    anim.SetTrigger("WIN");
                    導航.isStopped = true;
                    return;
                }
                else
                {
                    目標 = 目標物.transform;
                    if (目標 != null)
                    {
                        敵人 = new GameObject[] { 目標.gameObject };
                        傳目標給角色();
                        最短距離 = 50f;
                        //return;
                    }
                }
            }
        }

        Transform 最接近敵人 = null;

        foreach (GameObject e in 敵人)
        {
            目前距離 = Vector3.Distance(transform.position, e.transform.position);

            if (目前距離 < 最短距離)
            {
                最短距離 = 目前距離;
                最接近敵人 = e.transform;
            }
        }

        if (最接近敵人 != null)
        {
            目標 = 最接近敵人;
            傳目標給角色();

            dir = transform.position - 目標.position;
            發射點RAY = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
            Debug.DrawRay(發射點RAY, -dir, Color.red);

            if (Vector3.Distance(transform.position, 目標.position) < 射程範圍)
            {
                開火 = true;

                if (dir != Vector3.zero)
                {
                    旋轉 = Quaternion.LookRotation(-dir, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, 旋轉, 20 * Time.deltaTime);
                    transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
                }
            }
            else
            {
                開火 = false;
            }
        }
        else
        {
            開火 = false;
            最短距離 = 50f;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        string 武器標籤 = 是攻擊方 ? "防守方武器" : "攻擊方武器";

        if (other.CompareTag(武器標籤))
        {
            Destroy(other.gameObject);
            hp--;
            float 血量比例 = (float)hp / OrigHP;
            血條.transform.localScale = new Vector3(血量比例, 1f, 1f);
            血量文字.text = hp.ToString();

            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void 停止導航並播放勝利動畫()
    {
        if (導航 != null)
        {
            導航.SetDestination(this.transform.position);
            導航.isStopped = true;
        }
        anim.SetTrigger("WIN");
    }
}
