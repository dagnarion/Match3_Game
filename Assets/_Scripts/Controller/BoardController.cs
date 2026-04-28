using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardController : MonoBehaviour
{
    [SerializeField] private GridConfig config;
    [SerializeField] private BoardView boardView;
    private GridModel<ItemModel> grid;
    private BoardModel boardModel;
    private GridView gridView;
    private MatchModel matchModel;
    private GravityModel gravityModel;
    private SwapingModel swapingModel;
    private SwapingController swapingController;
    private void Awake()
    {
        Init();
    }

    void Init()
    {
        grid = new GridModel<ItemModel>(config.GridSize.x,config.GridSize.y);
        gridView = new GridView(config);
        boardModel = new BoardModel(grid,config);
        boardModel.OnItemCreate += boardView.CreateFillItemToBoard;
        boardView.Init(config);
        matchModel = new MatchModel(grid,config);
        gravityModel = new GravityModel(grid, config);
        swapingModel = new SwapingModel(grid);
        swapingController = new SwapingController(swapingModel,config);
    }

    private void Update()
    {
       swapingController.SwapingHandle();
        if (Input.GetKeyDown(KeyCode.A))
        {
            matchModel.Match();
            for(int x = 0;x<config.GridSize.x;x++)
            for (int y = 0; y < config.GridSize.y; y++)
            {
                if(grid.IsEmptyCell(x,y)) continue;
                if (grid.GetCell(x, y).IsMatched)
                {
                    grid.GetCell(x,y).DisableItem();
                    grid.ClearACell(x,y);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            boardModel.FillItemToBoard();
            gravityModel.ApplyGravity();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            GridDebug();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            gravityModel.ApplyGravity();
        }
    }

    private void Start()
    {
        boardModel.FillItemToBoard();
        gravityModel.ApplyGravity();
    }

    private void OnDestroy()
    {
        boardModel.OnItemCreate -= boardView.CreateFillItemToBoard;
    }
    
    private void OnDrawGizmos()
    {
        if(config == null || gridView == null) return;
        gridView.Draw();
    }

    private void GridDebug()
    {
        for (int x = 0; x < config.GridSize.x; x++)
        {
            string s = "";
            for (int y = 0; y < config.GridSize.y; y++)
            {
                if (grid.GetCell(x,y) == null) s += " 0";
                else
                {
                    s += (" " + grid.GetCell(x,y).Type.ToString()[0]) ;
                }
            }
            Debug.Log(s);
        }
    }
}