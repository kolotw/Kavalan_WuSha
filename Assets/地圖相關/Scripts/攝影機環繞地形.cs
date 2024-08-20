using UnityEngine;

public class CameraAutoOrbit : MonoBehaviour
{
    public Transform target;  // 攝影機圍繞旋轉的目標物體
    public float rotationSpeed = 10.0f;  // 攝影機旋轉速度
    public Vector3 initialPosition = new Vector3(2468, 659, 322);  // 攝影機初始位置
    public Vector3 initialRotation = new Vector3(18.2536526f, 299.564606f, 3.59605065e-06f);  // 攝影機初始旋轉角度

    private Vector3 offset;  // 初始的位移向量

    void Start()
    {
        // 設置攝影機的初始位置和旋轉角度
        transform.position = initialPosition;
        transform.eulerAngles = initialRotation;

        // 計算攝影機與目標之間的初始位移
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        if (target)
        {
            // 計算旋轉的角度增量，並更新攝影機位置
            Quaternion rotation = Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
            offset = rotation * offset;
            transform.position = target.position + offset;

            // 重新設定攝影機的旋轉，使其面向目標物體
            transform.LookAt(target);
        }
    }
}
