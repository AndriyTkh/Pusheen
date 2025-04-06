using UnityEngine;

public class PixelPerfectFollow : MonoBehaviour
{
    public Transform target;          // The object to follow
    public float pixelsPerUnit = 16f; // Your sprite’s PPU (16 for pixel art)

    void LateUpdate()
    {
        if (!target) return;

        Vector3 camPos = target.position;

        float unitsPerPixel = 1f / pixelsPerUnit;

        float snappedX = Mathf.Round(camPos.x / unitsPerPixel) * unitsPerPixel;
        float snappedY = Mathf.Round(camPos.y / unitsPerPixel) * unitsPerPixel;

        transform.position = new Vector3(snappedX, snappedY, transform.position.z);
    }
}
