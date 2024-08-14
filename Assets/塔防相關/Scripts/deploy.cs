using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deploy : MonoBehaviour
{
    GameObject[]  = new GameObject[2];
    GameObject ‵;
    Vector3 newPos;

    // Start is called before the first frame update
    void Start()
    {
        [0] = GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().[0];
        [1] = GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().[1];
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
                    if (GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().A > 0)
                    {//场竝 A 菲公オ 
                        newPos = hit.transform.position;
                        newPos.y += 0.15f;

                        if (hit.transform.name == "‵毕臔ネΘ翴(场竝)")
                        {
                            Instantiate(‵, newPos, Quaternion.identity);
                        }
                        else 
                        {
                            Instantiate([0], newPos, Quaternion.identity);
                        }

                        
                        GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().A--;
                    }                    
                }
                if (Input.GetKeyUp(KeyCode.Mouse1))
                {
                    if (GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().B > 0)
                    {
                        //场竝 B 菲公
                        newPos = hit.transform.position;
                        newPos.y += 0.15f;
                        Instantiate([1], newPos, Quaternion.identity);
                        GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().B--;
                    }                    
                }
            }

        
        }
    }
}
