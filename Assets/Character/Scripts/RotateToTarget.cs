using System;
using UnityEngine;

public class RotateToTarget : MonoBehaviour
{
    public float rotationSpeed = 20f;
    private Vector2 direction;
    public GameObject nonDefaultTarget;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (nonDefaultTarget)
            direction = nonDefaultTarget.transform.position - transform.position;
        else
        {
            direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
