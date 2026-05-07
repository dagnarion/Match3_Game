using System.Collections.Generic;
using UnityEngine;

public class DirectionModel
{
    public static List<Vector2Int> Direction = new List<Vector2Int>()
    {
        Vector2Int.right,
        Vector2Int.left,
        Vector2Int.up,
        Vector2Int.down
    };
    
    public Vector2Int GetDirection(Vector2 delta)
    {
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            return delta.x > 0 ? Vector2Int.right : Vector2Int.left;
        else
            return delta.y > 0 ? Vector2Int.up : Vector2Int.down;
    }
}
