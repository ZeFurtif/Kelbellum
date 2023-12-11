using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMotion : MonoBehaviour
{

    public Vector2 cycleDurationXZ = new Vector2(20f, 20f);
    public AnimationCurve movementPathX;
    public AnimationCurve movementPathZ;
    public Vector2 movementMagnitudeXZ = new Vector2(1,1);
    public Vector2 movementTimeOffsetXZ = new Vector2();

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        UpdateMotion();
    }

    private void UpdateMotion()
    {
        float timeX = Time.time % cycleDurationXZ.x;
        timeX /= cycleDurationXZ.x;

        float timeZ = Time.time % cycleDurationXZ.y;
        timeZ /= cycleDurationXZ.y;

        float newX = movementPathX.Evaluate(timeX + movementTimeOffsetXZ.x) * movementMagnitudeXZ.x;
        float newZ = movementPathX.Evaluate(timeZ + movementTimeOffsetXZ.y) * movementMagnitudeXZ.y;

        transform.position = initialPosition + new Vector3(newX, 0, newZ);
    }
}
