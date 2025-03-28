using UnityEngine;

public class DrawManager : MonoBehaviour
{

	public GameObject linePrefab;
	public LayerMask cantDrawOverLayer;
	int cantDrawOverLayerIndex;

	[Space(30f)]
	public Gradient lineColor;
	public float linePointsMinDistance;
	public float lineWidth;

	public DrawLine currentLine;
	Camera cam;

	[Space(30f)]
	public int maxChildNum;
	public int maxLineLength;
	public bool strictLength = false;




	void Start()
	{
		cam = Camera.main;
		cantDrawOverLayerIndex = LayerMask.NameToLayer("CantDrawOver");
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
			BeginDraw();

		if (currentLine != null)
			Draw();

		if (Input.GetMouseButtonUp(0))
			EndDraw();

		if (Input.GetMouseButtonDown(1))
        {
			if (currentLine != null)
				currentLine.SelfDestroy();
			EndDraw();
		}

		if (strictLength && currentLine != null && currentLine.pointsCount > 3)
        {
			if (Vector2.Distance(currentLine.GetLastPoint(), currentLine.GetSecondLastPoint()) > currentLine.pointsMaxDistance)
            {
				EndDraw();
            }
        }

		ChildCheck();
	}

    // Begin Draw ----------------------------------------------
    void BeginDraw()
	{
		currentLine = Instantiate(linePrefab, this.transform).GetComponent<DrawLine>();

		//Set line properties
		currentLine.UsePhysics(false);
		currentLine.SetLineColor(lineColor);
		currentLine.SetPointsMinDistance(linePointsMinDistance);
		currentLine.SetLineWidth(lineWidth);

	}
	// Draw ----------------------------------------------------
	void Draw()
	{
		Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

		//Check if mousePos hits any collider with layer "CantDrawOver", if true cut the line by calling EndDraw( )
		RaycastHit2D hit = Physics2D.CircleCast(mousePosition, lineWidth / 3f, Vector2.zero, 1f, cantDrawOverLayer);

		if (hit)
			EndDraw();
		else
			currentLine.AddPoint(mousePosition);

		if (currentLine != null)
        {
			if (currentLine.pointsCount > maxLineLength)
			{
				EndDraw();
			}
		}
	}
	// End Draw ------------------------------------------------
	void EndDraw()
	{
		if (currentLine != null)
		{
			if (currentLine.pointsCount < 2)
			{
				//If line has one point
				Destroy(currentLine.gameObject);
			}
			else
			{
				//Add the line to "CantDrawOver" layer
				//currentLine.gameObject.layer = cantDrawOverLayerIndex;

				//Activate Physics on the line
				currentLine.UsePhysics(true);

				currentLine = null;
			}
		}
	}

	// Child control lol ------------------------------------------------
	void ChildCheck()
    {
		if (transform.childCount > maxChildNum)
        {
			transform.GetChild(0).GetComponent<DrawLine>().SelfDestroy();
        }
    }
}