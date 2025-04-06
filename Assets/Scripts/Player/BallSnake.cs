using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class BallSnake : MonoBehaviour
{
    public int _length;
    [Range(0.01f, 3f)] public float maxDistance = 0.4f;
    //[Range(5, 90)] public int minAngle = 30;
    //[Range(0.01f, 3f)] public float minDistance = 0.3f;
    //public LineRenderer lineRen;

    public GameObject bodyInst;
    public GameObject bodyParent;
    public List<Transform> childrenList;

    //private Vector3[] segmentV;

    // public Transform targetDirection;
    // public float targetDistance;
    // public float smoothSpeed = 0.35f;

    // public float wiggleSpeed = 10;
    // public float wiggleMagnitude = 20;
    // public Transform wiggleDirection;

    void Start()
    {
        //lineRen.positionCount = _length;

        for (int i = 0; i < _length; i++)
        {
            GameObject instance = Instantiate(bodyInst, transform.position, transform.rotation);
            childrenList.Add(instance.transform);
        }

        //childrenList = bodyParent.GetComponentsInChildren<Transform>();
    }


    void FixedUpdate()
    {
        foreach (var item in childrenList)
        {
            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        SetPosition(childrenList[0].gameObject, transform.position);

        // -------- Distance constraint ---------
        for (int i = 1; i < _length; i++)
        {
            if (Vector2.Distance(childrenList[i].position, childrenList[i - 1].position) > maxDistance)
            {
                Vector2 targetDir = (childrenList[i].position - childrenList[i - 1].position).normalized;

                SetPosition(childrenList[i].gameObject, childrenList[i - 1].position + (Vector3)targetDir * maxDistance);
            }
        }

        //lineRen.SetPositions(segmentPoses);
    }

    private void SetPosition(GameObject obj, Vector3 position)
    {
        obj.transform.position = position;
    }
}
