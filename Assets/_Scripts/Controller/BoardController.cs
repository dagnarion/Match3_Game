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
    private void Awake()
    {
        Init();
    }

    void Init()
    {
        grid = new GridModel<ItemModel>(config.GridSize.x,config.GridSize.y);
        gridView = new GridView(config);
        boardModel = new BoardModel(grid,config);
        boardModel.OnItemCreate += boardView.CreateItemOnBoard;
        boardView.Init(config);
        matchModel = new MatchModel(grid,config);
        gravityModel = new GravityModel(grid, config);
    }

    private void Update()
    {
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

        if (Input.GetKeyDown(KeyCode.B))
        {
            gravityModel.ApplyGravity();
        }
    }

    private void Start()
    {
        boardModel.InitializeBoard();
    }

    private void OnDestroy()
    {
        boardModel.OnItemCreate -= boardView.CreateItemOnBoard;
    }
    
    private void OnDrawGizmos()
    {
        if(config == null || gridView == null) return;
        gridView.Draw();
    }
}