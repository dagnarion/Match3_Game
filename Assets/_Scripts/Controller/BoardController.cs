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
        if(config == null) return;
        gridView.Draw();
    }
}