using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class HP : NetworkBehaviour {
	[SyncVar]
	public int health = 100;
	// Use this for initialization
	void Start () {
		
	}

	void Update(){
		
	}

	public void TakeDamage(int damage){
		health -= damage;
		if (health <= 0) {
			Destroy (gameObject);
		}
	}
}
