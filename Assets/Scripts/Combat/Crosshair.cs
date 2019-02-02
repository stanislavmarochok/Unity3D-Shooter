using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {

	[SerializeField] Texture2D image;
	[SerializeField] int size;
	[SerializeField] float maxAngle;
	[SerializeField] float minAngle;

	float lookHeight;

	public void LookHeight (float value) {
		lookHeight += value;

		if (lookHeight > maxAngle || lookHeight < minAngle) {
			lookHeight -= value;
		}
	}

	void OnGUI () {

		if (GameManager.Instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMING ||
			GameManager.Instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMEDFIRING) { // show the crosshair only if aiming

			Vector3 screenPosition = Camera.main.WorldToScreenPoint (transform.position);
			screenPosition.y = Screen.height - screenPosition.y;
			GUI.DrawTexture (new Rect (screenPosition.x - size / 2, screenPosition.y - size / 2, size, size), image);

		}
	}
}
