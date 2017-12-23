﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
	AudioSource audioSource;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		Boosting();
        Rotate();
    }

    private void Boosting() {
        if (Input.GetKey(KeyCode.Space)) {
            rigidBody.AddRelativeForce(Vector3.up);
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
        }
        else {
            audioSource.Stop();
        }
    }

	private void Rotate() {
        rigidBody.freezeRotation = true;
		if (Input.GetKey("a") || Input.GetKey("d")) {
			if (Input.GetKey("a")) {
				transform.Rotate(Vector3.forward);
			}
			else if (Input.GetKey("d")) {
				transform.Rotate(Vector3.back);
			}
		}
        rigidBody.freezeRotation = false; // the physics engine takesover
	}
}
