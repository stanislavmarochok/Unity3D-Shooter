using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {

	[SerializeField] AudioClip[] clips;
	[SerializeField] float delayBetweenClips;

	bool canPlay;
	AudioSource source;

	void Start () {
		source = GetComponent<AudioSource> ();
		canPlay = true; 
	}
	
	public void Play () {

		if (!canPlay)
			return;

		GameManager.Instance.Timer.Add (() => {
			canPlay = true;
		}, delayBetweenClips);

		canPlay = false;

		int clipIndex = Random.Range (0, clips.Length);
		AudioClip clip = clips[clipIndex];
		source.PlayOneShot (clip);
	}
}
