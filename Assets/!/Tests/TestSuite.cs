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
    [UnityTest]
    public IEnumerator TetraMoveLeft()
    {
        GameObject gameGameObject =
            Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/!/Prefabs/Board.prefab"));
        board = gameGameObject.GetComponent<Board>();
        yield return new WaitForSeconds(0.1f);
        board.Spawn();
        Vector2Int initialPos = board.currentPosition;
        Vector2Int expectedPos = initialPos + Vector2Int.left;
        board.HandleMoveLeft();
        Assert.AreEqual(expectedPos, board.currentPosition);
        Object.Destroy(board.gameObject);
    }
}
