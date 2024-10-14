using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deploy : MonoBehaviour
{
    GameObject[]  = new GameObject[3];
    GameObject ‵;
    Vector3 newPos;
    GameObject 场竝;
    //int à︹ = 0;
    //int 逞緇计 = 0;

    // Start is called before the first frame update
    void Start()
    {
        [0] = GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().よ[0];
        [1] = GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().よ[1];
        [2] = GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().よ[2];
        ‵ = GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().‵;
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
            if (hit.transform.tag == "场竝")
            {
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    gameMaster gm = GameObject.Find("GAMEMASTER").GetComponent<gameMaster>();
                    int à︹ = gm.à︹;
                    int 逞緇计 = Get逞緇计(à︹, gm);

                    if (逞緇计 > 0)
                    {
                        // 场竝
                        Vector3 newPos = hit.transform.position;
                        newPos.y += 0.15f;

                        GameObject 场竝;

                        if (hit.transform.name == "‵毕臔ネΘ翴(场竝)")
                        {
                            场竝 = Instantiate(‵, newPos, Quaternion.identity);
                        }
                        else
                        {
                            场竝 = Instantiate([à︹], newPos, Quaternion.identity);
                        }

                        场竝.tag = "ňよ";

                        // 穝逞緇计秖
                        Update逞緇计(à︹, gm);
                    }
                }
            }
        }


    }


    // ┾逞緇计呸胯搭ぶ狡絏
    int Get逞緇计(int à︹, gameMaster gm)
    {
        switch (à︹)
        {
            case 0:
                return gm.A;
            case 1:
                return gm.B;
            case 2:
                return gm.C;
            default:
                return 0;
        }
    }

    // ┾穝逞緇计呸胯搭ぶ狡絏
    void Update逞緇计(int à︹, gameMaster gm)
    {
        switch (à︹)
        {
            case 0:
                gm.A--;
                break;
            case 1:
                gm.B--;
                break;
            case 2:
                gm.C--;
                break;
            default:
                break;
        }

    }
}
