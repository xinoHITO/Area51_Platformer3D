using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedGate : MonoBehaviour {
	public float _distance = 10;
	private Transform _player;
	private Animator _animator;
	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator> ();
		_player = GameObject.FindGameObjectWithTag ("Player").transform;

	}
	
	// Update is called once per frame
	void Update () {
		float distanceToPlayer = Vector3.Distance (transform.position, _player.position);
		if (distanceToPlayer <= _distance) {
			if (Input.GetKeyDown(KeyCode.E)) {
				Transform key = _player.Find("Key");
				//si key NO es nulo ... entonces el player tiene la llave
				if (key != null) {
					//activar la animacion de la puerta
					_animator.SetTrigger("open");
					//destruir la llave
					Destroy(key.gameObject);
					//desactivamos este script para que no se pueda volver a activar la animacion de la puerta
					this.enabled = false;
				}
			}
		} 
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, _distance);
	}
}
