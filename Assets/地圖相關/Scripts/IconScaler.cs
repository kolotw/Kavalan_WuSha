using UnityEngine;

public class IconScaler : MonoBehaviour
{
    public Camera mainCamera; // �D�ṳ��
    public Transform[] iconTransforms; // icon �� Transform
    public bool[] isInverseTransparency; // �O�_�O�ϦV�z����

    private float initialSize; // ��l Camera orthographic size
    private Vector3[] initialScales; // ��l icon scale

    //--- �u���Y�� ---
    public float zoomSpeed = 1.5f; // �Y��t��
    public float minZoom = 1f; // �̤p�Y��
    public float maxZoom = 10f; // �̤j�Y��

    //--- �ƹ��즲 ���ʦa�� ---
    private Vector3 dragOrigin; // �즲�_�l�I
    private bool isDragging = false; // �O�_���b�즲

    // �a�����
    public float minX = -4.2f;
    public float maxX = 7f;
    public float minY = -2.5f;
    public float maxY = 9f; 
    void Start()
    {
        mainCamera = Camera.main;
        initialSize = mainCamera.orthographicSize;

        // �O���C�� icon ����l scale
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
        // �p�G���U�ƹ�����
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }

        // �p�G��}�ƹ�����
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // �p�G���b�즲
        if (isDragging)
        {
            Vector3 currentMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 difference = dragOrigin - currentMousePosition;
            Vector3 newPosition = mainCamera.transform.position + difference;

            // ����۾������ʽd��
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

            mainCamera.transform.position = newPosition;
        }
    }

    void HandleZoom()
    {
        // ����u����J
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // �p��s�� orthographicSize
        float newSize = mainCamera.orthographicSize - scroll * zoomSpeed;

        // ����s�� orthographicSize �b minZoom �M maxZoom �d��
        newSize = Mathf.Clamp(newSize, minZoom, maxZoom);

        // �]�m�s�� orthographicSize
        mainCamera.orthographicSize = newSize;
    }

    void UpdateIconScaleAndTransparency()
    {
        // �p����
        float scaleRatio = mainCamera.orthographicSize / initialSize;

        for (int i = 0; i < iconTransforms.Length; i++)
        {
            // �ھڤ���Y�� icon�A�í����Y��d��
            float clampedScale = Mathf.Clamp(initialScales[i].x * scaleRatio, 0.6f, 1f);
            iconTransforms[i].localScale = new Vector3(clampedScale, clampedScale, clampedScale);

            // �p��z����
            float t = (mainCamera.orthographicSize - minZoom) / (maxZoom - minZoom);
            float alpha = Mathf.Lerp(1f, 0f, t);

            // �ھ� isInverseTransparency �лx�]�m�z����
            if (isInverseTransparency[i])
            {
                alpha = 1f - alpha;
            }

            // �]�m icon ���z����
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
