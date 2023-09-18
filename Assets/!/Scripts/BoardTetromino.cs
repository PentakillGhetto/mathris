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
        // 1. Convert current cell position to float Vector2
        // 2. Subtract current position from cell position (move tetromino to x=0,y=0); it's required for the correct rotation
        // 3. Rotate current position with rotation matrix
        // 4. Move rotated tetromino back to its original place
        Positions = Positions.Select(cell => new Vector2(cell.x, cell.y) - Position).Select(cell =>
        {
            // I and O tetrominoes have their pivot located differently which is 0.5 to the left in X axis, 0 otherwise
            cell.x += PivotOffset;
            cell.y += PivotOffset;
            int x, y;
            // if tetromino is either I or O
            if (PivotOffset != 0)
            {
                x = Mathf.CeilToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                y = Mathf.CeilToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
            }
            else
            {
                x = Mathf.RoundToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                y = Mathf.RoundToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
            }
            return new Vector2Int(x, y);
        }).Select(cell => cell += Position).ToList();
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
