using UnityEngine;
[CreateAssetMenu(fileName = "GridConfig" ,menuName = "GridConfig/Config")]
public class GridConfig : ScriptableObject
{
    [field:SerializeField] public Vector2 CellSize { get; private set; }
    [field:SerializeField] public Vector2Int GridSize { get; private set; }
    [field:SerializeField] public Vector2Int GridPosition { get; private set; }
    public Vector2 GetGridInWorld()
    {
        Vector2 offSet = new Vector2(-0.5f * CellSize.x * (GridSize.x - 1), -0.5f * CellSize.y * (GridSize.y - 1));
        Vector2 gridPositionInWorld = GridPosition+ offSet;
        return gridPositionInWorld;
    }
}
