using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : LivingEntity {

	public LayerMask groundCollisionMask;

	public float moveSpeed;
	public float moveDampeningGround;
	public float moveDampeningAir;
	public float jumpVelocity;

	float currentMoveDampening;

	bool slamed = false;

	Rigidbody2D rb;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update()
	{
		Vector3 input = new Vector3 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		float velX = 0;

		if (Grounded ()) {

			velX = Mathf.SmoothDamp (rb.velocity.x, input.x * moveSpeed, ref currentMoveDampening, moveDampeningGround);

			if (Input.GetKeyDown (KeyCode.Space) || input.y == 1)
				rb.velocity = new Vector2 (rb.velocity.x, jumpVelocity);
			
			slamed = false;

		} else {
			
			velX = Mathf.SmoothDamp (rb.velocity.x, input.x * moveSpeed, ref currentMoveDampening, moveDampeningAir);

			if (input.y == -1 && !slamed) {
				rb.velocity = new Vector2 (rb.velocity.x, -jumpVelocity);
				slamed = true;
			}
		}

		rb.velocity = new Vector2 (velX, rb.velocity.y);
	}

	bool Grounded()
	{
		if (Physics2D.Raycast (Position2D - Up2D * ((transform.localScale.y - .015f) / 2f), -transform.up, .05f, groundCollisionMask))
			return true;
		else if (Physics2D.Raycast (Position2D - Up2D * ((transform.localScale.y - .015f) / 2f), -transform.up, .05f, groundCollisionMask))
			return true;

		return false;
	}
}
