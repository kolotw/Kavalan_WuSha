using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class 噶瑪蘭巫術火球 : MonoBehaviour
{
    Animator anim;
    public GameObject 火球源;
    public Transform 發射點;
    GameObject 火球;

    //public Rig rig;
    bool 顯示火球 = false;
    public Transform 目標;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //rig.weight = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            anim.SetTrigger("FIRE");
        }
        if (顯示火球)
        {
            火球.transform.position = 發射點.transform.position;
            //transform.LookAt(目標.transform.position);
            //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            Quaternion targetRotation = Quaternion.LookRotation(目標.transform.position - transform.position);
            targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

        }
        else
        {

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
        Vector3 dir = 目標.transform.position - 火球.transform.position;
        火球.GetComponent<Rigidbody>().velocity = dir * 10f;
    }
}
