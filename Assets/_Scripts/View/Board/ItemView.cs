using System;
using DG.Tweening;
using UnityEngine;

public class ItemView : MonoBehaviour
{
    [field:SerializeField]  public ItemType Type { get; private set; }
    private ItemModel model;
    public event Action CompleteMove;
    private GridConfig config;
    
    public void SetUp(ItemModel model,GridConfig config)
    {
        this.config = config;
        this.model = model;
        model.Matched += ReleaseItem;
        model.PositionChange += MoveToPosition;
    }

    public void ReleaseItem()
    {
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        model.Matched -= ReleaseItem;
        model.PositionChange -= MoveToPosition;
    }

    public void SetStartPosition(int x,int y)
    {
        Vector2 gridInWorld = config.GetGridInWorld();
        Vector3 position = new Vector2(y,x) * config.CellSize + gridInWorld;
        this.transform.position = position;
    }
    
    public void MoveToPosition(int x,int y,float duration)
    {
        Vector2 gridInWorld = config.GetGridInWorld();
        Vector3 position = new Vector2(y,x) * config.CellSize + gridInWorld;
        transform.DOMove(position, duration).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            CompleteMove?.Invoke();
        });
    }
    
}
