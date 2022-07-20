using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraResolution : MonoBehaviour
{
    [SerializeField] private Camera cam;

    void Awake()
    {
        Vector2 resolution = GetComponent<CanvasScaler>().referenceResolution;
        Rect rect = cam.rect;
        float scaleHeight = ((float) Screen.width / Screen.height) / ((float) resolution.x / resolution.y);
        float scaleWidth = 1f / scaleHeight;

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

        cam.rect = rect;
    }
}