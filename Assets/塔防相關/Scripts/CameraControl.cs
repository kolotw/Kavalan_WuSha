using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float zoomSpeed = 2f;  // 縮放速度
    public float minZoom = 2.5f;  // 最小縮放值
    public float maxZoom = 6f;    // 最大縮放值

    public float dragSpeed = 0.0001f; // 拖曳速度

    // 移動邊界
    public float minX = -8f;
    public float maxX = -1f;
    public float minY = 2f;
    public float maxY = 10f;
    public float minZ = -1.8f;
    public float maxZ = 15f;

    private Vector3 dragOrigin;

    void FixedUpdate()
    {
        // 滾輪縮放
        float scrollData = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize -= scrollData * zoomSpeed;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);

        // 滑鼠拖曳移動
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = (Input.mousePosition - dragOrigin) * dragSpeed * Time.deltaTime;

            // 在相機的本地座標系統中移動 x、y 和 z 軸
            Vector3 move = new Vector3(-difference.x, -difference.y, 0);
            Camera.main.transform.Translate(move, Space.Self); // 使用 Space.Self 以根據相機的當前方向移動

            // 限制相機在邊界範圍內
            Vector3 clampedPosition = Camera.main.transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
            clampedPosition.z = Mathf.Clamp(clampedPosition.z, minZ, maxZ);
            Camera.main.transform.position = clampedPosition;

            dragOrigin = Input.mousePosition;
        }
    }
}
