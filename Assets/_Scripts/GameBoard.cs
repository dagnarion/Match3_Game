using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [Header("REFERENCE")]
    [SerializeField] private LineRenderer lineRender;
    
    [Header("CONFIG")] 
    [SerializeField] private bool IsGizmosDraw;
    [SerializeField] private GridContext context;
    private GridSpaceConverter gridSpaceConverter;
    private Vector2 gridPositionInWorld;
    private Grid<ItemSO> slots;
    private GridDraw gridDraw;
    
    private Camera mainCam;
    
    
    private void Awake()
    {
        slots = new Grid<ItemSO>(context.gridSize);
        gridDraw = new GridDraw(context);
        gridSpaceConverter = new GridSpaceConverter(context);
        mainCam = Camera.main;
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 cell = gridSpaceConverter.GetCell(mousePosition);
           if(cell != new Vector2(9999,9999))  Debug.Log(cell);
        }
    }
    


    public void OnDrawGizmos()
    {
        if(gridDraw == null || !IsGizmosDraw) return;
        gridDraw.Draw();
    }
    
}
