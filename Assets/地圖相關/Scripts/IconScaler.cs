using UnityEngine;

public class IconScaler : MonoBehaviour
{
    public Camera mainCamera; // 主攝像機
    public Transform[] 放大會顯示; // 正向透明度 icon 的 Transform
    public Transform[] 放大會消失; // 反向透明度 icon 的 Transform

    private float initialSize; // 初始 Camera orthographic size
    private Vector3[] initialScalesForward; // 正向透明度 icon 初始 scale
    private Vector3[] initialScalesInverse; // 反向透明度 icon 初始 scale

    //--- 滾輪縮放 ---
    public float zoomSpeed = 1.5f; // 縮放速度
    public float minZoom = 1f; // 最小縮放
    public float maxZoom = 10f; // 最大縮放

    //--- 滑鼠拖曳 移動地圖 ---
    private Vector3 dragOrigin; // 拖曳起始點
    private bool isDragging = false; // 是否正在拖曳

    // 地圖邊界
    public float minX = -4.2f;
    public float maxX = 7f;
    public float minY = -2.5f;
    public float maxY = 9f;

    void Start()
    {
        mainCamera = Camera.main;
        initialSize = mainCamera.orthographicSize;

        // 記錄每個 icon 的初始 scale
        initialScalesForward = new Vector3[放大會顯示.Length];
        initialScalesInverse = new Vector3[放大會消失.Length];

        for (int i = 0; i < 放大會顯示.Length; i++)
        {
            initialScalesForward[i] = 放大會顯示[i].localScale;
        }

        for (int i = 0; i < 放大會消失.Length; i++)
        {
            initialScalesInverse[i] = 放大會消失[i].localScale;
        }
    }

    void Update()
    {
        HandleMouseDrag();
        HandleZoom();
        UpdateIconScaleAndTransparency();
    }

    void HandleMouseDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 origin = Input.mousePosition;
            origin.z = -mainCamera.transform.position.z; // 确保 z 坐标正确
            dragOrigin = mainCamera.ScreenToWorldPoint(origin);
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            currentMousePosition.z = -mainCamera.transform.position.z; // 确保 z 坐标正确
            Vector3 currentWorldPosition = mainCamera.ScreenToWorldPoint(currentMousePosition);
            Vector3 difference = dragOrigin - currentWorldPosition;
            Vector3 newPosition = mainCamera.transform.position + difference;

            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

            mainCamera.transform.position = newPosition;
        }
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float newSize = mainCamera.orthographicSize - scroll * zoomSpeed;
        newSize = Mathf.Clamp(newSize, minZoom, maxZoom);
        mainCamera.orthographicSize = newSize;
    }

    void UpdateIconScaleAndTransparency()
    {
        float scaleRatio = mainCamera.orthographicSize / initialSize;

        // 更新正向透明度 icon
        UpdateIconGroup(放大會顯示, initialScalesForward, scaleRatio, false);

        // 更新反向透明度 icon
        UpdateIconGroup(放大會消失, initialScalesInverse, scaleRatio, true);
    }

    void UpdateIconGroup(Transform[] iconGroup, Vector3[] initialScales, float scaleRatio, bool isInverse)
    {
        for (int i = 0; i < iconGroup.Length; i++)
        {
            float clampedScale = Mathf.Clamp(initialScales[i].x * scaleRatio, 0.6f, 1f);
            iconGroup[i].localScale = new Vector3(clampedScale, clampedScale, clampedScale);

            float t = (mainCamera.orthographicSize - minZoom) / (maxZoom - minZoom);
            float alpha = Mathf.Lerp(1f, 0f, t);

            if (isInverse)
            {
                alpha = 1f - alpha;
            }

            SpriteRenderer spriteRenderer = iconGroup[i].GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a = alpha;
                spriteRenderer.color = color;
            }
        }
    }
}
