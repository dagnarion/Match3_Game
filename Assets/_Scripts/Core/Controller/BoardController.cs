using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardController : MonoBehaviour
{
    [field:SerializeField] public GridConfig Config { get; private set; }
    [field:SerializeField] public BoardView boardView { get; private set; }
    public GridModel<ItemModel> GridModel { get; private set; }
    public BoardModel BoardModel { get; private set; }
    private GridView gridView;
    public MatchModel MatchModel { get; private set; }
    public GravityModel GravityModel { get; private set; }
    private SwapingModel swapingModel;
    public SwapingController SwapingController { get; private set; }

    private StateMachine StateMachine;
    public SwapState SwapState { get; private set; }
    public MatchState MatchState { get; private set; }
    public RefillState RefillState { get; private set; }
    public GravityState GravityState { get; private set; }

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        GridModel = new GridModel<ItemModel>(Config.GridSize.x, Config.GridSize.y);
        gridView = new GridView(Config);
        BoardModel = new BoardModel(GridModel, Config);
        BoardModel.OnItemCreate += boardView.CreateFillItemToBoard;
        boardView.Init(Config);
        MatchModel = new MatchModel(GridModel, Config);
        GravityModel = new GravityModel(GridModel, Config);
        swapingModel = new SwapingModel(GridModel);
        SwapingController = new SwapingController(swapingModel, Config);

        GravityModel.OnItemChange += boardView.DoItemsAnimation;
        swapingModel.OnItemChange += boardView.DoItemsAnimation;
        
        StateMachine = new StateMachine();
        SwapState = new SwapState(this,StateMachine);
        MatchState = new MatchState(this,StateMachine);
        RefillState = new RefillState(this,StateMachine);
        GravityState = new GravityState(this,StateMachine);
    }

    private void Update()
    {
        StateMachine.Update();
        // SwapingController.SwapingHandle();
        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     MatchModel.Match();
        //     for (int x = 0; x < Config.GridSize.x; x++)
        //     for (int y = 0; y < Config.GridSize.y; y++)
        //     {
        //         if (GridModel.IsEmptyCell(x, y)) continue;
        //         if (GridModel.GetCell(x, y).IsMatched)
        //         {
        //            boardView.RemoveItemOnCell(GridModel.GetCell(x, y).ID);
        //             GridModel.ClearACell(x, y);
        //         }
        //     }
        // }
        //
        // if (Input.GetKeyDown(KeyCode.R))
        // {
        //     BoardModel.FillItemToBoard();
        //     GravityModel.ApplyGravity();
        // }
        //
        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     GridDebug();
        // }
        //
        // if (Input.GetKeyDown(KeyCode.B))
        // {
        //     GravityModel.ApplyGravity();
        // }
    }

    private void Start()
    {
        BoardModel.FillItemToBoard();
        GravityModel.ApplyGravity();
        StateMachine.Initialize(RefillState);
    }

    private void OnDestroy()
    {
        BoardModel.OnItemCreate -= boardView.CreateFillItemToBoard;
    }

    private void OnDrawGizmos()
    {
        if (Config == null || gridView == null) return;
        gridView.Draw();
    }

    private void GridDebug()
    {
        for (int x = 0; x < Config.GridSize.x; x++)
        {
            string s = "";
            for (int y = 0; y < Config.GridSize.y; y++)
            {
                if (GridModel.GetCell(x, y) == null) s += " 0";
                else
                {
                    s += (" " + GridModel.GetCell(x, y).Type.ToString()[0]);
                }
            }

            Debug.Log(s);
        }
    }
}