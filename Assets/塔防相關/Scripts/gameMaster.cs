using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameMaster : MonoBehaviour
{
    //------------- ���� -------------------
    public GameObject[] �u�� = new GameObject[2];
    public GameObject[] ��� = new GameObject[2];

    //------------- �귽�W�� -------------------
    public int ��A�W�� = 5;
    public int ��B�W�� = 5;

    //------------- �d�F���@�ͦ��I -------------------
    public GameObject �d�F;


    //------------- ��� -------------------
    public int �ثe���d = 1;
    public float �C�X���ͤ@�i = 10f;
    public float �C�����ͼĤH�����j = 1f;
    public int �C���ͦ��ĤH�� = 3;
    public int ��դW�� = 1;
    public int �ثe�ĴX�i��� = 0;
    float �����˼�;

    //------------- ���ͼĤH -------------------
    GameObject[] �ͦ��I;
    int �ĴX�ӥͦ��I;
    GameObject �w�ͦ��ĤH;

    //------------- UI TEXT -------------------
    public Text ��դ�r; // �ĴX���A�ĴX�i�A�˼ơK �ӱѰT��
    public Text �귽��r;

    bool EndGame = false;
    bool �w���� = false;

    public GameObject �}�l�e��;
    public bool isWin = false;
    // Start is called before the first frame update
    void Start()
    {
        �ͦ��I = GameObject.FindGameObjectsWithTag("�ͦ��I");
        �����˼� = �C�X���ͤ@�i;
        
    }
    void ����() {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("������");
        foreach (GameObject t in turrets)
        {
            if (t.GetComponent<Animator>() != null)
            {
                t.transform.Find("RigHead/HeadAim").gameObject.SetActive(false);
                //t.GetComponent<Animator>().SetBool("WIN", true);
            }
            t.GetComponent<������_�ݩʳ]�w>().enabled = false;
        }
        Camera.main.gameObject.GetComponent<deploy>().enabled = false;
        �w���� = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (�}�l�e��.activeInHierarchy == true) return;
        if (isWin) return;

        if (EndGame) {
            if(!�w����)
                ����();
            return;
        }
       if (GameObject.Find("target") == null)
        {
            ��դ�r.text = "�A��F";
            EndGame = true;
        }
        else
        {
            ��դ�r.text = "��" + �ثe���d.ToString() + "�� ��" + �ثe�ĴX�i���.ToString()
               + "�i��� �٦�" + Mathf.RoundToInt(�����˼�).ToString() + "��";
            �귽��r.text = "�귽�G\n���xA�G" + ��A�W��.ToString() + "\n���xB�G" + ��B�W��.ToString();

            if (�ثe�ĴX�i��� <= ��դW��)
            {
                �����˼� -= Time.deltaTime;
                if (�����˼� < 0)
                {
                    StartCoroutine(�@�i�ĤH());
                    �����˼� = �C�X���ͤ@�i;
                }
            }
            else
            {
                �����˼� = 0;
                if (GameObject.FindGameObjectsWithTag("������").Length == 0)
                {
                    ��դ�r.text = "�AĹ�F";
                    isWin = true;
                    EndGame = true;
                }
            }
        }
    }
    void �ͦ��ĤH()
    {
        int e = Random.Range(0, ���.Length);
        �w�ͦ��ĤH = Instantiate(���[e], �ͦ��I[�ĴX�ӥͦ��I].transform.position, Quaternion.identity);
        �w�ͦ��ĤH.tag = "������";
    }

    IEnumerator �@�i�ĤH()
    {
        �ثe�ĴX�i���++;
        �ĴX�ӥͦ��I = Random.Range(0, �ͦ��I.Length);
        
        for (int i = 0; i < �C���ͦ��ĤH��; i++)
        {
            �ͦ��ĤH();
            yield return new WaitForSeconds(�C�����ͼĤH�����j);
        }
    }

    public void ���U�}�l()
    {
        �}�l�e��.SetActive(false);
        StartCoroutine(�@�i�ĤH());
    }
}
