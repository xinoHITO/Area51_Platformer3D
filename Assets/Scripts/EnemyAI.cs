using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
	public float _weight = 10;


	protected Vector3 _impact;
	protected Rigidbody _rigidbody;

	protected Health _healthScript;
	// Use this for initialization
	protected virtual void Start () {
		_healthScript = GetComponent<Health> ();
		_rigidbody = GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	protected virtual void Update () {
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
				_rigidbody.isKinematic = true;
			}
		}
	}

	//attacker representa el objeto que te golpeo y force la intensidad con la
	//que fuiste golpeado
	public void Knockback(Transform attacker,float force){
		Vector3 dir = transform.position - attacker.position;
		dir.y = 0;
		dir.Normalize ();
		_impact = dir * force;
		_rigidbody.isKinematic = false;
	}

	public bool IsHurt(){
		//si estas en knockback
		//o muerto... esto retorna true
		if (_impact.magnitude > 0.1f || _healthScript._health <= 0) {
			return true;
		} else {
			return false;
		}
	}
}
