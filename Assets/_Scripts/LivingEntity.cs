using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : Entity {

	public float startHealth;
	float currentHealth;

	public virtual void Start()
	{
		currentHealth = startHealth;
	}

	public virtual void Hit (float damage)
	{
		currentHealth -= damage;

		if (currentHealth <= 0) {

			Die ();
		}
	}

	public virtual void Die()
	{

	}
}
