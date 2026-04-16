using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO/Item")]
public class ItemSO : ScriptableObject
{
   public Item Type { get; private set; }
   public SpriteRenderer Sprite { get; private set; }
}

public enum Item
{
    None = 0,
    Item1 = 1,
    Item2 = 2,
    Item3 = 3
}
