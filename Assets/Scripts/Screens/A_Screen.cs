using UnityEngine;
using System;

public abstract class A_Screen : MonoBehaviour
{
    [SerializeField] private UISCREENS _screenType;
    [HideInInspector] public CanvasManager canvasManager;

    public UISCREENS screenType => _screenType;
}
