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

        unitsPerPixel = (Camera.main.ViewportToWorldPoint(new Vector3(1f / 80f, 0, 0)) - Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0))).x;
    }
    void Update()
    {
        if (!target) return;

        Vector3 camPos = target.position;

        float offset = unitsPerPixel != 0 ? transform.position.x % unitsPerPixel : 0;

        transform.position = target.position + new Vector3(0, 0, -10);

        // Debug.Log($"Camera {transform.position.x} offset {offset} screan {objScrean.transform.localPosition.x}");
        // Debug.Log(Camera.allCamerasCount);
        // Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1)));
        Debug.Log(Camera.main.ViewportToWorldPoint(new Vector3(1f / 81f, 0, 1)) - Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 1)));

        objScrean.transform.localPosition = new Vector3(offset * mul, 0, 5);

        float snappedX = Mathf.Round(camPos.x / unitsPerPixel) * unitsPerPixel;
        float snappedY = Mathf.Round(camPos.y / unitsPerPixel) * unitsPerPixel;

        pixelCamera.position = new Vector3(snappedX, snappedY, -10);
    }
}
