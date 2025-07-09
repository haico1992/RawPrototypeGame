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
        EventManager.Subscribe(EventNames.OnSetupBoard, OnBoardSetup);
    }


    private void OnDisable()
    {
        EventManager.Unsubscribe(EventNames.OnSetupBoard, OnBoardSetup);
    }

    private void OnBoardSetup(object boardSize)
    {
        ResizeCameraByBoardSize((Vector2Int)boardSize);
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
