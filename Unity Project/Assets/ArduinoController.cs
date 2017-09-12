using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ArduinoConnector))]
public class ArduinoController : MonoBehaviour {
	public PlayerController player;
	ArduinoConnector ardC;
	GameController gc;
	bool disabledForward = false;
	bool disabledBackward = false;

	void Awake() {
		ardC = this.GetComponent<ArduinoConnector> ();
		gc = GameObject.FindGameObjectWithTag ("Controller").GetComponent<GameController> ();
	}
		
	void Update () {
		ardC.Open ();
		if (gc.won || gc.lost) {
			ardC.WriteToArduino ("w");
		} else {
			ardC.WriteToArduino ("t");
			if (!player.canMoveForward && !disabledForward) {
				disabledForward = true;
				ardC.WriteToArduino ("x");
			}

			if (disabledForward && player.canMoveForward) {
				ardC.WriteToArduino ("y");
				disabledForward = false;
			}

			if (!player.canMoveBackward && !disabledBackward) {
				disabledBackward = true;
				ardC.WriteToArduino ("p");
			}

			if (disabledBackward && player.canMoveBackward) {
				ardC.WriteToArduino ("k");
				disabledBackward = false;
			}			
		}

		StartCoroutine (ardC.AsynchronousReadFromArduino ((string s) => player.HandleInstruction(s), null, 10f));
		ardC.Close ();		
	}
}
