using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public float speed = 20f;
    private Vector2 targetPos;
    public GameObject nonDefaultTarget;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (nonDefaultTarget)
            targetPos = nonDefaultTarget.transform.position;
        else
        {
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }
}
