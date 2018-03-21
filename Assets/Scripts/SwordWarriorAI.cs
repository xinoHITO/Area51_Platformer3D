using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWarriorAI : EnemyAI {

	private Renderer _renderer;
	// Use this for initialization
	protected override void Start () {
		base.Start ();
		_renderer = GetComponentInChildren<Renderer> ();

	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
		if (_healthScript._health <= 0) {
			Destroy (gameObject);
		}
	}
		
}
