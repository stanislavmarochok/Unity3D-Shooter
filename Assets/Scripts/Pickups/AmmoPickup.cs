using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : PickupItem {

	[SerializeField] EWeaponType weaponType;
	[SerializeField] float respawnTime;
	[SerializeField] int amount;

	public void Start ()
	{
		GameManager.Instance.EventBus.AddListener ("EnemyDeath", new EventBus.EventListener()
			{
				Method = () =>
				{
					print ("Enemy Death Listener: " + transform.name);
				}
			});
	}

	public override void OnPickup (Transform item) {

		Container playerInventory = GameObject.Find("Inventory").GetComponentInChildren<Container> ();
		GameManager.Instance.Respawner.Despawn (gameObject, respawnTime);
		playerInventory.Put (weaponType.ToString(), amount);

		item.GetComponent<Player>().PlayerShoot.ActiveWeapon.reloader.HandleOnAmmoChanged ();
	}
}
