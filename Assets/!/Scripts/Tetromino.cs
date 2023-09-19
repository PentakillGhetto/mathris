using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Tetromino", menuName = "Tetromino")]
public class Tetromino : ScriptableObject
{
    public List<Vector2Int> cells;
    public Array2D<Vector2Int> wallKicks;
    public Tile tile;
    public float pivotOffset;
}