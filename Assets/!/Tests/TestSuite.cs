using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSuite
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
    public void TetraMoveLeft()
    {
        Vector2Int initialPos = board.currentPosition;
        Vector2Int expectedPos = initialPos + Vector2Int.left;
        board.HandleMoveLeft();
        Assert.AreEqual(expectedPos, board.currentPosition);
    }

    [Test]
    public void TetraMoveRight()
    {
        Vector2Int initialPos = board.currentPosition;
        Vector2Int expectedPos = initialPos + Vector2Int.right;
        board.HandleMoveRight();
        Assert.AreEqual(expectedPos, board.currentPosition);
    }


}
