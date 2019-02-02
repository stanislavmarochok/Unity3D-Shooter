using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {


	[System.Serializable]
	public class CameraRig {
		public Vector3 CameraOffset;
		public float Damping;
		public float CrouchHeight;
	}


	[SerializeField] 
	CameraRig defaultCamera;

	[SerializeField] 
	CameraRig aimCamera;

	Transform cameraLookTarget;
	Player localPlayer;

	// Use this for initialization
	void Awake () {
		GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined;
	}

	void HandleOnLocalPlayerJoined (Player player) {
		localPlayer = player;
		cameraLookTarget = localPlayer.transform.Find ("AimingPivot");

		if (cameraLookTarget == null) {
			cameraLookTarget = localPlayer.transform;
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {

		if (localPlayer == null)
			return;

		CameraRig cameraRig = defaultCamera;

		if (localPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMING || localPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMEDFIRING) {
			cameraRig = aimCamera;
		}

		float targetHeight = localPlayer.PlayerState.MoveState == PlayerState.EMoveState.CROUCHING ? cameraRig.CrouchHeight : 0;

		Vector3 targetPosition = cameraLookTarget.position + 
			localPlayer.transform.forward 	* cameraRig.CameraOffset.z + 
			localPlayer.transform.up 		* (cameraRig.CameraOffset.y + targetHeight) +
			localPlayer.transform.right 	* cameraRig.CameraOffset.x;

		transform.position = Vector3.Lerp (transform.position, targetPosition, cameraRig.Damping * Time.deltaTime);
		transform.rotation = Quaternion.Lerp (transform.rotation, cameraLookTarget.rotation, cameraRig.Damping * Time.deltaTime);

// 		TODO - camera following muzzle rotation to be able to look around and up and down
//		transform.position = Vector3.Lerp (transform.position, transform.position, cameraRig.Damping * Time.deltaTime);
//		transform.rotation = Quaternion.Lerp (transform.rotation, transform.rotation, cameraRig.Damping * Time.deltaTime);
	}
}
