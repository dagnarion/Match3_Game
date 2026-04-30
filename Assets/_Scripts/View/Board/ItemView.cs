using System;
using DG.Tweening;
using UnityEngine;

public class ItemView : MonoBehaviour
{
    [field:SerializeField]  public ItemType Type { get; private set; }
    private ItemModel model;
    private GridConfig config;
    
    public void SetGridConfig(GridConfig config)
    {
        this.config = config;
    }

    public void SetItemModel(ItemModel model)
    {
        this.model = model;
    }

    public void ReleaseItem()
    {
        this.gameObject.SetActive(false);
    }
    

    public void SetStartPosition(int x,int y)
    {
        Vector2 gridInWorld = config.GetGridInWorld();
        Vector3 position = new Vector2(y,x) * config.CellSize + gridInWorld;
        this.transform.position = position;
    }
    
    public Tween DoTransition(float duration)
    {
        Vector2 gridInWorld = config.GetGridInWorld();
        Vector3 position = new Vector2(model.y,model.x) * config.CellSize + gridInWorld;
        return transform.DOMove(position, duration).SetEase(Ease.OutQuad).Pause();
    }
    
}
