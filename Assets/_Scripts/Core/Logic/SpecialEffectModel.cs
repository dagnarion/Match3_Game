using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectModel
{
   private GridModel<ItemModel> grid;
   private GridConfig config;
   private BoardView boardView;
   private Dictionary<ItemModifier, ISpecialEffect> effectHolder = new Dictionary<ItemModifier, ISpecialEffect>()
   {
      {ItemModifier.HorizontalStripped,new HorizontalStrippedEffect() },
      {ItemModifier.VerticalStripped,new VerticalStrippedEffect() },
      {ItemModifier.Wrapped,new WrappedEffect() }
   };
   
   public SpecialEffectModel(GridModel<ItemModel> grid,GridConfig config)
   {
      this.grid = grid;
      this.config = config;
   }

   public void ApplyEffect(int x,int y)
   {
      Queue<Vector2Int> effectQueue = new Queue<Vector2Int>();
      bool[,] visited = new bool[config.GridSize.x,config.GridSize.y];
      effectQueue.Enqueue(new Vector2Int(x,y));
      
      while (effectQueue.Count > 0)
      {
         Vector2Int pos = effectQueue.Dequeue();
         ItemModel item = grid.GetCell(pos.x, pos.y);
         if(item == null || visited[pos.x,pos.y]) continue;
         visited[pos.x, pos.y] = true;
         item.SetMatchState(true);

         if (effectHolder.TryGetValue(item.Modifier, out ISpecialEffect specialEffect))
         {
            List<Vector2Int> holder = specialEffect.ApplyEffect(pos.x, pos.y, config);
            foreach (var it in holder)
               effectQueue.Enqueue(it);
         }
      }
      
   }

   
   
}
