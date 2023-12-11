using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToPixelGrid : MonoBehaviour
{

    private Camera m_MainCamera;

    public float pixelSize;

    void Start()
    {
        m_MainCamera = Camera.main;
        float pixelSize = 2f * m_MainCamera.orthographicSize / m_MainCamera.pixelHeight;
    }

    void LateUpdate()
    {
        
    }
}
