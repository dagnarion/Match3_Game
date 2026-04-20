using UnityEngine;

public class Item : MonoBehaviour
{
   private ItemSO itemData;

   public ItemConfig GetItemType()
   {
      return itemData.Type;
   }

   public void Init(ItemSO itemData)
   {
      this.itemData = itemData;
   }
}
