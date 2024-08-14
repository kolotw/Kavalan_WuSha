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

    GameObject[] 敵人;
    GameObject[] 中繼點;
    public float 射程範圍 = 4f;
    float 目前距離;
    float 最短距離 = 2f;
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
        
        if(攻方或守方.ToString() != "守方")  // 攻方、救援者(也算是攻方)、被救援者(回家) 都會移動，只有守方不會移動
        { 
            //攻方
            導航 = GetComponent<NavMeshAgent>();
            搜目標();
        }
        if(this.tag == "漢人")
        {
            rig.weight = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(攻方或守方)
        {
            case OptionsA.攻方:
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
                    if (Vector3.Distance(this.transform.position, 目標.transform.position) < 射程範圍)
                    {
                        動畫.SetBool("Run", false);
                        導航.isStopped = true;
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
                if ((目標.tag == "中繼點") || (目標.name == "target"))
                {
                    //動畫.SetBool("FIRE", false);
                    if (Vector3.Distance(this.transform.position, 目標.position) < 0.4f)
                    {
                        Destroy(目標.gameObject);
                    }

                }
                break;            
            case OptionsA.守方:
                找敵人();
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
        if(this.tag == "病毒")
        {

            return;
        }
        switch (目標敵人)
        {
            case OptionsB.漢人:
                if (目標 == null) return;
                //將目標傳到射箭行為，讓角色可以看著目標。
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
                //將目標傳到射箭行為，讓角色可以看著目標。
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
    void 搜目標()
    {
        //如果 場景內 有「中繼點」
        //先去找 中繼點，刪除後，繼續找中繼點，直到沒有
        //如果，敵人比較近，那先攻擊敵人
        //再去找目標…
        中繼點 = GameObject.FindGameObjectsWithTag("中繼點");

        if (中繼點.Length == 0)
        {
            //如果中繼點沒了，還是要先找敵人？
            if (找敵人() == 0)
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
        if (目標 != null)
        {
            導航.SetDestination(目標.position);
            動畫.SetBool("Run", true);
            傳目標給角色();
        }
        
    }
    int 找敵人()
    {
        //敵人 = GameObject.FindGameObjectsWithTag(目標敵人.ToString()); //漢人
        GameObject[] 敵人 = GameObject.FindGameObjectsWithTag(目標敵人.ToString());
        switch (攻方或守方)
        {
            case OptionsA.攻方:
                
                if (敵人.Length == 0)
                {
                    if(rig!=null) rig.weight = 0;
                    //動畫.SetTrigger("WIN");
                    return 0;
                }
                最短距離 = 射程範圍;
                foreach (GameObject e in 敵人)
                {

                    目前距離 = Vector3.Distance(this.transform.position, e.transform.position);
                    if (目前距離 < 射程範圍)
                    {
                        if (目前距離 < 最短距離)
                        {
                            最近敵人 = e.transform;
                            最短距離 = 目前距離;
                            目標 = e.transform;
                            傳目標給角色();
                        }
                    }
                }
                if (目標 != null)
                {
                    dir = this.transform.position - 目標.position;
                    發射點RAY = new Vector3(
                        this.transform.position.x, this.transform.position.y + 0.1f, this.transform.position.z);
                    Debug.DrawRay(發射點RAY, dir * -1, Color.red);

                    if (Vector3.Distance(this.transform.position, 目標.position) < 射程範圍)
                    {
                        開火 = true;
                        if (dir != Vector3.zero)
                        {
                            旋轉 = Quaternion.LookRotation(dir * -1, Vector3.up);
                            transform.rotation = Quaternion.Slerp(transform.rotation, 旋轉, 20 * Time.deltaTime);
                            this.transform.eulerAngles = new Vector3(0f, this.transform.eulerAngles.y, 0f);
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
                }
                return 敵人.Length;
                //break;
            
            case OptionsA.守方:
                float 目標距離 = 0;
                if (目標 == null)
                {
                    目標距離 = 5;
                }
                else
                {
                    目標距離 = Vector3.Distance(this.transform.position, 目標.position);
                }

                
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
            //break;

            
            default:
                return -1;
                //break;
        }

        
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (目標敵人)
        {
            case OptionsB.漢人:
                if (other.tag == "漢人武器")
                {
                    減血(other.gameObject);
                }
                    break;
            case OptionsB.噶瑪蘭:
                if (other.tag == "噶瑪蘭武器")
                {
                    減血(other.gameObject);
                }
                    break;
            default:
                break;
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
