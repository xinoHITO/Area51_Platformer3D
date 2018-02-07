using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
	public float _walkSpeed = 2;
	public float _runSpeed = 5;
	private Animator _animator;
	private Rigidbody _rigidbody;
	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator> ();
		_rigidbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		float v = Input.GetAxis ("Vertical");

		Vector3 moveVector = new Vector3 (0, 0, v);

	
		if (Input.GetKey (KeyCode.LeftShift)) {
			moveVector *= _walkSpeed;
		} else {
			moveVector *= _runSpeed;
		}

		_rigidbody.velocity = moveVector;

		transform.LookAt (transform.position + moveVector);


		if (Input.GetKey (KeyCode.LeftShift)) {
			_animator.SetFloat ("speed", Mathf.Abs(v));
		} else {
			_animator.SetFloat ("speed", Mathf.Abs(v)*2);
		}
	}
}
