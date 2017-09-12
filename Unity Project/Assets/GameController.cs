using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	//public GUIText scoreText;
	public Text scoreText;
	public Text lostText;
	public Text wonText;
	public Text restartText;
	internal bool won;
	internal bool lost;
	internal bool restart;

	private int score;


	// Use this for initialization
	void Start () {
		won = false;
		lost = false;
		restart = false;
		scoreText.text = "Score: 0";
		restartText.text = "";
		wonText.text = "";
		lostText.text = "";
		score = 0;
		UpdateScore ();
	}

	void Update() {
		if(restart) {
			if (Input.GetKeyDown(KeyCode.R)) {
				SceneManager.LoadScene ("testScene");
			}
		}
		if (score == 6) {
			Won ();
		}
	}
		
	public void AddScore(int newScoreValue) {
		score += newScoreValue;
		UpdateScore();
	}

	public void Won() {
		wonText.text = "You won! :)";
		won = true;
		Restart ();
	}

	public void Lost() {
		lostText.text = "You lost! :(";
		lost = true;
		Restart ();
	}

	void Restart() {
		this.restart = true;
		restartText.text = "Press R to Restart";
	}

	void UpdateScore() {
		scoreText.text = "Score: " + score.ToString ();
	}
}
