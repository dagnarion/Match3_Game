using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Grid/GridConfig")]
public class GridContext : ScriptableObject
{
    [field:SerializeField] public Vector2Int gridSize { get; private set; }
    [field:SerializeField] public Vector2 cellSize { get; private set; }
    [field:SerializeField] public Vector2Int gridPosition { get; private set; }
    
    public Vector2 GetGridInWorld()
    {
        Vector2 offSet = new Vector2(-0.5f * cellSize.x * (gridSize.x - 1), -0.5f * cellSize.y * (gridSize.y - 1));
        Vector2 gridPositionInWorld = gridPosition+ offSet;
        return gridPositionInWorld;
    }
}
