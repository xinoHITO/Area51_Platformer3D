using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {
	public float _damage = 20;
	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Enemy") {
			other.GetComponent<Health> ()._health -= _damage;
		}
	}
}
