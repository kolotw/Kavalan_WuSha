using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EBookController2 : MonoBehaviour
{
    public Text text1;
    public TextAsset txt;
    public int Row;
    int textstr = 0;
    
    void Start()
    {
        string[] str = txt.text.Split('\n');
        for (int i = 0; i < Row; i++)
        {
            text1.text += str[textstr] + '\n';
            textstr++;
        }
    }
    void Update()
    {
        
    }
}
