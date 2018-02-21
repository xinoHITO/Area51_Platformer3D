using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
	private Health _healthScript;
	// Use this for initialization
	void Start () {
		_healthScript = GetComponent<Health> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_healthScript._health <= 0) {
			Destroy (gameObject);
		}
	}
}
