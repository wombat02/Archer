using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

	public Vector3 Position {

		get { return transform.position; }
		set { transform.position = value; }
	}

	public Vector2 Position2D {

		get { return new Vector2 (Position.x, Position.y); }
		set { Position = new Vector3 (value.x, value.y, Position.z); }
	}

	public Vector2 Up2D {

		get { return new Vector2 (transform.up.x, transform.up.y); }
	}

	public Vector2 Right2D {

		get { return new Vector2 (transform.right.x, transform.right.y); }
	}

	public Vector2 Forward2D {

		get { return new Vector2 (transform.forward.x, transform.forward.y); }
	}

	public Vector3 EulerAngles {

		get { return transform.eulerAngles; }
		set { transform.eulerAngles = value; }
	}
		
	public float FromToDistance (Entity other)
	{
		return (other.Position - Position).magnitude;
	}

	public float FromToDistanceHorizontal (Entity other)
	{
		return (new Vector2 (other.Position.x, other.Position.y) - new Vector2 (Position.x, Position.y)).magnitude;
	}

	public float FromToDistanceVertical (Entity other)
	{
		return other.Position.y - Position.y;
	}

	public Vector3 DirTo (Entity other)
	{
		return (other.Position - Position).normalized;
	}
}
