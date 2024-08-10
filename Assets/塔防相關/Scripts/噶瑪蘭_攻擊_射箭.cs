using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class 噶瑪蘭_攻擊_射箭 : MonoBehaviour
{
    //瞄準相關
    Animator anim;
    public Transform 目標;
    public Rig rig;
    
    //產生物件
    public GameObject 箭;
    public Transform 箭的位置;
    GameObject arrow;
    
    //控制箭的位置
    bool 顯示箭 = false;
    public float 射速 = 10f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //rig.weight = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (顯示箭)
        {
            rig.weight += 0.2f;
            if (arrow != null)
            {
                arrow.transform.position = 箭的位置.position;
                arrow.transform.eulerAngles = 箭的位置.eulerAngles;
            }
        }
        else
        {
            rig.weight -= 0.1f;
        }
    }
    
    public void 左手瞄準()
    {

    }
    public void 拿出箭()
    {
        arrow = Instantiate(箭,箭的位置.position,Quaternion.identity);
        Destroy(arrow, 2f);
        顯示箭 = true;
        if (目標 == null) return;
        Quaternion targetRotation = Quaternion.LookRotation(目標.transform.position - transform.position);
        targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    }
    public void 拉弓()
    {

    }
    public void 射出箭()
    {
        //print("射出箭");
        顯示箭 = false;
        if (目標 == null) return;
        Transform 瞄準目標 = 目標.transform.Find("被瞄準的位置");
        Vector3 dir = 瞄準目標.transform.position - arrow.transform.position;
        //dir.y = 0.03f;
        arrow.GetComponent<Rigidbody>().velocity = dir * 射速;        
    }
}
