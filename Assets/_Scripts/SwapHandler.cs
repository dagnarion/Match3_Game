using UnityEngine;

public class SwapHandler
{
    private GridContext context;
    private GridSpaceConverter gridSpaceConverter;
    private Grid slots;

    public SwapHandler(GridContext context,GridSpaceConverter gridSpaceConverter,Grid slot)
    {
        this.context = context;
        this.gridSpaceConverter = gridSpaceConverter;
        this.slots = slot;
    }

    public void ChangeContext(GridContext context,Grid slot)
    {
        this.context = context;
        this.slots = slot;
    }
    
    public void SwapItem(Vector2 startPosition,Vector2Int direction)
    {
        Vector2Int currentObject = gridSpaceConverter.GetCell(startPosition);
        Vector2Int targetObject = currentObject +  direction;
        
        if(targetObject.x < 0 || targetObject.x >= context.gridSize.x || targetObject.y < 0 || targetObject.y >= context.gridSize.y) return;
        slots.SwapItem(currentObject,targetObject);

    }
}
