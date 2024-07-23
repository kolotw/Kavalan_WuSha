using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Profiling;

public class 地圖材質控制器 : MonoBehaviour
{
    public Terrain terrain; // 拖曳你的 Terrain 物件到這裡
    public int[] newOrder; // 設定新的順序，例如 [2, 0, 1] 表示第三個 layer 移到第一個，第一個 layer 移到第二個，第二個 layer 移到第三個
    private int currentOrderIndex = 0; // 追蹤當前需要使用的順序索引
    private TerrainLayer[] originalLayers; // 保存原始圖層順序

    void Start()
    {
        if (newOrder == null || newOrder.Length == 0)
        {
            Debug.LogError("請設置 newOrder 順序！");
            return;
        }

        // 保存原始圖層順序
        originalLayers = terrain.terrainData.terrainLayers;

        // 初次更新將第一個圖層順序應用
        UpdateLayerOrder(newOrder);
    }

    void Update()
    {
        // 這裡可以設置一個定時器或其他條件來定期觸發更新
        if (Input.GetKeyDown(KeyCode.Space)) // 例如按下 U 鍵
        {
            // 更新 currentOrderIndex 並循環
            currentOrderIndex = (currentOrderIndex + 1) % newOrder.Length;
            MoveLayerToTop(newOrder[currentOrderIndex]);
        }
    }

    public void MoveLayerToTop(int layerIndex)
    {
        int layersCount = terrain.terrainData.terrainLayers.Length;

        if (layerIndex < 0 || layerIndex >= layersCount)
        {
            Debug.LogError("層索引超出範圍！");
            return;
        }

        // 將特定 layer 移到最上面，其他層依次後移
        int[] updatedOrder = new int[layersCount];
        updatedOrder[0] = layerIndex;
        int newIndex = 1;

        for (int i = 0; i < layersCount; i++)
        {
            if (i != layerIndex)
            {
                updatedOrder[newIndex] = i;
                newIndex++;
            }
        }

        UpdateLayerOrder(updatedOrder);
    }

    private void UpdateLayerOrder(int[] newOrder)
    {
        // 根據 newOrder 重排 layers
        TerrainLayer[] reorderedLayers = new TerrainLayer[originalLayers.Length];
        for (int i = 0; i < newOrder.Length; i++)
        {
            reorderedLayers[i] = originalLayers[newOrder[i]];
        }

        // 設定 Terrain 的 layers 為新的順序
        terrain.terrainData.terrainLayers = reorderedLayers;

        // 重新繪製地形
        terrain.Flush();

        Debug.Log("Layer Palette 已成功更新！");
    }
}
