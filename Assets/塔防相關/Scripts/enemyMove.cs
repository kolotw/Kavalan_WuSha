using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class enemyMove : MonoBehaviour
{
    NavMeshAgent 代理人;
    public Transform 目標;
    GameObject[] 中繼點;

    public int hp = 10;

    public float 射程距離 = 2f;
    float 弓箭手距離 = 2f;
    public Transform 最近弓箭手;
    Animator 動畫;

    public Transform 發射點;
    public GameObject 子彈;
    public float 開火間距 = 0.3f;
    float fireTime;

    Text 血量;
    GameObject 血條;
    int OriHP;

    public Rig rig;

    // Start is called before the first frame update
    void Start()
    {
        血量 = transform.Find("Canvas/血量").gameObject.GetComponent<Text>();
        血量.text = hp.ToString();
        血條 = transform.Find("Canvas/血條").gameObject;
        OriHP = hp;
        rig.weight = 0;

        fireTime = 開火間距;
        動畫 = GetComponent<Animator>();
        代理人 = GetComponent<NavMeshAgent>();
        搜目標();

    }
    void 搜目標() {
        //如果 場景內 有「中繼點」
        //先去找 中繼點，刪除後，繼續找中繼點，直到沒有
        //如果，弓箭手比較近，那先攻擊弓箭手
        //再去找目標…
        
        中繼點 = GameObject.FindGameObjectsWithTag("中繼點");
        if (中繼點.Length == 0)
        {
            if(GameObject.Find("/target") != null)
            {
                目標 = GameObject.Find("/target").transform;
            }
            else
            {
                return;
            }
                
        }
        else
        {
            int r = Random.Range(0, 中繼點.Length - 1);
            目標 = 中繼點[r].transform;
        }
        代理人.SetDestination(目標.position);
    }

    bool 找弓箭手() {
        if (最近弓箭手 != null) {
            this.transform.LookAt(最近弓箭手); 
            return true; 
        }
        GameObject[] 弓箭手 = GameObject.FindGameObjectsWithTag("弓箭手");
        float dist;
        foreach (GameObject t in 弓箭手)
        {
            dist = Vector3.Distance(this.transform.position, t.transform.position);
            if (dist < 射程距離)
            {
                if (dist < 弓箭手距離)
                {
                    弓箭手距離 = dist;
                    最近弓箭手 = t.transform;
                    代理人.isStopped = true;
                    rig.weight = 1;
                    動畫.SetBool("FIRE", true);
                    this.transform.LookAt(最近弓箭手);
                    目標 = 最近弓箭手;
                    //return true;
                }
            }
        }
        if (最近弓箭手 != null)
        {
            return true;
        }
        else {
            代理人.isStopped = false;
            rig.weight = 0;
            動畫.SetBool("FIRE", false);
            return false;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        //cv.transform.LookAt(Camera.main.transform);
        if (GameObject.Find("/target") == null)
        {
            代理人.SetDestination(this.transform.position);
            代理人.isStopped = true;
            動畫.SetTrigger("WIN");
            return;
        }

        if (目標 == null)
        {
            搜目標();
        }

        if (!   找弓箭手())
        {
            動畫.SetBool("FIRE", false);
            if (Vector3.Distance(this.transform.position, 目標.position) < 0.4f)
            {
                //if (目標.name != "target")
                Destroy(目標.gameObject);
            }
        }
        else 
        {
            if (Vector3.Distance(this.transform.position, 目標.transform.position) > 射程距離) {
                目標 = null;
                最近弓箭手 = null;
                return;
            }
        }        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "我方子彈") 
        {
            Destroy(other.gameObject);
            hp--;
            血量.text = hp.ToString();
            float blood = (float)hp / (float)OriHP;
            血條.transform.localScale = new Vector3(blood, 1, 1);

            if (hp <= 0) { Destroy(this.gameObject); }
        }
    }
}
