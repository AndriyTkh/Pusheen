using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ProperSnake : MonoBehaviour
{
    public int _length;
    [Range(0.01f, 3f)] public float maxDistance = 0.4f;
    [Range(5, 90)] public int minAngle = 30;
    //[Range(0.01f, 3f)] public float minDistance = 0.3f;
    public LineRenderer lineRen;
    public Vector3[] segmentPoses;
    //private Vector3[] segmentV;

    // public Transform targetDirection;
    // public float targetDistance;
    // public float smoothSpeed = 0.35f;

    // public float wiggleSpeed = 10;
    // public float wiggleMagnitude = 20;
    // public Transform wiggleDirection;

    void Start()
    {
        lineRen.positionCount = _length;
        segmentPoses = new Vector3[_length];
    }


    void FixedUpdate()
    {
        segmentPoses[0] = transform.position;

        // -------- Distance constraint ---------
        for (int i = 1; i < _length; i++)
        {
            if (Vector2.Distance(segmentPoses[i], segmentPoses[i - 1]) > maxDistance)
            {
                Vector2 targetDir = (segmentPoses[i] - segmentPoses[i - 1]).normalized;

                segmentPoses[i] = segmentPoses[i - 1] + (Vector3)targetDir * maxDistance;
            }
        }

        // ---------- Angle constraint ---------
        for (int i = 1; i < _length - 1; i++)
        {
            Vector2 v1 = segmentPoses[i] - segmentPoses[i - 1];
            Vector2 v2 = segmentPoses[i] - segmentPoses[i + 1];
            float angle = Vector2.Angle(v1, v2);

            //[cos alpha    sin alpha]
            //[ -sin alpha  cos alpha]
            // Thus the rotated vector with the same magnitude will be

            // (x cos alpha +y sin alpha, -x sin alpha +y cos alpha).

            if (angle < minAngle)
            {
                Debug.Log(angle);

                Vector2 V = segmentPoses[i + 1] - segmentPoses[i];

                V = new Vector2(V.x * math.cos(angle) + V.y * math.sin(angle), -V.x * math.sin(angle) + V.y * math.cos(angle));

                segmentPoses[i + 1] = segmentPoses[i] + (Vector3)V;
            }
        }

        lineRen.SetPositions(segmentPoses);
    }
}
