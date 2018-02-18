using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

	public Transform weaponHolster;

	public Weapon startingWeapon;

	Weapon equipedWeapon;

	void Start()
	{
		if (startingWeapon != null)
			EquipWeapon (startingWeapon);
	}

	void EquipWeapon (Weapon equip)
	{
		if (equipedWeapon != null)
			Destroy (equipedWeapon);

		equipedWeapon = Instantiate (equip, weaponHolster.position, weaponHolster.rotation) as Weapon;
		equipedWeapon.transform.parent = weaponHolster;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown (0))
			OnMouseDown ();

		if (Input.GetMouseButton (0))
			OnMouseHold ();

		if (Input.GetMouseButtonUp (0))
			OnMouseRelease ();
	}

	public void OnMouseDown()
	{
		equipedWeapon.OnMouseDown ();
		weaponHolster.GetComponent<RestrictedPosMouseTrack> ().Toggle ();
	}

	public void OnMouseHold()
	{
		equipedWeapon.OnMouseHold ();
	}

	public void OnMouseRelease()
	{
		equipedWeapon.OnMouseRelease ();
		weaponHolster.GetComponent<RestrictedPosMouseTrack> ().Toggle ();
	}
}
