using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

	[SerializeField] float rateOfFire;
	[SerializeField] Projectile projectile;
	[SerializeField] Transform hand;
	[SerializeField] AudioController audioReload;
	[SerializeField] AudioController audioFire;

	public Transform AimTarget;
	public Vector3 AimTargetOffset;

	public WeaponReloader reloader;
	private ParticleSystem muzzleFireParticleSystem;

	float nextFireAllowed;
	Transform muzzle;

	public bool canFire;

	public void Equip () {
		transform.SetParent (hand);
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
	}

	void Awake () {
		muzzle = transform.Find ("Model/Muzzle");
		reloader = GetComponent<WeaponReloader> ();
		muzzleFireParticleSystem = muzzle.GetComponent<ParticleSystem> ();

	}

	public void Reload () {
		if (reloader == null)
			return;

		reloader.Reload ();
		audioReload.Play ();
	}

	void FireEffect () {
		if (muzzleFireParticleSystem == null)
			return;

		muzzleFireParticleSystem.Play ();
	}

	public virtual void Fire () {

		canFire = false;

		if (Time.time < nextFireAllowed)
			return;

		if (reloader != null) {
			if (reloader.IsReloading)
				return;
			
			if (reloader.RoundsRemainingInClip == 0)
				return;

			reloader.TakeFromClip (1);
		}

		nextFireAllowed = Time.time + rateOfFire;

		muzzle.LookAt (AimTarget.position + AimTargetOffset);

		FireEffect ();

		// instantiate the projectile (bullet)
		Instantiate (projectile, muzzle.position, muzzle.rotation);
		audioFire.Play ();
		canFire = true; 
	}
}
