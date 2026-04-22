using System;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class GameBoard : MonoBehaviour
{
    [Header("REFERENCE")] 
    [SerializeField] private GridContext context;
    public SwapHandler SwapHandler { get; private set; }
    private GridSpaceConverter gridSpaceConverter;
    private MatchDetector matchDetector;
    public  Grid slots { get; private set; }
    private GridDraw gridDraw;
    
    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        slots = new Grid(context.gridSize);
        gridDraw = new GridDraw(context);
        gridSpaceConverter = new GridSpaceConverter(context);
        matchDetector = new MatchDetector(context, slots);
        SwapHandler = new SwapHandler(context, gridSpaceConverter, slots);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
           matchDetector.MatchAllBoard();
        }
    }
    
    public void OnDrawGizmos()
    {
        if (gridDraw == null) return;
        gridDraw.ChangeGridContext(context);
        gridDraw.Draw();
    }
    
}

public enum BoardState
{
    Nozmal = 0,
    Swaping = 1,
    Handling = 2,
    Calculate = 3
}