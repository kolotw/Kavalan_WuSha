using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class 漢人行為 : MonoBehaviour
{
    Animator anim;

    public GameObject 我的箭;
    public Transform 發射點;
    GameObject 射出的箭;
    public Transform 目標;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void shootStart()
    {
       GameObject 射出的箭 = Instantiate(我的箭, 發射點.position, 發射點.rotation);
        射出的箭.transform.LookAt(目標.transform.position);
    }
    public void shootComplete()
    {

    }
}
