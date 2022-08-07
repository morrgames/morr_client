using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// SafeArea영역안에서, scaler의 비율에 따라, cam의 viewport를 세팅.
/// </summary>
public class CameraScaler : MonoBehaviour
{
    [SerializeField] private CanvasScaler scaler;
    [SerializeField] private Camera cam;

    Rect LastSafeArea = new Rect(0, 0, 0, 0);

    void Awake()
    {
        Refresh();
    }

    private void Update()
    {
        Refresh();
    }

    void Refresh()
    {
        Rect safeArea = Screen.safeArea;
        if (safeArea != LastSafeArea)
        {
            ApplySafeArea(safeArea);
        }
    }

    void ApplySafeArea(Rect safeRect)
    {
        LastSafeArea = safeRect;
        Rect rect = cam.rect;

        //set safe area.
        {
            var tx = safeRect.x / Screen.width;
            var twidth = (safeRect.width) / Screen.width;

            var ty = safeRect.y / Screen.height;
            var theight = (safeRect.height) / Screen.height;

            rect.x = tx;
            rect.width = twidth;
            rect.y = ty;
            rect.height = theight;

            //
            cam.rect = rect;
        }

        //set ratio.
        {
            Vector2 resolution = scaler.referenceResolution;
            float scaleHeight = ((float) safeRect.width / safeRect.height) / ((float) resolution.x / resolution.y);
            float scaleWidth = 1f / scaleHeight;
            scaleWidth *= rect.width;
            scaleHeight *= rect.height;

            if (scaleHeight < 1)
            {
                rect.height = scaleHeight;
                rect.y = (1f - scaleHeight) / 2f;
            }
            else
            {
                rect.width = scaleWidth;
                rect.x = (1f - scaleWidth) / 2f;
            }

            //
            cam.rect = rect;
        }
    }
}