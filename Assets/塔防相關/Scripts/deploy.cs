using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deploy : MonoBehaviour
{
    GameObject[] ���x = new GameObject[2];
    GameObject �d�F;
    Vector3 newPos;
    GameObject �w���p;

    // Start is called before the first frame update
    void Start()
    {
        ���x[0] = GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().�u��[0];
        ���x[1] = GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().�u��[1];
        �d�F = GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().�d�F;
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
            if (hit.transform.tag == "�i���p")
            {
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    if (GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().��A�W�� > 0)
                    {//���p ���xA �ƹ��� 
                        newPos = hit.transform.position;
                        newPos.y += 0.15f;

                        if (hit.transform.name == "�d�F���@�ͦ��I(�i���p)")
                        {
                            �w���p = Instantiate(�d�F, newPos, Quaternion.identity);
                            �w���p.tag = "���u��";
                        }
                        else 
                        {
                            �w���p = Instantiate(���x[0], newPos, Quaternion.identity);
                            �w���p.tag = "���u��";
                        }

                        
                        GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().��A�W��--;
                    }                    
                }
                if (Input.GetKeyUp(KeyCode.Mouse1))
                {
                    if (GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().��B�W�� > 0)
                    {
                        //���p ���xB �ƹ��k
                        newPos = hit.transform.position;
                        newPos.y += 0.15f;
                        �w���p = Instantiate(���x[1], newPos, Quaternion.identity);
                        �w���p.tag = "���u��";
                        GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().��B�W��--;
                    }                    
                }
            }

        
        }
    }
}
