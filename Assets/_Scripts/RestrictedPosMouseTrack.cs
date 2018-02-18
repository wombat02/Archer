using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictedPosMouseTrack : MonoBehaviour {

	public float maxDist;
	public float minDist;

	public float smoothing;

	bool toggle = true;

	Vector3 smoothDampVec;

	public void Toggle()
	{
		toggle = !toggle;
	}

	void Update()
	{
		if (toggle) {
			Vector3 screenPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			screenPoint = new Vector3 (screenPoint.x, screenPoint.y, 0);

			transform.position = Vector3.SmoothDamp (transform.position, transform.parent.position + (screenPoint - transform.parent.position).normalized *
			Mathf.Clamp ((screenPoint - transform.parent.position).magnitude, minDist, maxDist), ref smoothDampVec, smoothing);
		}
	}
}
