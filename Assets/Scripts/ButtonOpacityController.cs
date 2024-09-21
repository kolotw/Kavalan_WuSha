using UnityEngine;
using UnityEngine.UI;

public class ButtonTransparency : MonoBehaviour
{
    public Button button; // ��J�A�����s
    private Image buttonImage;
    private float transparency = 1.0f; // �q�{�������z��
    public float transparencyChangeSpeed = 0.1f; // ����z���ק��ܪ��t��

    void Start()
    {
        buttonImage = button.GetComponent<Image>();
    }

    void Update()
    {
        // �ϥηƹ��u���i��z�����ܤ�
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            transparency -= scroll * transparencyChangeSpeed*2;
            transparency = Mathf.Clamp(transparency, 0.5f, 1f); // ����z���׽d��b0��1����

            // �����s���z���ס]alpha�^
            Color color = buttonImage.color;
            color.a = transparency;
            buttonImage.color = color;
        }
    }
}
