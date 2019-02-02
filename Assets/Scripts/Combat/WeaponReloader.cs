using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReloader : MonoBehaviour {

	[SerializeField] int maxAmmo;
	[SerializeField] float reloadTime;
	[SerializeField] int clipSize;
	[SerializeField] Container inventory;
	[SerializeField] EWeaponType weaponType;

	public int shotsFiredInClip;
	bool isReloading;

	public event System.Action OnAmmoChanged;

	System.Guid containerItemId; 

	public int RoundsRemainingInClip {
		get {
			return clipSize - shotsFiredInClip;
		}
	}

	public int RoundsRemainingInInventory {
		get {
			return inventory.GetAmountRemaining (containerItemId);
		}
	}

	public bool IsReloading {
		get {
			return isReloading;
		}
	}

	void Awake () {

		inventory.OnContainerReady += () => {
			containerItemId = inventory.Add (weaponType.ToString(), maxAmmo);			
		};
	
	}

	public void Reload () {
		if (isReloading)
			return;

		print ("Reload started!");

		isReloading = true;	

		GameManager.Instance.Timer.Add (() => {
			ExecuteReload (inventory.TakeFromContainer (containerItemId, clipSize - RoundsRemainingInClip));
			}, 	
			reloadTime);
	}

	private void ExecuteReload (int amount) {

		isReloading = false;

		shotsFiredInClip -= amount;

		HandleOnAmmoChanged ();
	}

	public void TakeFromClip (int amount) {
		
		shotsFiredInClip += amount;
		HandleOnAmmoChanged ();
	}

	public int getClipSize () {
		return clipSize;
	}

	public void HandleOnAmmoChanged () {
			if (OnAmmoChanged != null)
			OnAmmoChanged ();
	}

}
