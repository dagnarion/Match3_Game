using UnityEngine;

public static class ItemFactory
{
    public static GameObject CreateItem(ItemSO itemDefination,Vector2 position,Transform holder)
    {
        GameObject items = CreateGameObject(itemDefination);
        items.transform.SetParent(holder);
        items.transform.position = position;
        return items ; // chỗ này sẽ add component tương ứng cho từng loại item
    }

    private static GameObject CreateGameObject(ItemSO itemDefination)
    {
        GameObject item = new GameObject(itemDefination.name);

        item.AddComponent<Item>().Init(itemDefination);
        
        Animator itemAnimator = item.AddComponent<Animator>();
        itemAnimator.runtimeAnimatorController = itemDefination.Animation;
        
        SpriteRenderer spriteRenderer = item.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemDefination.Sprite;
        itemAnimator.Rebind();
        itemAnimator.Update(0f);
        
        return item;
    }
}