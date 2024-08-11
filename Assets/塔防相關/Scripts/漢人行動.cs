using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class 漢人行動 : MonoBehaviour
{
    NavMeshAgent 導航;
    public Transform 目標;
    GameObject[] 中繼點;

    public int hp = 30;

    public float 射程距離 = 2f;
    public Transform 最近敵人;
    Animator 動畫;

    public float 開火間距 = 1.6f;
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
        rig.weight = 1;

        fireTime = 開火間距;
        動畫 = GetComponent<Animator>();
        導航 = GetComponent<NavMeshAgent>();
        搜目標();

    }
    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("/target") == null)
        {
            導航.SetDestination(this.transform.position);
            導航.isStopped = true;
            動畫.SetTrigger("WIN");
            return;
        }

        if (最近敵人!=null)
        {
           
            if (Vector3.Distance(this.transform.position, 目標.transform.position) < 射程距離)
            {
                動畫.SetBool("Run", false);
                導航.isStopped=true;
                this.transform.LookAt(最近敵人);
                this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);
                //開火間距
                if (Time.time > fireTime)
                {
                    
                    動畫.SetTrigger("FIRE");
                    fireTime = Time.time + 開火間距;
                }
            }
        }
        else
        {
            搜目標();
        }
        if((目標.tag == "中繼點")||(目標.name=="target"))
        {
            //動畫.SetBool("FIRE", false);
            if (Vector3.Distance(this.transform.position, 目標.position) < 0.4f)
            {
                Destroy(目標.gameObject);
            }

        }
    }
    void 搜目標() {
        //如果 場景內 有「中繼點」
        //先去找 中繼點，刪除後，繼續找中繼點，直到沒有
        //如果，敵人比較近，那先攻擊敵人
        //再去找目標…
        
        中繼點 = GameObject.FindGameObjectsWithTag("中繼點");
        if (中繼點.Length == 0)
        {
            //如果中繼點沒了，還是要先找敵人？
            if(找敵人()==0)
            {
                if (GameObject.Find("/target") != null)
                {
                    目標 = GameObject.Find("/target").transform;
                }
            }
        }
        else
        {
            int r = Random.Range(0, 中繼點.Length - 1);
            目標 = 中繼點[r].transform;
        }

        //這邊來找敵人吧！
        找敵人();

        導航.isStopped = false;
        導航.SetDestination(目標.position);
        動畫.SetBool("Run", true);
        傳目標給角色();
    }
    int 找敵人()
    {
        
        float 目標距離 = 0;
        if (目標 == null)
        {
            目標距離 = 5;
        }
        else
        {
            目標距離 = Vector3.Distance(this.transform.position, 目標.position);
        }

        GameObject[] 敵人 = GameObject.FindGameObjectsWithTag("噶瑪蘭");
        float dist;
        foreach (GameObject t in 敵人)
        {
            dist = Vector3.Distance(this.transform.position, t.transform.position);
            if (dist < 目標距離)
            {
                目標 = t.transform;
                最近敵人 = t.transform;
                
            }
        }

        return 敵人.Length;
    }
    void 傳目標給角色()
    {
        //將目標傳到射箭行為，讓角色可以看著目標。
        if (this.transform.name == "漢人_農夫_鋤頭(Clone)") 
        {
            GetComponent<漢人砍劈>().目標 = 目標;
        }
        else if(this.transform.name == "漢人-弓箭手 F1(Clone)") 
        {
            GetComponent<漢人射箭>().目標 = 目標;
        }

        
        transform.Find("Rig 1/HeadAim").gameObject.GetComponent<MultiAimConstraint>().data.sourceObjects
            = new WeightedTransformArray { new WeightedTransform(目標, 1) };
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "噶瑪蘭武器") 
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
