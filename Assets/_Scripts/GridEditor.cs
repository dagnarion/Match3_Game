using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(GameBoard))]
public class GridEditor : Editor
{
    private GameBoard gameBoard;
    private void Awake()
    {
        gameBoard = (GameBoard) target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Init"))
        {
            gameBoard.Init();
        }
        if (GUILayout.Button("CreateGrid"))
        {
            gameBoard.CreateBoard();
        }        
        if (GUILayout.Button("ClearGrid"))
        {
            gameBoard.ClearBoard();
        }
    }
}
