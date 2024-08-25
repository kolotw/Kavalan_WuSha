using UnityEngine;

public class IconScaler : MonoBehaviour
{
    public Camera mainCamera; // 主攝像機
    public Transform[] iconTransforms; // icon 的 Transform
    public bool[] isInverseTransparency; // 是否是反向透明度

    private float initialSize; // 初始 Camera orthographic size
    private Vector3[] initialScales; // 初始 icon scale

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
        initialScales = new Vector3[iconTransforms.Length];
        for (int i = 0; i < iconTransforms.Length; i++)
        {
            initialScales[i] = iconTransforms[i].localScale;
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
        // 如果按下滑鼠左鍵
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }

        // 如果放開滑鼠左鍵
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // 如果正在拖曳
        if (isDragging)
        {
            Vector3 currentMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 difference = dragOrigin - currentMousePosition;
            Vector3 newPosition = mainCamera.transform.position + difference;

            // 限制相機的移動範圍
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

            mainCamera.transform.position = newPosition;
        }
    }

    void HandleZoom()
    {
        // 獲取滾輪輸入
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // 計算新的 orthographicSize
        float newSize = mainCamera.orthographicSize - scroll * zoomSpeed;

        // 限制新的 orthographicSize 在 minZoom 和 maxZoom 範圍內
        newSize = Mathf.Clamp(newSize, minZoom, maxZoom);

        // 設置新的 orthographicSize
        mainCamera.orthographicSize = newSize;
    }

    void UpdateIconScaleAndTransparency()
    {
        // 計算比例
        float scaleRatio = mainCamera.orthographicSize / initialSize;

        for (int i = 0; i < iconTransforms.Length; i++)
        {
            // 根據比例縮放 icon，並限制縮放範圍
            float clampedScale = Mathf.Clamp(initialScales[i].x * scaleRatio, 0.6f, 1f);
            iconTransforms[i].localScale = new Vector3(clampedScale, clampedScale, clampedScale);

            // 計算透明度
            float t = (mainCamera.orthographicSize - minZoom) / (maxZoom - minZoom);
            float alpha = Mathf.Lerp(1f, 0f, t);

            // 根據 isInverseTransparency 標誌設置透明度
            if (isInverseTransparency[i])
            {
                alpha = 1f - alpha;
            }

            // 設置 icon 的透明度
            SpriteRenderer spriteRenderer = iconTransforms[i].GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a = alpha;
                spriteRenderer.color = color;
            }
        }
    }
}
