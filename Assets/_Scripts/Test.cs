using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private GridConfig config;
    Vector2 cellSize;
    Vector2 originPosition;
    Vector2 cellOffSet;
    

    private void Awake()
    {
        cellSize = config.CellSize;
        cellOffSet = 0.5f * cellSize;
        Vector2 offSet = new Vector2(-0.5f * cellSize.x * (config.GridSize.x - 1), -0.5f * cellSize.y * (config.GridSize.y - 1));
        originPosition = (Vector2)Camera.main.transform.position + offSet;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GetMousePos(mousePos);
        }
    }

    void PrintCell(int x, int y)
    {
        if (x < 0 || y < 0 || x >= config.GridSize.x || y >= config.GridSize.y) return;
        Debug.Log(x + " " + y);
    }

    void GetXYInScreen(Vector2 worldPos, out int x, out int y)
    {
        x = Mathf.FloorToInt(((worldPos - originPosition).x + cellOffSet.x) / cellSize.x);
        y = Mathf.FloorToInt(((worldPos - originPosition).y + cellOffSet.y) / cellSize.y);
    }

    void GetMousePos(Vector2 worldPos)
    {
        int x, y;
        GetXYInScreen(worldPos, out x, out y);
        PrintCell(x, y);
    }
}
