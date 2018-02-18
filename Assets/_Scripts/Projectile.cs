using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : Entity {

	Rigidbody2D rb;

	public LayerMask hitMask;

	bool hasHitObject;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D> ();
	}

	public void Shoot(float work, Vector3 dir)
	{
		float velocity = Mathf.Sqrt ((2f * work) / rb.mass);

		rb.velocity = dir * velocity;
	}

	void Update()
	{
		if (!hasHitObject) {
			transform.eulerAngles = new Vector3 (0, 0, 90 + Mathf.Atan2 (rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg);
	
			Vector2 origin = base.Position2D;
			Vector2 dir = -Up2D;
			float rayLength = (transform.lossyScale.y / 2f) + (rb.velocity.magnitude) * Time.deltaTime;

			if (Physics2D.Raycast (origin, dir, rayLength, hitMask)) {

				rb.velocity = Vector2.zero;
				rb.Sleep ();
				hasHitObject = true;
			} 
		}
	}
}
