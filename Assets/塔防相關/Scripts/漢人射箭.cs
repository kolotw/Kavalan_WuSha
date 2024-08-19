using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 漢人射箭 : MonoBehaviour
{
    Animator anim;

    public GameObject 我的箭;
    public Transform 發射點;
    GameObject 射出的箭;
    public Transform 目標;
    public float 射速 = 10f;
    public bool 是攻擊方 = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void shootStart()
    {
        射出的箭 = Instantiate(我的箭, 發射點.position, 發射點.rotation);
        
        Destroy(射出的箭, 2f);
    }
    public void shootComplete()
    {
        if (目標 == null) return;
        Transform 瞄準目標 = 目標.transform.Find("被瞄準的位置");
        transform.Find("目標").position = 瞄準目標.transform.position;
        Vector3 dir = 瞄準目標.transform.position - 射出的箭.transform.position;
        射出的箭.GetComponent<Rigidbody>().velocity = dir * 射速;
        射出的箭.transform.LookAt(目標);

    }
}
