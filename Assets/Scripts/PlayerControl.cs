using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
	[Header("MOVEMENT")]
	public bool _canControl = true;
	public float _walkSpeed = 2;
	public float _runSpeed = 5;
	public float _crouchSpeed = 1;
	[Header("FLOOR CHECK")]
	public float _ceilingCheck = 1;
	public float _groundCheck = 0.15f;
	public LayerMask _groundCheckMask;
	[Header("JUMP")]
	public float _gravity = 9.8f;
	public float _jumpForce = 10;
	[Header("ATTACK")]
	public GameObject _swordR;
	public GameObject _swordL;


	private Animator _animator;
	private Rigidbody _rigidbody;
	private CapsuleCollider _collider;
	//esta variable indica si te puedes parar despues de haberte agachado
	//dependiendo si hay espacio encima de ti para que te puedas parar
	private bool _canStandUp;

	private Vector3 _moveVector;
	private float _verticalSpeed;
	private bool _isGrounded;


	private float _v;
	private float _h;
	private bool _pressedShift;
	private bool _pressedControl;
	private bool _pressedSpace;
	private bool _pressedLeftClick;
	// Use this for initialization
	void Start () {
		
		_animator = GetComponent<Animator> ();
		_rigidbody = GetComponent<Rigidbody> ();
		_collider = GetComponent<CapsuleCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
		

		if (_canControl) {
			_v = Input.GetAxis ("Vertical");
			_h = Input.GetAxis("Horizontal");
			_pressedLeftClick = Input.GetMouseButtonDown (0);
			_pressedShift = Input.GetKey (KeyCode.LeftShift);
			_pressedControl = Input.GetKey (KeyCode.LeftControl);
			_pressedSpace = Input.GetKeyDown (KeyCode.Space);
		} else {
			_v = 0;
			_h = 0;
			_pressedLeftClick = false;
			_pressedShift = false;
			_pressedControl = false;
			_pressedSpace = false;
		}


		Crouch ();
		ManageAnimations ();

	}

	void FixedUpdate(){
		
		GroundMovement ();
		VerticalMovement ();
	
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

		_rigidbody.velocity = _moveVector;

	}

	void GroundMovement(){
		_moveVector = (Camera.main.transform.right * _h) + (Camera.main.transform.forward * _v);
		//anulamos el y por si la camara estaba apunatndo en picada
		_moveVector.y = 0;
		_moveVector.Normalize ();
		if (_collider.height < 1.8f) {
			_moveVector *= _crouchSpeed;
		} else {
			if (_pressedShift) {
				_moveVector *= _walkSpeed;
			} else {
				_moveVector *= _runSpeed;
			}
		}

		transform.LookAt (transform.position + _moveVector);
	}

	//maneja la lógica de la gravedad y el salto
	void VerticalMovement(){
		Vector3 origin = transform.position + new Vector3 (0, 0.1f, 0);
		_isGrounded = Physics.Raycast (origin, Vector3.down, _groundCheck,_groundCheckMask);

		if (_isGrounded) {
			//si estas en el piso te dejamos en un valor fijo
			//no se pone cero porque a veces puede pasar que te quedas ligeramente flotando en el aire
			_verticalSpeed = -0.1f;
			if (_pressedSpace) {
				_verticalSpeed = _jumpForce;
			}
		} else {
			//si estas en el aire vamos disminuyendo la velocidad en el eje Y
			_verticalSpeed -= _gravity * Time.fixedDeltaTime;
		}

		_moveVector.y = _verticalSpeed;
	}

	void ManageAnimations(){
		float speed = Mathf.Abs (_h) + Mathf.Abs (_v);
		if (speed > 1) {
			speed = 1;
		}
		if (!_pressedShift) {
			speed *= 2;
		}
		float currentAnimatorSpeed = _animator.GetFloat ("speed");
		speed = Mathf.Lerp (currentAnimatorSpeed, speed, Time.deltaTime * 10);
		_animator.SetFloat ("speed", speed);

		if (_pressedControl) {
			_animator.SetBool ("crouch", true);
		} else {
			if (_canStandUp) {
				_animator.SetBool ("crouch", false);
			}
		}
		float currentAnimVerticalSpeed = _animator.GetFloat ("verticalSpeed");
		float result = Mathf.Lerp (currentAnimVerticalSpeed, _verticalSpeed/3, Time.deltaTime * 2);
		_animator.SetFloat ("verticalSpeed", result);
		_animator.SetBool ("isGrounded", _isGrounded);

		if (_isGrounded) {
			if (_pressedLeftClick) {
				_animator.SetTrigger ("attack");
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

	void ActivateRightHit(){
		_swordL.GetComponent<Collider> ().enabled = false;
		_swordR.GetComponent<Collider> ().enabled = true;
	}

	void ActivateLeftHit(){
		_swordL.GetComponent<Collider> ().enabled = true;
		_swordR.GetComponent<Collider> ().enabled = false;
	}


	void DeactivateHits(){
		_swordL.GetComponent<Collider> ().enabled = false;
		_swordR.GetComponent<Collider> ().enabled = false;
	}
}
