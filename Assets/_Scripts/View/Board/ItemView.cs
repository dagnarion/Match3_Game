using System;
using DG.Tweening;
using UnityEngine;

public class ItemView : MonoBehaviour
{
    [field:SerializeField]  public ItemType Type { get; private set; }
    public event Action CompleteMove;
    public void SetUp(ItemModel model,GridConfig config)
    {
        Vector2 gridInWorld = config.GetGridInWorld();
        Vector3 position = new Vector2(model.x,model.y) * config.CellSize + gridInWorld;
        this.transform.position = position;
    }

    public void MoveToPosition(Vector2 position,float duration)
    {
        transform.DOMove(position, duration).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            CompleteMove?.Invoke();
        });
    }
    
}
