using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AmmoCounter : MonoBehaviour {

	[SerializeField] Text text;

	PlayerShoot playerShoot;
	WeaponReloader reloader;

	void Awake () {
		GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined;
	}

	void HandleOnLocalPlayerJoined (Player player) {

		PlayerShoot _PlayerShoot = player.GetComponent<PlayerShoot>();
		playerShoot = _PlayerShoot;

		playerShoot.OnWeaponSwitch += HandleOnWeaponSwitch; 

	}

	void HandleOnWeaponSwitch (Shooter activeWeapon) {
		reloader = activeWeapon.reloader;
		reloader.OnAmmoChanged += HandleOnAmmoChanged;
		HandleOnAmmoChanged ();
	}

	void HandleOnAmmoChanged () {
		int amountInInventory = reloader.RoundsRemainingInInventory;
		int amountInClip = reloader.RoundsRemainingInClip;
		text.text = string.Format ("{0}/{1}", amountInClip, amountInInventory);
	}

}
