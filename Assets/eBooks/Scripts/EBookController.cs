using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EBookController : MonoBehaviour
{
    public TextAsset txt;
    public int Row;
    int textnum = 1;
    void Start()
    {
        int Pagenum = Row / 18;
        int Pagelest = Row % 18;
        int textstr = 0;
        string[] str = txt.text.Split('\n');
        /*
        string filla = txt.text;
        Debug.Log(filla);
        AAA.text = filla;
        */
        for (int a = 0; a <= Pagenum; a++)
        {
            int Pagecount = 18;
            if (Pagenum  == a) 
            {
                Pagecount = Pagelest;
            }
            for (int i = 0; i < Pagecount; i++)
            {
                GameObject.Find("Text (" +textnum + ")").GetComponent<Text>().text += str[textstr] + '\n';
                textstr++;
            }
            textnum++;
        }
   
    }
    void Update()
    {

    }
}
