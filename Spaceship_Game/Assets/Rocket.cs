using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}

    private void ProcessInput() {
        if (Input.GetKey(KeyCode.Space)) {
            print("The space button has been pressed");
        }
        else if (Input.GetKey("a")) {
            print("the player is rotating left");
        }
        else if (Input.GetKey("d")) {
            print("the player is rotating right");
        }
    }
}
