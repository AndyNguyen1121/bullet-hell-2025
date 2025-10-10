using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicCameraSettings : MonoBehaviour
{
    public Camera UICam;
    public Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        UICam = GetComponent<Camera>();
        mainCam = Camera.main;

        float targetaspect = 16.0f / 9.0f;

        float windowaspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        // obtain camera component so we can modify its viewport

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = mainCam.rect;
            UICam.rect = rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            mainCam.rect = rect;
            UICam.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = mainCam.rect;
            UICam.rect = rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            mainCam.rect = rect;
            UICam.rect = rect;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UICam.fieldOfView = mainCam.fieldOfView;
    }
}
