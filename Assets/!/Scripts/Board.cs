using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public List<Tetromino> tetrominoes;
    public Tilemap tilemap;
    public Vector2Int initialPosition;
    public Vector2Int size;
    public Tetromino tetromino;
    public Vector2Int currentPosition;

    void Start()
    {
        tetromino = tetrominoes[Random.Range(0, tetrominoes.Count)];
        currentPosition = initialPosition;
        Spawn(currentPosition);
    }

    public void Spawn(Vector2Int position) =>
        tetromino.cells.ForEach(cell =>
        tilemap.SetTile(new Vector3Int(cell.x + position.x, cell.y + position.y, 0), tetromino.tile)
        );

    public void Clear()
    {
        tetromino.cells.ForEach(cell =>
        tilemap.SetTile(new Vector3Int(cell.x + currentPosition.x, cell.y + currentPosition.y, 0), null)
        );
    }

    public bool IsValidMove(Vector2Int position) =>
        tetromino.cells.TrueForAll(cell =>
    !tilemap.HasTile(new Vector3Int(cell.x + position.x, cell.y + position.y, 0)) &&
        new RectInt(new Vector2Int(-this.size.x / 2, -this.size.y / 2), this.size).Contains(cell + position));

    bool Move(Vector2Int translation)
    {
        bool isMoved = false;
        Clear();

        if (IsValidMove(currentPosition + translation))
        {
            currentPosition += translation;
            isMoved = true;
        }

        Spawn(currentPosition);
        return isMoved;
    }


    public void HandleMoveLeft()
    {
        Move(Vector2Int.left);
    }

    public void HandleMoveRight()
    {
        Move(Vector2Int.right);
    }


    public void HandleFall()
    {
        Move(Vector2Int.down);
    }
}
