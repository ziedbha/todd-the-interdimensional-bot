using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour {
	public float speed;
	public float movement;
	private bool reachedInitial = true;
	Vector3 initialPos;

	// Use this for initialization
	void Start () {
		initialPos = this.transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.position.x < initialPos.x) {
			reachedInitial = true;
		}
		if (this.transform.position.x < initialPos.x + movement & reachedInitial) {
			this.transform.position = this.transform.position + new Vector3 (1, 0, 0) * Time.deltaTime * speed;
		} else if (this.transform.position.x > initialPos.x) {
			reachedInitial = false;
			this.transform.position = this.transform.position - new Vector3 (1, 0, 0) * Time.deltaTime * speed;
		}

		
	}
}
