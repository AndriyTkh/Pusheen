using UnityEngine;

public class Head : MonoBehaviour
{
    public GameObject parentPart;
    public GameObject partInst;
    public int partIndex = 4;
    public float maxDist = 4f;

    void Start()
    {
        if (partIndex != 0)
        {
            var instanse = Instantiate(partInst, transform.position, transform.rotation);
            instanse.GetComponent<Head>().parentPart = gameObject;
            instanse.GetComponent<Head>().partIndex = partIndex - 1;
        }
    }

    void FixedUpdate()
    {
        if (parentPart)
        {
            if (Vector2.Distance(gameObject.transform.position, parentPart.transform.position) > maxDist)
            {
                Vector2 toTargetPart = (gameObject.transform.position - parentPart.transform.position).normalized;
                transform.position = (Vector2)parentPart.transform.position + toTargetPart * maxDist;
            }
        }

    }
}
