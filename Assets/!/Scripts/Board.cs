using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(BoardTetromino))]
public class Board : MonoBehaviour
{
    public Tilemap tilemap;
    public List<Tetromino> tetrominoes;
    public BoardTetromino boardTetromino;
    public Vector2Int initialPosition;
    public Vector2Int size;
    [HideInInspector] public RectInt bounds;

    void Start()
    {
        bounds = new RectInt(new Vector2Int(-size.x / 2, -size.y / 2), size);
        boardTetromino.Initialize(tetrominoes[Random.Range(0, tetrominoes.Count)], initialPosition);
        Paint();
    }

    public void Paint() =>
        boardTetromino.Positions.ForEach(position =>
            tilemap.SetTile(position.ToVector3Int(), boardTetromino.Tile)
    );

    public void Clear() =>
        boardTetromino.Positions.ForEach(position =>
            tilemap.SetTile(position.ToVector3Int(), null)
        );

    public bool IsValidMove(Vector2Int translation) =>
        boardTetromino.Positions.TrueForAll(position =>
            !tilemap.HasTile((position + translation).ToVector3Int()) && bounds.Contains(position + translation));

    public bool Move(Vector2Int translation)
    {
        bool isMoved = false;
        if (IsValidMove(translation))
        {
            boardTetromino.Move(translation);
            isMoved = true;
        }
        return isMoved;
    }


    public bool TestWallKicks(int rotationIndex, int rotationDirection)
    {
        int wallKickIndex = GetWallKickIndex(rotationIndex, rotationDirection);

        for (int i = 0; i < boardTetromino.WallKicks.Get_X_Length; i++)
        {
            Vector2Int translation = boardTetromino.WallKicks[i, wallKickIndex];

            if (Move(translation))
            {
                return true;
            }
        }

        return false;
    }

    public int GetWallKickIndex(int rotationIndex, int rotationDirection)
    {
        int wallKickIndex = rotationIndex * 2;

        if (rotationDirection < 0)
        {
            wallKickIndex--;
        }

        return Utils.Wrap(wallKickIndex, 0, boardTetromino.WallKicks.Get_Y_Length);
    }

    public void HandleMoveLeft()
    {
        Clear();
        Move(Vector2Int.left);
        Paint();
    }

    public void HandleMoveRight()
    {
        Clear();
        Move(Vector2Int.right);
        Paint();
    }


    public void HandleFall()
    {
        Clear();
        Move(Vector2Int.down);
        Paint();
    }

    public void HandleHardFall()
    {
        Clear();
        while (Move(Vector2Int.down))
        {
            continue;
        }
        Paint();
    }

    public void HandleRotateRight()
    {
        Clear();
        boardTetromino.Rotate(1);
        TestWallKicks(boardTetromino.rotationIndex, 1);
        Paint();
    }

    public void HandleRotateLeft()
    {
        Clear();
        boardTetromino.Rotate(-1);
        TestWallKicks(boardTetromino.rotationIndex, -1);
        Paint();
    }
}
