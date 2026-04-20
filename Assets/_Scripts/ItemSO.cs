using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO/Item")]
public class ItemSO : ScriptableObject
{
  [field:SerializeField] public ItemConfig Type { get; private set; }
  [field:SerializeField] public Sprite Sprite { get; private set; }
  [field:SerializeField] public RuntimeAnimatorController Animation { get; private set; }
}

public enum ItemConfig
{
    None = 0,
    Yellow = 1,
    Green = 2,
    Gray = 3,
    Red = 4,
    Blue = 5
}
