using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public List<Tetromino> tetrominoes;
    public Tilemap tilemap;
    public Vector2Int initialPosition;
    public Vector2Int size;
    [HideInInspector] public Tetromino tetromino;
    public Vector2Int currentPosition;
    [HideInInspector] public int currentRotationIndex;
    [HideInInspector] public List<Vector2Int> currentPositions;
    [HideInInspector] public RectInt bounds;

    void Start()
    {
        currentPosition = initialPosition;
        currentRotationIndex = 0;
        bounds = new RectInt(new Vector2Int(-size.x / 2, -size.y / 2), size);
        tetromino = tetrominoes[Random.Range(0, tetrominoes.Count)];
        currentPositions = new List<Vector2Int>(tetromino.cells);
        UpdateCurrentPositions(currentPosition);
        Spawn();
    }

    public void Spawn() =>
        currentPositions.ForEach(position =>
    tilemap.SetTile(position.ToVector3Int(), tetromino.tile)
    );

    private void UpdateCurrentPositions(Vector2Int translation)
    {
        for (int i = 0; i < currentPositions.Count; i++)
        {
            currentPositions[i] = currentPositions[i] + translation;
        }
    }

    public void Clear()
    {
        currentPositions.ForEach(position =>
        tilemap.SetTile(position.ToVector3Int(), null)
        );
    }

    public bool IsValidMove(Vector2Int translation) =>
        currentPositions.TrueForAll(position =>
    !tilemap.HasTile((position + translation).ToVector3Int()) && bounds.Contains(position + translation));

    bool Move(Vector2Int translation)
    {
        bool isMoved = false;
        Clear();

        if (IsValidMove(translation))
        {
            currentPosition += translation;
            UpdateCurrentPositions(translation);
            isMoved = true;
        }

        Spawn();
        return isMoved;
    }

    public void Rotate(int direction)
    {
        Clear();
        currentRotationIndex = Wrap(currentRotationIndex + direction, 0, 4);
        for (int i = 0; i < currentPositions.Count; i++)
        {
            Vector2Int cell = currentPositions[i];
            float x = 0, y = 0;
            x = (cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction);
            y = (cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction);

            currentPositions[i] = new(Mathf.CeilToInt(x + tetromino.pivotOffset), Mathf.CeilToInt(y + tetromino.pivotOffset));
        }
        Spawn();
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

    public void HandleHardFall()
    {
        while (Move(Vector2Int.down))
        {
            continue;
        }
    }

    public void HandleRotateRight()
    {
        Rotate(1);
    }

    public void HandleRotateLeft()
    {
        Rotate(-1);
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
