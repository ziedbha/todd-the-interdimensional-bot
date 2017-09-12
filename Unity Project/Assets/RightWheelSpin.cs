using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightWheelSpin : MonoBehaviour {

	public void Update() {
		transform.Rotate(Time.deltaTime * -90, 0, 0);
	}
}
