using UnityEngine;
using System.Collections;
public class 点击事件穿透 : MonoBehaviour, ICanvasRaycastFilter
{
    public bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        return false;
    }
}