using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deploy : MonoBehaviour
{
    GameObject[] ���x = new GameObject[3];
    GameObject �d�F;
    Vector3 newPos;
    GameObject �w���p;
    //int ���� = 0;
    //int �Ѿl�� = 0;

    // Start is called before the first frame update
    void Start()
    {
        ���x[0] = GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().�u��[0];
        ���x[1] = GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().�u��[1];
        ���x[2] = GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().�u��[2];
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
                    gameMaster gm = GameObject.Find("GAMEMASTER").GetComponent<gameMaster>();
                    int ���� = gm.����;
                    int �Ѿl�� = Get�Ѿl��(����, gm);

                    if (�Ѿl�� > 0)
                    {
                        // ���p���x
                        Vector3 newPos = hit.transform.position;
                        newPos.y += 0.15f;

                        GameObject �w���p;

                        if (hit.transform.name == "�d�F���@�ͦ��I(�i���p)")
                        {
                            �w���p = Instantiate(�d�F, newPos, Quaternion.identity);
                        }
                        else
                        {
                            �w���p = Instantiate(���x[����], newPos, Quaternion.identity);
                        }

                        �w���p.tag = "���u��";

                        // ��s�Ѿl�ƶq
                        Update�Ѿl��(����, gm);
                    }
                }
            }
        }


    }


    // ����Ѿl���޿�A��֭��ƥN�X
    int Get�Ѿl��(int ����, gameMaster gm)
    {
        switch (����)
        {
            case 0:
                return gm.��A�W��;
            case 1:
                return gm.��B�W��;
            case 2:
                return gm.��C�W��;
            default:
                return 0;
        }
    }

    // �����s�Ѿl�ƪ��޿�A��֭��ƥN�X
    void Update�Ѿl��(int ����, gameMaster gm)
    {
        switch (����)
        {
            case 0:
                gm.��A�W��--;
                break;
            case 1:
                gm.��B�W��--;
                break;
            case 2:
                gm.��C�W��--;
                break;
            default:
                break;
        }

    }
}
