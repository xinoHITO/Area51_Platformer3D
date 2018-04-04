using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawn : NetworkBehaviour {

	public GameObject _enemyPrefab;
	public int _enemyCount = 5;
	public float _spawnRange = 5;
	// Use this for initialization
	void Start () {
		

	}

	//esta funcion inicia cuando el server de esta partida se inicia
	public override void OnStartServer(){
		for (int i = 0; i < _enemyCount; i++) {
			Vector3 pos = transform.position + (Random.insideUnitSphere*_spawnRange);
			pos.y = 0;
			GameObject newEnemy = Instantiate (_enemyPrefab, pos, Quaternion.identity);
			NetworkServer.Spawn (newEnemy);	
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
