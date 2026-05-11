using System.Collections.Generic;
using UnityEngine;

public class HorizontalStrippedEffect : ISpecialEffect
{
    public List<Vector2Int> ApplyEffect(int x, int y, GridConfig config)
    {
        List<Vector2Int> holder = new List<Vector2Int>();
        for(int i = 0; i < config.GridSize.y; i++)
            holder.Add(new Vector2Int(x, i));
        return holder;
    }
}
