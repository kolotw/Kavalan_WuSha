using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Profiling;

public class 地圖材質控制器 : MonoBehaviour
{
    public Terrain terrain; // 拖曳你的 Terrain 物件到這裡
    public int[] newOrder; // 設定新的順序，例如 [2, 0, 1] 表示第三個 layer 移到第一個，第一個 layer 移到第二個，第二個 layer 移到第三個
    private int currentLayerIndex = 0; // 追蹤當前需要移到最上面的圖層索引

    void Start()
    {
        // 初始化 newOrder，與當前 layers 數量相同
        int layersCount = terrain.terrainData.terrainLayers.Length;
        newOrder = new int[layersCount];
        for (int i = 0; i < layersCount; i++)
        {
            newOrder[i] = i;
        }

        // 初次更新將第一個圖層移到最上面
        MoveLayerToTop(currentLayerIndex);
    }

    void Update()
    {
        // 這裡可以設置一個定時器或其他條件來定期觸發更新
        if (Input.GetKeyDown(KeyCode.U)) // 例如按下 U 鍵
        {
            // 更新 currentLayerIndex 並循環
            currentLayerIndex = (currentLayerIndex + 1) % newOrder.Length;
            MoveLayerToTop(currentLayerIndex);
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
        // 設置新的順序
        this.newOrder = newOrder;

        // 取得當前 Terrain 的 layers
        TerrainLayer[] originalLayers = terrain.terrainData.terrainLayers;

        if (originalLayers.Length != newOrder.Length)
        {
            Debug.LogError("新順序陣列的長度必須與現有 layers 的數量相同！");
            return;
        }

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
