using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

	void OnDrawGizmos () {
		Gizmos.color = Color.blue;
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.DrawWireCube (Vector3.zero + Vector3.up * 1, Vector3.one + Vector3.up * 1);
	}
}
