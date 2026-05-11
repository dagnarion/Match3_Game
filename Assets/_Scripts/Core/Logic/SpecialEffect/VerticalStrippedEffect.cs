using System.Collections.Generic;
using UnityEngine;

public class VerticalStrippedEffect : ISpecialEffect
{
    public List<Vector2Int> ApplyEffect(int x, int y, GridConfig config)
    {
        List<Vector2Int> holder = new List<Vector2Int>();
        for (int i = 0; i < config.GridSize.x; i++)
        {
            holder.Add(new Vector2Int(i, y));
        }
        return holder;
    }
}
