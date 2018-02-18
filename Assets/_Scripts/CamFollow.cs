using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CamFollow : MonoBehaviour {

	public enum States {

		Hovering,
		Moving,
		Inspecting
	}

	public Transform target;

	[Header("Settings")]
	public Vector3 offset;
	public float smoothDampTime;

	public ZoomControls zoomControls;

	Camera cam;

	States currentState;

	Vector3 smoothDamp;

	void Awake()
	{
		cam = GetComponent<Camera> ();
	}

	void Update()
	{
		if (target != null) {

			Vector3 targetPos = target.position + offset;
			transform.position = Vector3.SmoothDamp (transform.position, targetPos, ref smoothDamp, smoothDampTime);
		}
	}

	[System.Serializable]
	public struct ZoomControls {

		public float hoveringZoom;
		public float switchingPOIZoom;
		public float inspectingZoom;
	}
}
