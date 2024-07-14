using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 移動陽光 : MonoBehaviour
{
    GameObject mySun;
    float sunX;
    Vector3 newAng;
    float dTime = 0.1f;
    float nTime;
    // Start is called before the first frame update
    void Start()
    {
        mySun = GameObject.Find("/Sun");
        sunX = 180;
        nTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (nTime + dTime > Time.time) 
        {
            sunX -= 0.1f;
            if(sunX < 0) sunX = 180;
            newAng = new Vector3(sunX,66.5f,0);
            mySun.transform.eulerAngles = newAng;
            nTime = Time.time;
        }
    }
}
