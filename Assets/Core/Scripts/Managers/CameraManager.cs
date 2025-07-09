using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float BoardToCameraRatio =1;
    private Camera targetCamera;

    void Start()
    {
        targetCamera = Camera.main;
        if (BoardManager.instance)
        {
            BoardManager.instance.OnSetUpBoard += OnBoardSetup;
        }
    }


    private void OnDisable()
    {
        if (BoardManager.instance)
        {
            BoardManager.instance.OnSetUpBoard -= OnBoardSetup;
        }
    }

    public void OnBoardSetup(Vector2Int boardSize)
    {
        ResizeCameraByBoardSize(boardSize);

    }

    /// <summary>
    /// Zoom out camera so that the whole board can fit in view
    /// </summary>
    /// <param name="boardSize"></param>
    void ResizeCameraByBoardSize(Vector2Int boardSize)
    {
        int size = Math.Max(boardSize.x,boardSize.y);
        targetCamera.orthographicSize = size*BoardToCameraRatio;
    }
}
