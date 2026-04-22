using System;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class GameBoard : MonoBehaviour
{
    [Header("REFERENCE")] 
    [SerializeField] private GridContext context;

    private SwapHandler swapHandler;
    private DragHandler dragHandler;
    private GridSpaceConverter gridSpaceConverter;
    private MatchDetector matchDetector;
    public  Grid slots { get; private set; }
    private GridDraw gridDraw;

    public event Action Swaping;
    public event Action Handling;
    public event Action Matching;

    private BoardState currentState = BoardState.Nozmal;
    
    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        slots = new Grid(context.gridSize);
        gridDraw = new GridDraw(context);
        gridSpaceConverter = new GridSpaceConverter(context);
        matchDetector = new MatchDetector(context, slots,this);
        swapHandler = new SwapHandler(context, gridSpaceConverter, slots);
        dragHandler = new DragHandler(this, swapHandler);
    }
    
    private void Update()
    {
        switch (currentState)
        {
            case BoardState.Swaping:
                break;
            
            case BoardState.Matching:
                break;
            
            case BoardState.Nozmal:
                break;
            
            case BoardState.Calculate:
                break;
        }
    }

    private void SwapingState()
    {
        Swaping?.Invoke();
    }
    
    
    public void OnDrawGizmos()
    {
        if (gridDraw == null) return;
        gridDraw.ChangeGridContext(context);
        gridDraw.Draw();
    }
    public enum BoardState
    {
        Nozmal = 0,
        Swaping = 1,
        Matching = 2,
        Calculate = 3
    }
    
}

