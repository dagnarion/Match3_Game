using System.Collections.Generic;
using UnityEngine;

public class BombEffect : ISpecialEffect
{
    public List<Vector2Int> ApplyEffect(int x, int y, GridConfig config)
    {
        List<Vector2Int> holder = new List<Vector2Int>();
        Vector2Int currentDirection = new Vector2Int(x, y);
        foreach (var direction in DirectionModel.EightDirection)
        {
            Vector2Int nextDirection = currentDirection + direction;
            Vector2Int farDirection = nextDirection + direction;
            Vector2Int far2Direction = farDirection + direction;
            if (nextDirection.x < 0 || nextDirection.x >= config.GridSize.x || nextDirection.y <0 || nextDirection.y >= config.GridSize.y) continue;
            holder.Add(nextDirection);
            if (farDirection.x < 0 || farDirection.x >= config.GridSize.x || farDirection.y <0 || farDirection.y >= config.GridSize.y) continue;
            holder.Add(farDirection); 
            if (far2Direction.x < 0 || far2Direction.x >= config.GridSize.x || far2Direction.y <0 || far2Direction.y >= config.GridSize.y) continue;
            holder.Add(far2Direction);
        }
        return holder; 
    }
}
