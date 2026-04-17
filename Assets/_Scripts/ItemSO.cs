using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO/Item")]
public class ItemSO : ScriptableObject
{
  [field:SerializeField] public Item Type { get; private set; }
  [field:SerializeField] public Sprite Sprite { get; private set; }
  [field:SerializeField] public RuntimeAnimatorController Animation { get; private set; }
}

public enum Item
{
    None = 0,
    Item1 = 1,
    Item2 = 2,
    Item3 = 3,
    SpecialItem1 = 4,
    SpecialItem2 = 5
}
