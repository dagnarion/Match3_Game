using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [Header("CONFIG")] 
    [SerializeField] private bool IsGizmosDraw;
    [SerializeField] private GridContext context;
    private GridSpaceConverter gridSpaceConverter;
    private Vector2 gridPositionInWorld;
    private Grid<ItemSO> slots;
    private IDraw _gridGizmosDraw;
    
    private Camera mainCam;
    
    
    private void Awake()
    {
        slots = new Grid<ItemSO>(context.gridSize);
        _gridGizmosDraw = new GridGizmosDraw(context);
        gridSpaceConverter = new GridSpaceConverter(context);
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(gridSpaceConverter.GetCell(mousePosition));
        }
    }


    public void OnDrawGizmos()
    {
        if(_gridGizmosDraw == null || !IsGizmosDraw) return;
        _gridGizmosDraw.Draw();
    }
    
}
