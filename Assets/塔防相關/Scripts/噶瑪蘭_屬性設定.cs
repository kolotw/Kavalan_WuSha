using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class 噶瑪蘭_屬性設定 : MonoBehaviour
{
    GameObject[] 敵人;
    public float 射程範圍 = 4f;
    float 目前距離;
    float 最短距離 = 2f;
    Transform 目標;
    Vector3 dir;
    Vector3 發射點RAY;

    Quaternion 旋轉;

    bool 開火 = false;
    public float 火力間隔 = 2f;
    float fireTime;

    public int hp = 10;
    int OrigHP;
    GameObject 血條;
    Text 血量文字;

    //弓箭手
    Animator anim;
    public Rig rig;

    // Start is called before the first frame update
    void Start()
    {
        OrigHP = hp;
        fireTime = Time.time;
        血條 = transform.Find("Canvas/血條").gameObject;
        血量文字 = transform.Find("Canvas/血量").gameObject.GetComponent<Text>();
        血量文字.text = hp.ToString();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        找敵人();
        if (敵人.Length == 0) return;
        if (開火) {
            if (Time.time > fireTime + 火力間隔)
            {
                rig.weight = 1;
                anim.SetTrigger("FIRE");
                fireTime = Time.time;
            }
        }
    }
    void 傳目標給角色()
    {
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
    }
    void 找敵人() 
    {
        敵人 = GameObject.FindGameObjectsWithTag("漢人");
        if (敵人.Length==0)
        {
            rig.weight = 0;
            anim.SetTrigger("WIN");
            return;
        }
        最短距離 = 射程範圍;
        foreach (GameObject e in 敵人)
        {
            
            目前距離 = Vector3.Distance(this.transform.position, e.transform.position);
            if (目前距離 < 射程範圍)
            {
                if (目前距離 < 最短距離)
                {
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
            else {
                開火 = false;
            }
        }
        else {
            開火 = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "漢人武器")
        {
            Destroy(other.gameObject);
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
}
