using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class Bullet : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter (Collider col) {
		if (!isServer) {
			return;
		}
		col.GetComponent<HP> ().TakeDamage (20);
		Destroy (gameObject);
	}
}
