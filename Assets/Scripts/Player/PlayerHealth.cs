using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Destructable {

	[SerializeField] Ragdoll ragdoll;

	public override void Die ()
	{
		base.Die ();
		ragdoll.EnableRagdoll (true);
	}

}
