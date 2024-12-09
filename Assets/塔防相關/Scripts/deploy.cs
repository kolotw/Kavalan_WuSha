using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deploy : MonoBehaviour
{
    GameObject[] 砲台 = new GameObject[3];
    GameObject 吳沙;
    Vector3 newPos;
    GameObject 已部署;
    //int 角色 = 0;
    //int 剩餘數 = 0;

    // Start is called before the first frame update
    void Start()
    {
        switch (GameObject.Find("/GAMEMASTER").GetComponent<gameMaster>().守方.Length)
        {
            case 1:
                砲台[0] = GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().守方[0];
                break;
            case 3:
                砲台[0] = GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().守方[0];
                砲台[1] = GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().守方[1];
                砲台[2] = GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().守方[2];
                break;
            default:
                break;
        }
        
        吳沙 = GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().吳沙;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(
            Camera.main.ScreenToWorldPoint(Input.mousePosition),
            transform.TransformDirection(Vector3.forward),
            out hit,
            Mathf.Infinity
            ))
        {
            if (hit.transform.tag == "可部署")
            {
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    gameMaster gm = GameObject.Find("GAMEMASTER").GetComponent<gameMaster>();
                    int 角色 = gm.角色;
                    int 剩餘數 = Get剩餘數(角色, gm);

                    if (剩餘數 > 0)
                    {
                        // 部署砲台
                        Vector3 newPos = hit.transform.position;
                        newPos.y += 0.15f;

                        GameObject 已部署;

                        if (hit.transform.name == "吳沙救護生成點(可部署)")
                        {
                            已部署 = Instantiate(吳沙, newPos, Quaternion.identity);
                        }
                        else
                        {
                            已部署 = Instantiate(砲台[角色], newPos, Quaternion.identity);
                        }

                        已部署.tag = "防守方";

                        // 更新剩餘數量
                        Update剩餘數(角色, gm);
                    }
                }
            }
        }


    }


    // 抽取剩餘數邏輯，減少重複代碼
    int Get剩餘數(int 角色, gameMaster gm)
    {
        switch (角色)
        {
            case 0:
                return gm.砲A上限;
            case 1:
                return gm.砲B上限;
            case 2:
                return gm.砲C上限;
            default:
                return 0;
        }
    }

    // 抽取更新剩餘數的邏輯，減少重複代碼
    void Update剩餘數(int 角色, gameMaster gm)
    {
        switch (角色)
        {
            case 0:
                gm.砲A上限--;
                break;
            case 1:
                gm.砲B上限--;
                break;
            case 2:
                gm.砲C上限--;
                break;
            default:
                break;
        }

    }
}
