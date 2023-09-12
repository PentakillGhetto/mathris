using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public List<Tetromino> tetrominoes;
    public Tilemap tilemap;
    public Vector2Int initialPosition;
    private Tetromino tetromino;
    private Vector2Int currentPosition;

    void Start()
    {
        tetromino = tetrominoes[Random.Range(0, tetrominoes.Count)];
        currentPosition = initialPosition;
        Spawn(currentPosition);
    }

    void Spawn(Vector2Int position) =>
        tetromino.cells.ForEach(cell =>
        tilemap.SetTile(new Vector3Int(cell.x + position.x, cell.y + position.y, 0), tetromino.tile)
        );

    void Move(Vector2Int translation)
    {
        tilemap.ClearAllTiles();
        currentPosition += translation;
        Spawn(currentPosition);
    }


    public void HandleMoveLeft()
    {
        Move(Vector2Int.left);
    }

    public void HandleMoveRight()
    {
        Move(Vector2Int.right);
    }


    public void HandleNextSign()
    {
        Move(Vector2Int.up);
    }

    public void HandleFall()
    {
        Move(Vector2Int.down);
    }
}
