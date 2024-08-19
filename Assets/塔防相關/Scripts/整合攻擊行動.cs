using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.VFX;

public class 整合攻擊行動 : MonoBehaviour
{
    Animator anim;
    public bool 是攻擊方 = false;
    public GameObject 發射武器;
    public Transform 發射點;
    public VisualEffect vfx;
    public Transform 目標;
    public float 射速 = 10f;
    GameObject 發射後的武器;
    bool 顯示武器 = false;
    public Rig rig;
    void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rig>();
        if(vfx!=null) vfx.Stop();
        //if (this.tag == "攻擊方") 是攻擊方 = true;
        //if (this.tag == "防守方") 是攻擊方 = false;
    }

    void Update()
    {
        if(rig ==  null) return;    
        if (顯示武器) 
        {
            rig.weight = 1;
            發射後的武器.transform.position = 發射點.transform.position;
            發射後的武器.transform.eulerAngles = 發射點.transform.eulerAngles;
        } 
        else
        {
            rig.weight = 0;
        }
    }

    // 通用的發射功能
    private void 發射武器到目標(float 速度)
    {
        if (目標 == null) return;
        Transform 瞄準目標 = 目標.transform.Find("被瞄準的位置");
        if (瞄準目標 == null) return;
        Vector3 dir = 瞄準目標.transform.position - 發射後的武器.transform.position;
        發射後的武器.GetComponent<Rigidbody>().velocity = dir.normalized * 速度;
        發射後的武器.transform.LookAt(目標);
    }

    //----------- 漢人砍劈 --------------
    public void 開始砍()
    {
        if (目標 != null)
        {
            發射後的武器 = Instantiate(發射武器, 發射點.position, Quaternion.identity);
            發射後的武器.tag = 是攻擊方 ? "攻擊方武器" : "防守方武器";
            Destroy(發射後的武器, 2f);
            發射武器到目標(射速);
            if (vfx != null) vfx.SendEvent("OnPlay");
        }
    }

    public void 砍完了()
    {
        // 這裡可以根據需要添加後續操作
    }

    //----------- 漢人射箭 --------------
    public void 開始射箭()
    {
        發射後的武器 = Instantiate(發射武器, 發射點.position, 發射點.rotation);
        發射後的武器.tag = 是攻擊方 ? "攻擊方武器" : "防守方武器";
        Destroy(發射後的武器, 2f);
    }

    public void 完成射箭()
    {
        發射武器到目標(射速);
    }

    //----------- 噶瑪蘭 火球 ----------
    public void 產生火球()
    {
        發射後的武器 = Instantiate(發射武器, 發射點.position, Quaternion.identity);
        發射後的武器.tag = 是攻擊方 ? "攻擊方武器" : "防守方武器";
        Destroy(發射後的武器, 2f);
    }

    public void 發射火球()
    {
        發射武器到目標(射速);
    }

    //---------------噶瑪蘭 射箭----------------------
    public void 拿出箭()
    {
        發射後的武器 = Instantiate(發射武器, 發射點.position, Quaternion.identity);
        發射後的武器.tag = 是攻擊方 ? "攻擊方武器" : "防守方武器";
        Destroy(發射後的武器, 2f);
        if (目標 != null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(目標.transform.position - transform.position);
            targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
        顯示武器 = true;
    }

    public void 射出箭()
    {
        發射武器到目標(射速);
        顯示武器 = false;
    }
}
