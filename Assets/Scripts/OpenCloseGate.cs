using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseGate : MonoBehaviour {
	public bool _open;
	public Animator _gateAnimator;
	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			if (_open) {
				_gateAnimator.SetTrigger ("open");
			} else {
				_gateAnimator.SetTrigger ("close");
			}
		}
	}
}
