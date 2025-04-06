using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetToCamera : MonoBehaviour
{
    // Attach this to the Quad
    void Start()
    {
        float unitsWide = 322f / 16f; // PPU = 16
        float unitsTall = 182f / 16f;

        transform.localScale = new Vector3(unitsWide, unitsTall, 1);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
