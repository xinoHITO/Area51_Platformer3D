using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScore : MonoBehaviour {
	public int score = 100;
	private HP _hpScript;
	private ScoreManager _scoreManager;
	// Use this for initialization
	void Start () {
		_scoreManager = FindObjectOfType<ScoreManager> ();
		_hpScript = GetComponent<HP> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_hpScript.health <= 0) {
			Debug.Log ("gained score");
			_scoreManager.GainScore (score);
			this.enabled = false;
		}
	}
}
