using UnityEngine;

public class ItemView : MonoBehaviour
{
    [field:SerializeField]  public ItemType Type { get; private set; }
    public void SetUp(ItemModel model,GridConfig config)
    {
        Vector2 gridInWorld = config.GetGridInWorld();
        Vector3 position = new Vector2(model.x,model.y) * config.CellSize + gridInWorld;
        this.transform.position = position;
    }
}
