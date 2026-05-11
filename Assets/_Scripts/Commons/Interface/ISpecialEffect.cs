using System.Collections.Generic;
using UnityEngine;

public interface ISpecialEffect
{
    public List<Vector2Int> ApplyEffect(int x,int y,GridConfig config);
}
