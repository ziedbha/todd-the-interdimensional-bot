using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	public GameObject explosion;
	[Header("Movement")]
	public float speed;

	[Header("Rotation")]
	public int speedRot;
	public float friction; 
	public float lerpSpeed; 

	int score;
	float xDeg;
	bool forward;
	internal bool won;
	internal bool lost;
	internal bool restart;
	internal bool canMoveBackward = true;
	internal bool canMoveForward = true;
	string instruction;

	Vector3 toRotation;
	AudioSource audio;
	GameController gc;
	Rigidbody rb;

	void RotateTransform(float xNum) { 
		xDeg = xNum * speedRot * friction; 
		toRotation = new Vector3 (0, xDeg, 0);
		transform.Rotate (toRotation);
	}

	void Start () {
		rb = this.GetComponent<Rigidbody> ();
		audio = this.GetComponent<AudioSource> ();
		gc = GameObject.FindGameObjectWithTag ("Controller").GetComponent<GameController> ();
	}

	void Update () {
		if (gc.won) {
			won = true;
		}

		if (gc.lost) {
			lost = true;
		}

		if (gc.restart) {
			restart = true;
		}

		if (!gc.won) {
			float rotate = 0;
			float move = 0;
			switch (instruction) {
			case "f":
				move = 1;
				break;
			case "b":
				move = -1;
				break;
			case "l":
				rotate = -1;
				break;
			case "r":
				rotate = 1;
				break;
			case "s":
				rotate = 0;
				move = 0;
				break;
			default:
				break;
			}

			if (Mathf.Sign(move) < 0) {
				forward = false;
			} else if (Mathf.Sign(move) > 0) {
				forward = true;
			}
			if (move != 0) {
				Vector3 newpos = rb.position + transform.forward * -speed * Mathf.Sign(move);
				rb.MovePosition (newpos);
			}
			RotateTransform(rotate); 			
		}
	}

	void OnCollisionEnter() {
		if (forward) {
			canMoveForward = false;
		} else {
			canMoveBackward = false;
		}

	}

	void OnCollisionExit() {
		canMoveBackward = true;
		canMoveForward =  true;
	}

	public void HandleInstruction(string instruction) {
		this.instruction = instruction;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Pick Up")) {
			this.audio.Play ();
			Destroy (other.gameObject);
			gc.AddScore(1);
		} else {
			gc.Lost ();
			GameObject.Instantiate (explosion, this.transform.position, this.transform.rotation);
			Destroy (this.gameObject);
		}
	}
}

