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
    [UnitySetUp]
    public IEnumerator Setup()
    {
        GameObject gameGameObject =
                    Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/!/Prefabs/Board.prefab"));
        board = gameGameObject.GetComponent<Board>();
        yield return new WaitForSeconds(0.1f);
        board.Spawn();
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(board.gameObject);
    }
    [Test]
    public void CheckLeftCollision()
    {
        board.currentPosition = new Vector2Int(-4, 7);
        board.UpdateCurrentPositions(board.currentPosition);
        Debug.Log(board.tetromino.ToString());
        board.HandleMoveLeft();
        Vector2Int checkPos = board.currentPosition;
        board.HandleMoveLeft();
        Assert.AreEqual(checkPos, board.currentPosition);
    }

    [Test]
    public void CheckRightCollision()
    {
        board.currentPosition = new Vector2Int(3, 7);
        board.UpdateCurrentPositions(board.currentPosition);
        Debug.Log(board.tetromino.ToString());
        board.HandleMoveRight();
        Vector2Int checkPos = board.currentPosition;
        board.HandleMoveRight();
        Assert.AreEqual(checkPos, board.currentPosition);
    }

    // [Test]
    // public void CheckDownCollision()
    // {
    //     board.currentPosition = new Vector2Int(-1, -10);
    //     board.UpdateCurrentPositions(board.currentPosition);
    //     Debug.Log(board.tetromino.ToString());
    //     board.HandleFall();
    //     Vector2Int checkPos = board.currentPosition;
    //     board.HandleFall();
    //     Assert.AreEqual(checkPos, board.currentPosition);
    // }
}
