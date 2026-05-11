using System.Collections.Generic;
using UnityEngine;

public class WrappedEffect : ISpecialEffect
{
    public List<Vector2Int> ApplyEffect(int x, int y, GridConfig config)
    {
        List<Vector2Int> holder = new List<Vector2Int>();
        Vector2Int currentDirection = new Vector2Int(x, y);
        foreach (var direction in DirectionModel.EightDirection)
        {
            Vector2Int nextDirection = currentDirection + direction;
            if (nextDirection.x < 0 || nextDirection.x >= config.GridSize.x || nextDirection.y <0 || nextDirection.y >= config.GridSize.y) continue;
            holder.Add(nextDirection);
        }
        return holder;
    }
}
