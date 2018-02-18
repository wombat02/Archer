using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Bow : Weapon {

	[Header("-Bow-")]
	public float drawStringK;
	public float drawStringLengthX;

	public float minBowEndAngle;
	public float maxBowEndAngle;
	float bowEndR;
	float drawStringLengthY;

	float maxX;
	float minX;

	public Projectile projectilePrefab;

	[SerializeField]
	public Transform[] bowEnds;
	[SerializeField]
	public Transform[] bowTips;

	Transform handle;

	LineRenderer lineRenderer;

	public override void Awake()
	{
		lineRenderer = GetComponent<LineRenderer> ();

		bowEndR = bowEnds [0].transform.lossyScale.y / 2f;
		handle = transform.Find ("Handle").transform;

		drawStringLengthY = bowEndR + (handle.localScale.y / 2f);

		minX = Mathf.Sqrt (Mathf.Pow (bowEndR, 2f) / (1f + Mathf.Pow (Mathf.Tan (minBowEndAngle * Mathf.Deg2Rad), 2f)));
		maxX = Mathf.Sqrt (Mathf.Pow (bowEndR, 2f) / (1f + Mathf.Pow (Mathf.Tan (maxBowEndAngle * Mathf.Deg2Rad), 2f)));

		AnimateBow (0);

		base.Awake ();
	}

	public override void Fire()
	{
		Projectile p = Instantiate (projectilePrefab, transform.position, Quaternion.Euler (0, 0, transform.eulerAngles.z - 90f)) as Projectile;

		float w = drawStringK * (Mathf.Sqrt (Mathf.Pow(base.DrawPercent() * drawStringLengthX, 2f) + Mathf.Pow(drawStringLengthY, 2f)) - drawStringLengthY);
		p.Shoot (w, -transform.right);

		base.Fire ();
	}

	public override void OnMouseHold()
	{
		base.OnMouseHold ();

		//bow end draw animation

		float handleAngle = Mathf.Atan2 (Input.mousePosition.y - base.mouseSetPos.y, Input.mousePosition.x - base.mouseSetPos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, handleAngle));

		AnimateBow (DrawPercent());

		lineRenderer.SetPosition (0, bowTips [0].position);
		lineRenderer.SetPosition (1, handle.position + handle.right * (minX + DrawPercent() * drawStringLengthX));
		lineRenderer.SetPosition (2, bowTips [1].position);
	}

	public override void OnMouseRelease()
	{
		lineRenderer.SetPosition (0, Vector3.zero);
		lineRenderer.SetPosition (1, Vector3.zero);
		lineRenderer.SetPosition (2, Vector3.zero);

		AnimateBow (0);

		base.OnMouseRelease ();
	}

	void AnimateBow (float drawPercent)
	{
		float x = drawPercent * bowEndR;

		float y = Mathf.Sqrt (Mathf.Pow (bowEndR, 2f) - Mathf.Pow (x, 2f));

		float bowEndAngle = Mathf.Atan2(y,x) * Mathf.Rad2Deg;   

		if (Mathf.Abs(bowEndAngle) < minBowEndAngle) {

			bowEndAngle = Mathf.Sign(bowEndAngle) * minBowEndAngle;
			x = minX;
			y = Mathf.Sqrt (Mathf.Pow (bowEndR, 2f) - Mathf.Pow (x, 2f));

		} else if (Mathf.Abs(bowEndAngle) > maxBowEndAngle) {

			bowEndAngle = Mathf.Sign(bowEndAngle) * maxBowEndAngle;
			x = maxX;
			y = Mathf.Sqrt (Mathf.Pow (bowEndR, 2f) - Mathf.Pow (x, 2f));

		}

		bowEnds [0].position = bowEnds[0].parent.position + bowEnds[0].parent.up * (y + bowEnds[0].parent.lossyScale.y / 2f) + bowEnds[0].parent.right * x;
		bowEnds [1].position = bowEnds[1].parent.position - bowEnds[1].parent.up * (y + bowEnds[0].parent.lossyScale.y / 2f) + bowEnds[1].parent.right * x;

		bowEnds [0].localRotation = Quaternion.Euler (new Vector3 (0, 0, 90 + bowEndAngle));
		bowEnds [1].localRotation = Quaternion.Euler (new Vector3 (0, 0, 270 - bowEndAngle));

	
	}
}
