using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera m_MainCamera;

    [Header("Follow Settings")]
    public float smoothTime = 0.3f;
    private Vector3 targetPosition;
    private Vector3 rawPosition;

    [Header("Subpixel Camera")]
    public bool pixelSnap;
    public bool adjustCamera;
    private float pixelSize;
    public RectTransform renderImage;

    [Header("Debugging")]
    public GameObject[] targets;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;


    void Start()
    {
        m_MainCamera = Camera.main;
        pixelSize = 2f * m_MainCamera.orthographicSize / m_MainCamera.pixelHeight;
        rawPosition = offset;
    }

    void Update()
    {
        GetTargets();
        if(targets.Length != 0)
        {
            Move();
        }
    }

    void GetTargets()
    {
        targets = GameObject.FindGameObjectsWithTag("Player");
    }

    void Move()
    {
        if(targets != null) {

            targetPosition = CalculateTargetPos() + offset;
            targetPosition = Vector3.SmoothDamp(rawPosition, targetPosition, ref velocity, smoothTime);
            rawPosition = targetPosition;
            if(pixelSnap)
            {
                //MOVE CAMERA
                Vector3 wordDelta = targetPosition - transform.position;
                Vector3 localDelta = transform.InverseTransformDirection(wordDelta) / pixelSize;
                localDelta.x = Mathf.RoundToInt(localDelta.x);
                localDelta.y = Mathf.RoundToInt(localDelta.y);
                localDelta.z = Mathf.RoundToInt(localDelta.z);
                transform.position += transform.right * localDelta.x * pixelSize + transform.up * localDelta.y * pixelSize + transform.forward * localDelta.z * pixelSize;   
            
                //ADJUST RENDER
                if (renderImage != null && adjustCamera)
                {
                    Vector3 cameraDelta = (targetPosition - transform.position);
                    Vector2 cameraOffset = new Vector2(cameraDelta.x,cameraDelta.z) / pixelSize;
                    cameraOffset = cameraOffset * 1080 / m_MainCamera.pixelHeight; 
                    
                    renderImage.anchoredPosition = - cameraOffset;
                }
            }

            else 
            {
                transform.position = targetPosition;
            }
        }    
    }

    Vector3 CalculateTargetPos()
    {
        Vector3 tp = new Vector3(0,0,0);

        foreach(GameObject g in targets)
        {
            tp += g.transform.position;
        }

        return tp / targets.Length;
    }

}
