using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deploy : MonoBehaviour
{
    GameObject[]  = new GameObject[2];
    GameObject ‵;
    Vector3 newPos;
    GameObject 场竝;

    // Start is called before the first frame update
    void Start()
    {
        [0] = GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().よ[0];
        [1] = GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().よ[1];
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
                            场竝 = Instantiate(‵, newPos, Quaternion.identity);
                            场竝.tag = "ňよ";
                        }
                        else 
                        {
                            场竝 = Instantiate([0], newPos, Quaternion.identity);
                            场竝.tag = "ňよ";
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
                        场竝 = Instantiate([1], newPos, Quaternion.identity);
                        场竝.tag = "ňよ";
                        GameObject.Find("GAMEMASTER").GetComponent<gameMaster>().B--;
                    }                    
                }
            }

        
        }
    }
}
