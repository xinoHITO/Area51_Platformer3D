using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKey : MonoBehaviour {
	private Transform _player;

	private bool _isPlayerInRange;

	// Use this for initialization
	void Start () {
		//FindGameObjectWithTag te permite encontrar un GameObject en base a su etiqueta o tag
		_player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void Update(){
		if (_isPlayerInRange && Input.GetKeyDown(KeyCode.E)) {
			//hacemos que la llave se convierta en hija del player
			transform.parent = _player;
			transform.position = _player.position + new Vector3 (0, 2.5f, 0);
			//apagamos el trigger de la llave
			GetComponent<Collider> ().enabled = false;
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			_isPlayerInRange = true;
		}
	}
	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			_isPlayerInRange = false;
		}
	}
}
