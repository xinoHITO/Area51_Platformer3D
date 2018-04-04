using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class HP : NetworkBehaviour {
	[SyncVar (hook="UpdateHealthBar")]
	public int health = 100;
	public int maxHealth =100;
	public bool destroyOnDeath = false;
	public Image _hpBar;

	private NetworkTransform _networkTransform;
	private NetworkStartPosition[] spawnPoints;
	// Use this for initialization
	void Start () {
		_networkTransform = GetComponent<NetworkTransform> ();
		spawnPoints = FindObjectsOfType<NetworkStartPosition> ();
		_hpBar.fillAmount = health*1.0f / maxHealth;
	}

	void Update(){
		
	}



	public void TakeDamage(int damage){
		health -= damage;

		if (health <= 0) {
			if (destroyOnDeath) {
				//como esta funcion solo se llama en el server
				//al hacer esto... tambien se destruira en los clientes
				Destroy (gameObject,0.1f);
			} else {
				RpcRespawn ();
				health = maxHealth;
			}

		}

	}

	[ClientRpc]
	void RpcRespawn(){
		
		if (isLocalPlayer) {
			
			Vector3 spawnPos = Vector3.zero;
			if (spawnPoints.Length > 0) {
				spawnPos = spawnPoints [Random.Range (0, spawnPoints.Length)].transform.position;
			}
			transform.position = spawnPos;	
		} else {
			_networkTransform.interpolateMovement = 0;
			Invoke ("RestoreNetworkTransform",0.5f);
		}
	}

	void RestoreNetworkTransform(){
		_networkTransform.interpolateMovement = 1;
	}

	void UpdateHealthBar(int newHealth){
		_hpBar.fillAmount = newHealth*1.0f / maxHealth;
	}
}
