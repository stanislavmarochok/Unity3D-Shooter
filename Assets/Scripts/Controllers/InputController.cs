using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	public float Vertical;
	public float Horizontal;
	public Vector2 MouseInput;
	public bool Fire1;
	public bool Fire2;
	public bool Reload;
	public bool IsWalking;
	public bool IsSprinting;
	public bool IsCrouching;
	public bool IsJump;
	public bool MouseWheelUp;
	public bool MouseWheelDown;


	void Update () {
		Vertical = Input.GetAxis ("Vertical");
		Horizontal = Input.GetAxis ("Horizontal");

		MouseInput = new Vector2 (Input.GetAxis("Mouse X"), Input.GetAxis ("Mouse Y"));
		Fire1 = Input.GetButton ("Fire1");
		Fire2 = Input.GetButton ("Fire2");

		Reload = Input.GetKey (KeyCode.R);
		IsWalking = Input.GetKey (KeyCode.X);
		IsSprinting = Input.GetKey (KeyCode.LeftShift);
		IsCrouching = Input.GetKey (KeyCode.C);
		MouseWheelUp = Input.GetAxis ("Mouse ScrollWheel") > 0;
		MouseWheelDown = Input.GetAxis ("Mouse ScrollWheel") < 0;

	}

}
