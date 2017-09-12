using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotateBot : MonoBehaviour {
	public int speed;
	public float friction; 
	public float lerpSpeed; 
	public float xDeg;
	public float yDeg; 
	Quaternion fromRotation; 
 	Quaternion toRotation;

	void Update () {
		if(Input.GetKey(KeyCode.Space)) {
			RotateTransform(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); 
		} else {
			RotateTransform(0f, 0f); 
		}	
	}

	void RotateTransform(float xNum, float yNum) { 
		xDeg -= xNum * speed * friction; 
		fromRotation = transform.rotation; 
		toRotation = Quaternion.Euler(0,xDeg,0); 
		transform.rotation = Quaternion.Lerp(fromRotation,toRotation,Time.deltaTime * lerpSpeed); 

	}

}



