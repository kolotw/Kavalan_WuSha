using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class 噶瑪蘭射箭 : MonoBehaviour
{
    Animator anim;
    public GameObject 左手;
    public GameObject 右手;
    public Transform 目標;
    public Rig rig;
    
    public GameObject 箭;
    public Transform 箭的位置;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rig.weight = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void 左手瞄準()
    {

    }
    public void 拿出箭()
    {

    }
    public void 拉弓()
    {

    }
    public void 射箭()
    {

    }
}
