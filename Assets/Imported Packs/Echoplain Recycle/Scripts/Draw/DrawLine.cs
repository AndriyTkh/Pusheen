using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DrawLine : MonoBehaviour
{

	public LineRenderer lineRenderer;
	public EdgeCollider2D edgeCollider;
	public Rigidbody2D rigidBody;

	[HideInInspector] public List<Vector2> points = new List<Vector2>();
	[HideInInspector] public int pointsCount = 0;

	//The min-max distance between line's points.
	public float pointsMinDistance = 0.1f;
	public float pointsMaxDistance = 2f;

	//Circle collider added to each line's point
	float circleColliderRadius;

	//Destruction particle
	public GameObject particle;

	private void OnMouseOver()
    {
		if (Input.GetMouseButton(1))
			SelfDestroy();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DestroyBox"))
        {
			SelfDestroy();
        }
    }

    public void SelfDestroy()
    {
		Vector2 worldPos;

		foreach (Vector2 point in points)
		{
			worldPos = edgeCollider.transform.TransformPoint(point);
			Instantiate(particle, worldPos, particle.transform.rotation);
		}

		SoundManager.Instance.PlaySound3D("Crumbling", transform.position);

		Destroy(gameObject);
	}

    public void AddPoint(Vector2 newPoint)
	{
		//If distance between last point and new point is less than pointsMinDistance do nothing (return)
		if (pointsCount >= 1 && Vector2.Distance(newPoint, GetLastPoint()) < pointsMinDistance)
			return;

		points.Add(newPoint);
		pointsCount++;

		//Add Circle Collider to the Point
		CircleCollider2D circleCollider = this.gameObject.AddComponent<CircleCollider2D>();
		circleCollider.offset = newPoint;
		circleCollider.radius = circleColliderRadius;

		//Line Renderer
		lineRenderer.positionCount = pointsCount;
		lineRenderer.SetPosition(pointsCount - 1, newPoint);

		//Edge Collider
		//Edge colliders accept only 2 points or more (we can't create an edge with one point :D )
		if (pointsCount > 1)
			edgeCollider.points = points.ToArray();

		SoundManager.Instance.PlaySound3D("Writing", transform.position);
	}

	public Vector2 GetLastPoint()
	{
		return (Vector2)lineRenderer.GetPosition(pointsCount - 1);
	}

	public Vector2 GetSecondLastPoint()
	{
		return (Vector2)lineRenderer.GetPosition(pointsCount - 2);
	}

	public void UsePhysics(bool usePhysics)
	{
		// isKinematic = true  means that this rigidbody is not affected by Unity's physics engine
		rigidBody.isKinematic = !usePhysics;
	}

	public void SetLineColor(Gradient colorGradient)
	{
		lineRenderer.colorGradient = colorGradient;
	}

	public void SetPointsMinDistance(float distance)
	{
		pointsMinDistance = distance;
	}

	public void SetLineWidth(float width)
	{
		lineRenderer.startWidth = width;
		lineRenderer.endWidth = width;

		circleColliderRadius = width / 2f;

		edgeCollider.edgeRadius = circleColliderRadius;
	}

}