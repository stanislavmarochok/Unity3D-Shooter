using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour {

	void OnTriggerEnter (Collider collider) {
		if (collider.tag != "Player") {
			return;
		}

		PickUp (collider.transform);

	}

	public virtual void OnPickup (Transform item ) {
		print ("test");
	}

	void PickUp (Transform item) {
		OnPickup (item);
	}
}
