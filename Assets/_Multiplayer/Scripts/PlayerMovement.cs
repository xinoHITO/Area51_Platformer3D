using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerMovement : NetworkBehaviour {
	public float _rotateSpeed = 10;
	public float _walkSpeed = 5;

	public GameObject _bulletPrefab;
	public Transform _shootPoint;

	private Rigidbody _rigidbody;
	// Use this for initialization
	void Start () {
		_rigidbody = GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void Update () {



		if (!isLocalPlayer) {
			return;
		}

		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");


		Quaternion rot = transform.rotation;

		rot = Quaternion.Euler(0, (h * _rotateSpeed * Time.deltaTime) + rot.eulerAngles.y,0);
		_rigidbody.MoveRotation (rot);


		_rigidbody.velocity = transform.forward * v * _walkSpeed;
	

		if (Input.GetMouseButtonDown(0)) {
			CmdFire ();
		}

	}
	//Esta funcion se ejecuta cuando el cliente
	//dueño de este objeto se conecta a la partida
	public override void OnStartLocalPlayer(){
		GetComponent<Renderer> ().material.color = Color.blue;
	}

	[Command]
	void CmdFire(){
		GameObject bullet = Instantiate (_bulletPrefab, _shootPoint.position, Quaternion.identity);
		bullet.GetComponent<Rigidbody> ().velocity = transform.forward * 30;
		NetworkServer.Spawn (bullet);
		Destroy (bullet, 3);
	}
}
