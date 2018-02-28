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
	public float _rotateLerpSpeed = 5;

	private float _angle = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	void LateUpdate () {
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
		Vector3 finalPos = targetPos + (direction*-_distance);
		transform.position = Vector3.Lerp (transform.position, finalPos, Time.deltaTime * _moveLerpSpeed);
		 
		Quaternion finalRotation = Quaternion.LookRotation (targetPos - transform.position);
		transform.rotation = Quaternion.Lerp (transform.rotation, finalRotation, Time.deltaTime * _rotateLerpSpeed);
	//	transform.LookAt (targetPos);
	}
}
