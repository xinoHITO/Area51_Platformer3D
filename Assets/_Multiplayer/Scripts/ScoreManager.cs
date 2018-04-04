using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class ScoreManager : NetworkBehaviour {
	[SyncVar (hook ="UpdateScoreText")]
	private int score = 0;

	public Text scoreText;
	// Use this for initialization
	void Start () {
		
	}

	public override void OnStartServer(){
		UpdateScoreText (0);
		GetComponent<RectTransform>().localPosition = new Vector3 (0, 115.5f, 0);

	}

	void OnConnectedToServer(){
		GetComponent<RectTransform>().localPosition = new Vector3 (0, 115.5f, 0);
	}


	public void GainScore(int gainScore){
		if (!isServer) {
			return;
		}
		score += gainScore;
	}

	void UpdateScoreText(int newScore){
		scoreText.text = "" + newScore;
	}
}
