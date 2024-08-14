using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/*
 這邊主要是要呼應主線故事：吳沙有醫術會救原住民
 所以至少登場的有2種人
 - 被救者：一開始中毒倒地的人 (tag: waypoint)。
    一開始路倒，被救活後就回到生成點。
 - 救援者：吳沙，不攻擊，只救人(醫務兵的概念)，有血量，會被殺。

 ■遊戲如何進行？塔防？
 
 */
public class 被救的人的行為 : MonoBehaviour
{

    Animator anim;
    NavMeshAgent agent;
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("/target");
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(this.transform.position, target.transform.position);
        if(dist < 1)
            Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "病毒武器")
        {
            anim.SetTrigger("DIE");
            this.tag = "被救援者";
            Destroy(other.gameObject);
        }
    }
    public void 跑回家() 
    {
        anim.SetTrigger("BACKHOME");
        Transform home = GameObject.Find("/target").transform;
        agent.SetDestination(home.transform.position);
        this.tag = "漢人";
    }
}
