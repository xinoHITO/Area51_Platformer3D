using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWhenEnemiesAreDead : MonoBehaviour {
	
	public GameObject _enemy;
	public Animator _animator;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (_enemy == null) {
			_animator.SetTrigger("open");
			this.enabled = false;
		}
	}
}
