using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
	public float _walkSpeed = 2;
	public float _runSpeed = 5;
	public float _crouchSpeed = 1;
	public float _ceilingCheck = 1;
	private Animator _animator;
	private Rigidbody _rigidbody;
	private CapsuleCollider _collider;

	//esta variable indica si te puedes parar despues de haberte agachado
	//dependiendo si hay espacio encima de ti para que te puedas parar
	private bool _canStandUp;

	private float _v;
	private float _h;
	private bool _pressedShift;
	private bool _pressedControl;
	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator> ();
		_rigidbody = GetComponent<Rigidbody> ();
		_collider = GetComponent<CapsuleCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
		_v = Input.GetAxis ("Vertical");
		_h = Input.GetAxis("Horizontal");
		_pressedShift = Input.GetKey (KeyCode.LeftShift);
		_pressedControl = Input.GetKey (KeyCode.LeftControl);


		Crouch ();

		ManageAnimations ();

	}

	void FixedUpdate(){
		
		GroundMovement ();
			
	
		//si estoy agachado...
		if (_collider.height < 1.8f) {
			//lanzamos un raycast hacia arriba para saber si hay un obstaculo
			bool hit = Physics.Raycast (transform.position, Vector3.up, _ceilingCheck);
			_canStandUp = !hit;
			if (hit) {
				//dibujamos el raycast
				//en Debug.DrawRay la longitud del rayo se aplica multiplicando 
				//la direccion por la longitud que quieres dar al rayo
				Debug.DrawRay (transform.position, Vector3.up * _ceilingCheck, Color.green);
			} else {
				Debug.DrawRay (transform.position, Vector3.up*_ceilingCheck, Color.red);
			}

		}
	}

	void GroundMovement(){
		Vector3 moveVector = new Vector3 (_h, 0, _v);
		moveVector.Normalize ();
		if (_collider.height < 1.8f) {
			moveVector *= _crouchSpeed;
		} else {
			if (_pressedShift) {
				moveVector *= _walkSpeed;
			} else {
				moveVector *= _runSpeed;
			}
		}
		_rigidbody.velocity = moveVector;

		transform.LookAt (transform.position + moveVector);
	}

	void ManageAnimations(){
		float speed = Mathf.Abs (_h) + Mathf.Abs (_v);
		if (speed > 1) {
			speed = 1;
		}
		if (_pressedShift) {
			_animator.SetFloat ("speed", speed);
		} else {
			_animator.SetFloat ("speed", speed*2);
		}

		if (_pressedControl) {
			_animator.SetBool ("crouch", true);
		} else {
			if (_canStandUp) {
				_animator.SetBool ("crouch", false);
			}

		}
	}

	void Crouch(){
		if (_pressedControl) {
			_collider.height = 0.9f;
			Vector3 newCenter = new Vector3 (0, 0.45f, 0);
			_collider.center = newCenter;
		} else {
			if (_canStandUp) {
				_collider.height = 1.8f;
				Vector3 newCenter = new Vector3 (0, 0.9f, 0);
				_collider.center = newCenter;	
			}
		}
	}
}
