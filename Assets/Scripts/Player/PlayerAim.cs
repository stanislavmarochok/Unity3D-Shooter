using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour {

	[SerializeField] float minAngle;
	[SerializeField] float maxAngle;

	public void SetRotation (float amount) 
	{
		float clampedAngle = GetClampedAngle (amount);
		transform.eulerAngles = new Vector3 (clampedAngle - amount, transform.eulerAngles.y, transform.eulerAngles.z);
	}

	private float GetClampedAngle (float amount)
	{
		float newAngle = CheckAngle (transform.eulerAngles.x - amount);
		float clampedAngle = Mathf.Clamp (newAngle, minAngle, maxAngle);
		return clampedAngle;
	}

	public float GetAngle () {
		return CheckAngle (transform.eulerAngles.x);
	}

	public float CheckAngle (float value) {
		float angle = value - 180;

		if (angle > 0)
			return angle - 180;

		return angle + 180;
	}

	void Update () {
	//	print (GetAngle());
	}
}
