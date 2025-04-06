using System;
using UnityEngine;

public class PixelPerfectFollow : MonoBehaviour
{
    public Transform target;          // The object to follow
    public float pixelsPerUnit = 16f; // Your sprite’s PPU (16 for pixel art)
    public Transform objScrean;
    public Transform pixelCamera;
    public float mul = 1;
    public float time = 1;

    float unitsPerPixel;

    private void Start()
    {
        Time.timeScale = time;

        // unitsPerPixel = (Camera.main.ViewportToWorldPoint(new Vector3(1f / 320f, 0, 0)) - Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0))).x;
    }
    void LateUpdate()
    {
        unitsPerPixel = 1f / pixelsPerUnit;

        //float offset = transform.position.x % unitsPerPixel;

        transform.position = target.position + new Vector3(0, 2, -10);


        float snappedX = Mathf.Round(transform.position.x / unitsPerPixel) * unitsPerPixel;
        float snappedY = Mathf.Round(transform.position.y / unitsPerPixel) * unitsPerPixel;


        objScrean.transform.localPosition = new Vector3(snappedX - transform.position.x, snappedY - transform.position.y, 5);


        // Vector3 screanOffset = pixelCamera.position - transform.position;
        // objScrean.transform.localPosition = new Vector3(screanOffset.x, screanOffset.y, 5);

        pixelCamera.transform.position = new Vector3(snappedX, snappedY, -10);
    }
}
