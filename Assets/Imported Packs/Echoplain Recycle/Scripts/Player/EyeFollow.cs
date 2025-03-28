using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFollow : MonoBehaviour
{
    private Camera _cam;
    private Vector2 _moucePos;

    void Start()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        _moucePos = _cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 difference = _moucePos - (Vector2)transform.position;
        float rot2 = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, rot2 - 90);
    }
}
