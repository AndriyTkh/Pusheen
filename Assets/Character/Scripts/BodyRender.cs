using System;
using UnityEngine;

public class BodyRender : MonoBehaviour
{
    public int length;
    public LineRenderer lineRen;
    public Vector3[] segmentPoses;
    private Vector3[] segmentV;

    public Transform targetDirection;
    public float targetDistance;
    public float smoothSpeed = 0.35f;

    public float wiggleSpeed = 10;
    public float wiggleMagnitude = 20;
    public Transform wiggleDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRen.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentV = new Vector3[length];
        ResetPos();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        wiggleDirection.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude);


        segmentPoses[0] = targetDirection.position;

        for (int i = 1; i < segmentPoses.Length; i++)
        {
            Vector3 targetPos = segmentPoses[i - 1] + (segmentPoses[i] - segmentPoses[i - 1]).normalized * targetDistance;
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], targetPos, ref segmentV[i], smoothSpeed);
        }
        lineRen.SetPositions(segmentPoses);
    }

    private void ResetPos()
    {
        segmentPoses[0] = targetDirection.position;
        for (int i = 1; i < length; i++)
        {
            segmentPoses[i] = segmentPoses[i - 1] + targetDirection.right * targetDistance;
        }
        lineRen.SetPositions(segmentPoses);
    }
}
