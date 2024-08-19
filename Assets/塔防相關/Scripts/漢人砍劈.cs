using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class 漢人砍劈 : MonoBehaviour
{
    Animator anim;
    public GameObject 劍氣;
    public Transform 發射點;
    public VisualEffect vfx;
    public Transform 目標;
    public bool 是攻擊方 = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //vfx = GetComponent<VisualEffect>();
        vfx.Stop();
        //目標 = transform.Find("target");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            anim.SetTrigger("FIRE");
        }
    }
    public void 開始砍()
    {
        if (目標 != null)
        {
            GameObject go = Instantiate(劍氣, 發射點.position, Quaternion.identity);
            Destroy(go, 2f);
            go.transform.LookAt(目標.transform.position);
            Vector3 dir = 目標.transform.position - go.transform.position;
            go.GetComponent<Rigidbody>().velocity = dir * 10f;
            vfx.SendEvent("OnPlay");
        }
    }

    public void 砍完了()
    {

    }
}
