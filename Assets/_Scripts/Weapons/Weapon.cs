using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : Entity {

	[Header("-Weapon-")]
	public float sensitivity = 1;
	public float drawLength;

	public bool pointOutRadially = true;

	[Header("Graphics")]
	public bool drawGraphics;

	public GameObject indicatorPrefab;
	Image indicator;

	public float lineWidth;
	public float indicatorRadius;
	float sqrMaxDrawMagnitude;

	public Color lineColor;
	public Color indicatorColor;

	protected Vector2 mouseSetPos;
	Vector2 currentMousePos;

	public LineRenderer graphicsLineRenderer;


	public virtual void Awake()
	{
		if (drawGraphics) {

			graphicsLineRenderer.startColor = graphicsLineRenderer.endColor = lineColor;
			graphicsLineRenderer.startWidth = lineWidth;
			graphicsLineRenderer.endWidth = .01f;

			indicator = Instantiate (indicatorPrefab, Vector3.zero, Quaternion.identity).GetComponentInChildren<Image>();
			indicator.transform.localScale = Vector3.one * indicatorRadius;
			indicator.color = indicatorColor; 

			sqrMaxDrawMagnitude = Mathf.Pow(
		}
	}

	public void Update()
	{
		if (pointOutRadially && !Input.GetMouseButton(0)) {

			Vector3 playerScreenPos = Camera.main.WorldToScreenPoint (transform.parent.parent.position);
			transform.rotation = Quaternion.Euler (new Vector3 (EulerAngles.x, EulerAngles.y, Mathf.Atan2 (Input.mousePosition.y - playerScreenPos.y, Input.mousePosition.x - playerScreenPos.x) * Mathf.Rad2Deg));
		}
	}

	public float DrawPercent()
	{
		return Mathf.Clamp01((Mathf.Abs((mouseSetPos - currentMousePos).magnitude) * sensitivity) / drawLength); 
	}

	public virtual void Fire()
	{
		
	}

	public virtual void OnMouseDown()
	{
		mouseSetPos = Input.mousePosition;
	}

	public virtual void OnMouseHold()
	{
		currentMousePos = Input.mousePosition;

		if (drawGraphics) {

			indicator.transform.position = Camera.main.ScreenToWorldPoint (mouseSetPos);
			indicator.transform.position = new Vector3 (indicator.transform.position.x, indicator.transform.position.y, 0);

			indicator.fillAmount = Mathf.Abs(Mathf.Atan2 (Input.mousePosition.y - mouseSetPos.y, Input.mousePosition.x - mouseSetPos.x) / (Mathf.PI));

			Vector3 pos = Camera.main.ScreenToWorldPoint (mouseSetPos);
			graphicsLineRenderer.SetPosition (0, new Vector3 (pos.x, pos.y, 0));

			pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			pos = new Vector3 (pos.x, pos.y, 0);

			if ((pos - graphicsLineRenderer.GetPosition(0).sqrMagnitude) > )

			graphicsLineRenderer.SetPosition (1, pos);

		}
	}

	public virtual void OnMouseRelease()
	{
		if (drawGraphics) {
			
			indicator.transform.position = Vector3.zero;
			indicator.fillAmount = 0;

			graphicsLineRenderer.SetPosition (0, Vector3.zero);
			graphicsLineRenderer.SetPosition (1, Vector3.zero);
		}

		Fire ();
	}
}
