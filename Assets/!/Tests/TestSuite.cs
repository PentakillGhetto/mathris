using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSuite
{
    private Board board;

    [SetUp]
    public void Setup()
    {
        GameObject gameGameObject =
                    Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/!/Prefabs/Board.prefab"));
        board = gameGameObject.GetComponent<Board>();
        board.bounds = new RectInt(new Vector2Int(-board.size.x / 2, -board.size.y / 2), board.size);
        board.boardTetromino.Initialize(board.tetrominoes[Random.Range(0, board.tetrominoes.Count)], board.initialPosition);
        board.Paint();
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(board.gameObject);
    }

    [Test]
    public void TetraMoveLeft()
    {
        Vector2Int initialPosition = board.boardTetromino.Position;
        Vector2Int expectedPosition = initialPosition + Vector2Int.left;
        board.HandleMoveLeft();
        Assert.AreEqual(expectedPosition, board.boardTetromino.Position);
    }

    [Test]
    public void TetraMoveRight()
    {
        Vector2Int initialPosition = board.boardTetromino.Position;
        Vector2Int expectedPosition = initialPosition + Vector2Int.right;
        board.HandleMoveRight();
        Assert.AreEqual(expectedPosition, board.boardTetromino.Position);
    }
}
