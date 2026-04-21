using UnityEngine;
using DG.Tweening;
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

   public void SetPosition(Vector3 position)
   {
      transform.DOMove(position, 0.2f).SetEase(Ease.InOutSine);
   }
}
