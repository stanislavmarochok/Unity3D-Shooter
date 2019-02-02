using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCover : MonoBehaviour {

	private bool canTakeCover;
	private bool isInCover;
	private RaycastHit closestHit;

	[SerializeField] int numberOfRays;
	[SerializeField] LayerMask coverMask;



	internal void SetPlayerCoverAllowed (bool value)
	{
		canTakeCover = value;
	}

	void Update ()
	{
		if (!canTakeCover)
			return;

		if (Input.GetKeyDown (KeyCode.F)) 
		{
			FindCoverAroundPlayer ();

			if (closestHit.distance == 0)
				return;

			transform.rotation = Quaternion.LookRotation (closestHit.normal) * Quaternion.Euler (0, 180f, 0);

		}
	}

	private void FindCoverAroundPlayer ()
	{
		closestHit = new RaycastHit ();
		float angleStep = 360 / numberOfRays;
		for (int i = 0; i < numberOfRays; i++) {
			Quaternion angle = Quaternion.AngleAxis (i * angleStep, transform.up);
			CheckClosestPoint (angle);
		}
	}

   	private void CheckClosestPoint (Quaternion angle)
	{
		RaycastHit hit;
		if (Physics.Raycast (transform.position + Vector3.up * .3f, angle * Vector3.forward, out hit, 5f, coverMask)) {
			if (closestHit.distance == 0 || hit.distance < closestHit.distance)
				closestHit = hit;
		}
	}
}
