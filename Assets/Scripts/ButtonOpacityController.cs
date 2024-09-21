using UnityEngine;
using UnityEngine.UI;

public class ButtonTransparency : MonoBehaviour
{
    public Button button; // 拖入你的按鈕
    private Image buttonImage;
    private float transparency = 1.0f; // 默認完全不透明
    public float transparencyChangeSpeed = 0.1f; // 控制透明度改變的速度

    void Start()
    {
        buttonImage = button.GetComponent<Image>();
    }

    void Update()
    {
        // 使用滑鼠滾輪進行透明度變化
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            transparency -= scroll * transparencyChangeSpeed*2;
            transparency = Mathf.Clamp(transparency, 0.5f, 1f); // 限制透明度範圍在0到1之間

            // 更改按鈕的透明度（alpha）
            Color color = buttonImage.color;
            color.a = transparency;
            buttonImage.color = color;
        }
    }
}
