using System;
using UnityEngine;

public class BoardManager : MonoBehaviour
{


    public GameObject whiteChessPrefab;
    public GameObject blackChessPrefab;

    private const float tileSize = 1.0f;
    private const float tileOffset = 0.5f;

    private int selectionX = -1;
    private int selectionY = -1;

    private int lastSelectionX = -1;
    private int lastSelectionY = -1;

    private int role = 2;
    private bool first = true;
    private bool end = false;

    GameObject clone = null;
    GameObject[,] boardData = new GameObject[15, 15];
    private int[] lastAIMove = null;

    // Use this for initialization
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("BGM");
        ChessAI.board = new int[ChessAI.size, ChessAI.size];
    }

    // Update is called once per frame
    void Update()
    {
        //DrawChessBoard();
        UpdateSelection();
        if (!end)
        {
            if (role == 1)
            {
                DrawPrefab();
            }
            else
            {
                AITurn();
            }
        }
    }

    private void UpdateSelection()
    {
        if (!Camera.main)
        {
            Debug.Log("No Camera");
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50.0f, LayerMask.GetMask("ChessBoardCube")))
        {
            //Debug.Log(hit.point);
            selectionX = (int)(hit.point.x + tileOffset);
            selectionY = (int)(hit.point.z + tileOffset);
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }

    private void DrawChessBoard()
    {
        Vector3 widthLine = Vector3.right * 14;
        Vector3 heightLine = Vector3.forward * 14;

        for (int x = 0; x <= 14; x++)
        {
            Vector3 start = Vector3.right * x;
            Debug.DrawLine(start, start + heightLine);
            for (int y = 0; y <= 14; y++)
            {
                start = Vector3.forward * y;
                Debug.DrawLine(start, start + widthLine);
            }
        }

        if (selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(Vector3.right * (selectionX - tileOffset) + Vector3.forward * (selectionY - tileOffset), Vector3.right * (selectionX + tileOffset) + Vector3.forward * (selectionY + tileOffset));
            Debug.DrawLine(Vector3.right * (selectionX - tileOffset) + Vector3.forward * (selectionY + tileOffset), Vector3.right * (selectionX + tileOffset) + Vector3.forward * (selectionY - tileOffset));
        }
    }

    private void DrawPrefab()
    {
        first = false;
        if (clone != null && selectionX == lastSelectionX && selectionY == lastSelectionY && Input.GetMouseButtonDown(0) && boardData[selectionX, selectionY] == null)
        {
            Debug.Log(String.Format("Black Chess Down:{0},{1}", selectionX, selectionY));
            boardData[selectionX, selectionY] = (GameObject)Instantiate(blackChessPrefab, new Vector3(selectionX, 0, selectionY), Quaternion.identity);
            ChessAI.board[selectionX, selectionY] = 1;
            end = ChessAI.CheckWinner(selectionX, selectionY, 1);
            Debug.Log(end);
            role = 2;
        }
        if (clone != null && (selectionX != lastSelectionX || selectionY != lastSelectionY))
        {
            Destroy(clone);
        }
        if (clone == null && selectionX >= 0 && selectionX < 15 && selectionY >= 0 && selectionY < 15 && boardData[selectionX, selectionY] == null)
        {
            clone = (GameObject)Instantiate(blackChessPrefab, new Vector3(selectionX, 0, selectionY), Quaternion.identity);
            lastSelectionX = selectionX;
            lastSelectionY = selectionY;
        }
    }

    private void AITurn()
    {
        if (first)
        {
            first = false;
            Debug.Log(String.Format("White Chess Down:{0},{1},{2}", 7, 7, 0));
            boardData[7, 7] = (GameObject)Instantiate(whiteChessPrefab, new Vector3(7, 0, 7), Quaternion.identity);
            ChessAI.board[7, 7] = 2;
            end = ChessAI.CheckWinner(7, 7, 2);
            Debug.Log(end);
            role = 1;
            return;
        }
        int[] maxPoint = ChessAI.NextMove(role);
        Debug.Log(String.Format("White Chess Down:{0},{1},{2}", maxPoint[0], maxPoint[1], maxPoint[2]));
        boardData[maxPoint[0], maxPoint[1]] = (GameObject)Instantiate(whiteChessPrefab, new Vector3(maxPoint[0], 0, maxPoint[1]), Quaternion.identity);
        ChessAI.board[maxPoint[0], maxPoint[1]] = 2;
        end = ChessAI.CheckWinner(maxPoint[0], maxPoint[1], 2);
        Debug.Log(end);
        role = 1;
    }
}
