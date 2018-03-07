using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
	public float _weight = 10;


	private Vector3 _impact;

	private Health _healthScript;
	private Rigidbody _rigidbody;
	private Renderer _renderer;
	// Use this for initialization
	void Start () {
		_healthScript = GetComponent<Health> ();
		_rigidbody = GetComponent<Rigidbody> ();
		_renderer = GetComponentInChildren<Renderer> ();

		_rigidbody.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (_impact.magnitude > 0.1f) {
			_rigidbody.velocity = _impact;
			///partimos el vector en 2 partes... su direccion y su magnitud
			Vector3 impactDir = _impact.normalized;
			float impactMagnitude = _impact.magnitude;
			//reducimos un poco la magnitud
			impactMagnitude -= Time.deltaTime * _weight;
			//volvemos a juntarlos para formar el vector de impacto
			_impact = impactDir * impactMagnitude;
			if (impactMagnitude < 0.1f) {
				_impact = Vector3.zero;
				_renderer.material.color = Color.white;
				_rigidbody.isKinematic = true;
			}
		}

		if (_healthScript._health <= 0) {
			Destroy (gameObject);
		}
	}

	//attacker representa el objeto que te golpeo y force la intensidad con la
	//que fuiste golpeado
	public void Knockback(Transform attacker,float force){
		Vector3 dir = transform.position - attacker.position;
		dir.y = 0;
		dir.Normalize ();
		_impact = dir * force;
		_renderer.material.color = Color.red;
		_rigidbody.isKinematic = false;
	}
}
