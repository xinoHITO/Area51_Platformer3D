using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGameObjectToTarget : MonoBehaviour {
	public Transform _object;
	public Transform _targetPoint;

	public Transform _lookAtTarget;

	private bool _startLerp;

	// Use this for initialization
	void Start () {
		
	}

	void Update(){

		if (_startLerp) {
			
			_object.position = Vector3.Lerp (_object.position, _targetPoint.position, Time.deltaTime * 5);

			//chequeamos si nuestra variable _lookAtTarget es nula
			if (_lookAtTarget != null) {
				Quaternion targetRotation = Quaternion.LookRotation (_lookAtTarget.position - _object.position);
				_object.rotation = Quaternion.Lerp (_object.rotation, targetRotation, Time.deltaTime * 8);
			}

			float distance = Vector3.Distance (_object.position, _targetPoint.position);
			if (distance < 0.1f) {
				_startLerp = false;
				this.enabled = false;
			}
		}

	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			_startLerp = true;
			GetComponent<Collider> ().enabled = false;
		}
	}
}
