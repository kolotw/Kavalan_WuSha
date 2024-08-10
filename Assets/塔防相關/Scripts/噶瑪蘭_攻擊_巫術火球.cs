using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class 噶瑪蘭_攻擊_巫術火球 : MonoBehaviour
{
    Animator anim;
    public GameObject 火球源;
    public Transform 發射點;
    GameObject 火球;

    //public Rig rig;
    bool 顯示火球 = false;
    public Transform 目標;
    public float 射速 = 10f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (顯示火球)
        {
            火球.transform.position = 發射點.transform.position;
            if (目標 == null) return;
            Quaternion targetRotation = Quaternion.LookRotation(目標.transform.position - transform.position);
            targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }        
    }
    public void 產生火球()
    {
        //print("產生");
        火球 = Instantiate(火球源, 發射點.position,Quaternion.identity);
        Destroy(火球, 2f);
        顯示火球 = true;
    }
    public void 發射火球()
    {
        //print("發射");
        顯示火球 = false;
        if (目標 == null) return;
        Transform 瞄準目標 = 目標.transform.Find("被瞄準的位置");
        Vector3 dir = 瞄準目標.transform.position - 火球.transform.position;
        火球.GetComponent<Rigidbody>().velocity = dir * 射速;
    }
}
