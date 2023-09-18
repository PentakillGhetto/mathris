using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class BoardTetromino : MonoBehaviour
{
    public Tetromino Tetromino { get; private set; }
    public List<Vector2Int> Positions { get; private set; }
    public Vector2Int Position { get; private set; }
    public int rotationIndex;

    // public List<Vector2Int> Cells => tetromino.cells;
    public Tile Tile => Tetromino.tile;
    public float PivotOffset => Tetromino.pivotOffset;

    void Start()
    {
        rotationIndex = 0;
    }

    public void Initialize(Tetromino tetromino)
    {
        Initialize(tetromino, Vector2Int.zero);
    }

    public void Initialize(Tetromino tetromino, Vector2Int position)
    {
        Tetromino = tetromino;
        Position = position;
        Positions = Tetromino.cells.Select(cell => cell += position).ToList();
    }

    public void Move(Vector2Int translation)
    {
        Position += translation;
        Positions = Positions.Select(cell => cell += translation).ToList();
    }

    public void Rotate(int direction)
    {
        rotationIndex = Wrap(rotationIndex + direction, 0, 4);

        List<Vector2Int> newPositions = new(Positions);

        for (int i = 0; i < Positions.Count; i++)
        {
            var cell = Positions[i] - Position;

            int x, y;

            x = Mathf.RoundToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
            y = Mathf.RoundToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));

            newPositions[i] = new Vector2Int(x, y) + Position;
        }
        Positions = newPositions;
        // Positions = Tetromino.cells.Select(cell => new Vector2(cell.x, cell.y)).Select(cell =>
        // {
        //     // cell.x += PivotOffset;
        //     // cell.y += PivotOffset;
        //     int x = Mathf.RoundToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
        //     int y = Mathf.RoundToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
        //     return new Vector2Int(x, y);
        // }).ToList();
        // }).Select(cell => cell += Position).ToList();
    }

    /// <summary>
    /// Puts value between min and max inclusively
    /// </summary>
    /// <param name="value"></param>
    /// <param name="min">Lower bound</param>
    /// <param name="max">Higher bound</param>
    /// <returns>Min if value is bigger than max, max if value is less than min, value otherwise</returns>
    public int Wrap(int value, int min, int max) => value < min ?
    max - (min - value) % (max + min) :
         min + (value - min) % (max - min);
}
