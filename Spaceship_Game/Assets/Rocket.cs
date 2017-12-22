﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>(); 
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}

    private void ProcessInput() {
        if (Input.GetKey(KeyCode.Space)) {
            rigidBody.AddRelativeForce(Vector3.up);
        }
        if (Input.GetKey("a") || Input.GetKey("d")) {         
			if (Input.GetKey("a")) {
				print("Rotating LEFT");
			}
			else if (Input.GetKey("d")) {
				print("Rotating RIGHT");
			}
        }
    }
}
