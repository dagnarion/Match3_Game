using UnityEngine;

public static class ItemFactory
{
    public static GameObject CreateItem(ItemSO itemDefination)
    {
        return CreateGameObject(itemDefination); // chỗ này sẽ add component tương ứng cho từng loại item
    }

    private static GameObject CreateGameObject(ItemSO itemDefination)
    {
        GameObject item = new GameObject(itemDefination.name);
        
        Animator itemAnimator = item.AddComponent<Animator>();
        itemAnimator.runtimeAnimatorController = itemDefination.Animation;
        
        SpriteRenderer spriteRenderer = item.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemDefination.Sprite;
        itemAnimator.Rebind();
        itemAnimator.Update(0f);
        
        return item;
    }
}