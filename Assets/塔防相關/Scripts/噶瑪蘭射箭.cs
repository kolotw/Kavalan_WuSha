using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class 噶瑪蘭射箭 : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rig.weight = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            anim.SetTrigger("FIRE");
            顯示箭 = true;
        }
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
    }
    public void 拉弓()
    {

    }
    public void 射出箭()
    {
        print("射出箭");
        顯示箭 = false;
        arrow.GetComponent<Rigidbody>().velocity = arrow.transform.up * 10f;
    }
}
