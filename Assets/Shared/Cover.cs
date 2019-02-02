using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour {

	[SerializeField] Collider trigger;

	PlayerCover playerCover;

	private bool CheckLocalPlayer (Collider other)
	{
		if (other.tag != "Player")
			return false;

		// we are not the local player
		if (other.GetComponent<Player> () != GameManager.Instance.LocalPlayer)
			return false;

		playerCover = GameManager.Instance.LocalPlayer.GetComponent<PlayerCover> ();
		return true;
	}

	void OnTriggerEnter (Collider other) 
	{
		if (!CheckLocalPlayer (other))
			return;

		playerCover.SetPlayerCoverAllowed (true);
	}

	void OnTriggerExit (Collider other)
	{
		if (!CheckLocalPlayer (other))
			return;

		playerCover.SetPlayerCoverAllowed (false);
	}
}
