using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class TestCollision
{
    private Board board;
    [SetUp]
    public void Setup()
    {
        GameObject gameGameObject =
                    Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/!/Prefabs/Board.prefab"));
        board = gameGameObject.GetComponent<Board>();
        board.bounds = new RectInt(new Vector2Int(-board.size.x / 2, -board.size.y / 2), board.size);
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(board.gameObject);
    }

    [Test]
    public void CheckLeftCollision()
    {
        board.boardTetromino.Initialize(board.tetrominoes[Random.Range(0, board.tetrominoes.Count)], new Vector2Int(-4, 7));
        board.Paint();
        board.HandleMoveLeft();
        Vector2Int checkPos = board.boardTetromino.Position;
        board.HandleMoveLeft();
        Assert.AreEqual(checkPos, board.boardTetromino.Position);
    }

    [Test]
    public void CheckRightCollision()
    {
        board.boardTetromino.Initialize(board.tetrominoes[Random.Range(0, board.tetrominoes.Count)], new Vector2Int(3, 7));
        board.Paint();
        board.HandleMoveRight();
        Vector2Int checkPos = board.boardTetromino.Position;
        board.HandleMoveRight();
        Assert.AreEqual(checkPos, board.boardTetromino.Position);
    }

    [Test]
    public void CheckDownCollision()
    {
        board.boardTetromino.Initialize(board.tetrominoes[Random.Range(0, board.tetrominoes.Count)], new Vector2Int(-1, -10));
        board.Paint();
        board.HandleFall();
        Vector2Int checkPos = board.boardTetromino.Position;
        board.HandleFall();
        Assert.AreEqual(checkPos, board.boardTetromino.Position);
    }
}
