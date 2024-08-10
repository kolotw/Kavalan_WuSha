using UnityEngine;

public class 移動目標 : MonoBehaviour
{
    public float speed = 2.0f; // 移動速度
    public float distance = 5.0f; // 移動距離

    private Vector3 startPosition;

    void Start()
    {
        // 紀錄物件的初始位置
        startPosition = transform.position;
    }

    void Update()
    {
        // 計算新的X座標
        float newX = startPosition.x + Mathf.PingPong(Time.time * speed, distance) - distance / 2;

        // 更新物件位置
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
