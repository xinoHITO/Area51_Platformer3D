using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	public Transform _target;
	public Vector3 _offset;
	public float _distance = 10;
	public float _rotationSpeed = 30;
	public bool _invertAxis = false;

	public float _moveLerpSpeed = 5;
	public float _wallClipLerp = 5;

	public LayerMask _raycastMask;

	private float _angle = 0;
	private float _currentDistance;

	// Use this for initialization
	void Start () {
		_currentDistance = _distance;
	}
	
	void FixedUpdate () {
		ProtectFromWallClip ();

		FollowTarget ();
	}


	void ProtectFromWallClip(){
		Vector3 targetPos = _target.position + _offset;
		Vector3 rayDirection = transform.position - targetPos;
		rayDirection.Normalize ();
		RaycastHit hitInfo;
		bool hit = Physics.Raycast(targetPos,rayDirection,out hitInfo,_currentDistance,_raycastMask.value);
		if (hit) {
			Debug.Log ("forward:"+hitInfo.collider.name);

			//si hay un obstáculo entre la camara y el player
			if (hitInfo.collider.transform != _target) {
				//la camara se acerca al player
				_currentDistance -= Time.fixedDeltaTime * _wallClipLerp;
				//pero limitamos para que nunca sea negativo
				if (_currentDistance < 0.5f) {
					_currentDistance = 0.5f;
				}
			}
			Debug.DrawRay (targetPos, rayDirection * _currentDistance, Color.green);
		} else {
			Debug.DrawRay (targetPos, rayDirection * ( _currentDistance+1), Color.red);
			bool hitBackwards = Physics.Raycast (targetPos, rayDirection,out hitInfo, (_currentDistance)+1, _raycastMask.value);
			if (!hitBackwards) {
				_currentDistance += Time.fixedDeltaTime * _wallClipLerp;
				if (_currentDistance > _distance) {
					_currentDistance = _distance;
				}
				Debug.DrawRay (targetPos, rayDirection * (_currentDistance+1), Color.magenta);
//				Debug.Log ("NO backwards");

			} else {
				Debug.DrawRay (targetPos, rayDirection * (_currentDistance+1), Color.cyan);
//				Debug.Log ("backwards:" + hitInfo.collider.name);
			}
		}
	}

	void FollowTarget(){
		//esto detecta el movimiento lateral del mouse
		float mouseX = Input.GetAxis ("Mouse X");

		if (_invertAxis) {
			_angle += mouseX * _rotationSpeed * Time.deltaTime;
		} else {
			_angle -= mouseX* _rotationSpeed * Time.deltaTime;
		}

		if (Mathf.Abs(_angle) > 360) {
			_angle /= 360;
		}
		Vector3 targetPos = _target.position + _offset;
		Vector3 direction = Quaternion.Euler(0,_angle,0) * new Vector3 (0, 0, 1);


		Vector3 finalPos = targetPos + (direction*-_currentDistance);
		transform.position = Vector3.Lerp (transform.position, finalPos, Time.deltaTime * _moveLerpSpeed);


		transform.LookAt (targetPos);
	}
}
