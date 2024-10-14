using UnityEngine;
using UnityEngine.UIElements;

public class GUI控制器 : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button button1;
    private Button button2;
    private Button button3;
    public Label myLabel;
    public Label selected;
    public Label leftA;
    public Label leftB;
    public Label leftC;
    //start panel
    private Button b_Back;
    private Button b_Go;
    private VisualElement ve_Start;
    private VisualElement ve_Result;
    private Button res_b_Back;
    private Button res_b_Go;
    private Label resultLabel;

    void Start()
    {
        // 獲取 UIDocument 元件
        uiDocument = GetComponent<UIDocument>();

        // 獲取 UI 根元素
        var root = uiDocument.rootVisualElement;
        ve_Start = root.Q<VisualElement>("VE_StartPanel"); //關卡說明面板
        ve_Result = root.Q<VisualElement>("VE_Result");

        // 查找 Button 和 Label 元件
        button1 = root.Q<Button>("Button_KavalanFemale1"); // 假設第一個按鈕名稱為 "button1"
        button2 = root.Q<Button>("Button_KavalanFemale2"); // 假設第二個按鈕名稱為 "button2"
        button3 = root.Q<Button>("Button_KavalanMale1"); // 假設第三個按鈕名稱為 "button3"
        myLabel = root.Q<Label>("Label_Info");   // 假設 Label 的名稱為 "myLabel"
        
        selected = root.Q<Label>("Label_Char");  //選擇角色
        leftA = root.Q<Label>("Label_LeftA");
        leftB = root.Q<Label>("Label_LeftB");
        leftC = root.Q<Label>("Label_LeftC");

        b_Back = root.Q<Button>("btn_Back");
        b_Go = root.Q<Button>("btn_Go");

        res_b_Back = root.Q<Button>("Res_btn_Back");
        res_b_Go = root.Q<Button>("Res_btn_Go");
        resultLabel = root.Q<Label>("Label_Result");

        // 為每個按鈕添加點擊事件
        button1.clicked += OnButton1Clicked;
        button2.clicked += OnButton2Clicked;
        button3.clicked += OnButton3Clicked;
        b_Back.clicked += OnBtnBackClicked;
        b_Go.clicked += OnBtnGoClicked;

        res_b_Back.clicked += OnResBtnBackClicked;
        res_b_Go.clicked += OnResBtnGoClicked;

        // 設置 Label 初始文字
        myLabel.text = "請選擇一個按鈕";
        selected.text = "選擇角色";

        ve_Result.style.display = DisplayStyle.None;
    }

    // 第一個按鈕的點擊事件處理函數
    private void OnButton1Clicked()
    {
        //myLabel.text = "你點擊了按鈕 1";
        GameObject.Find("/GAMEMASTER").GetComponent<gameMaster>().角色 = 0;
    }

    // 第二個按鈕的點擊事件處理函數
    private void OnButton2Clicked()
    {
        //myLabel.text = "你點擊了按鈕 2";
        GameObject.Find("/GAMEMASTER").GetComponent<gameMaster>().角色 = 1;
    }

    // 第三個按鈕的點擊事件處理函數
    private void OnButton3Clicked()
    {
        //myLabel.text = "你點擊了按鈕 3";
        GameObject.Find("/GAMEMASTER").GetComponent<gameMaster>().角色 = 2;
    }
    private void OnBtnBackClicked()
    {
        //myLabel.text = "返回";
    }
    private void OnBtnGoClicked()
    {
        //myLabel.text = "繼續";
        ve_Start.style.display = DisplayStyle.None;
        GameObject.Find("/GAMEMASTER").GetComponent<gameMaster>().startGame = true;
    }
    private void OnResBtnBackClicked()
    {
        //myLabel.text = "返回";
    }
    private void OnResBtnGoClicked()
    {
        //myLabel.text = "返回";
    }
    public void result(bool isWin)
    {
        ve_Result.style.display = DisplayStyle.Flex;
        if(isWin)
        {
            resultLabel.text = "勝利";
        }
        else
        {
            resultLabel.text = "輸了";
        }
    }

}
