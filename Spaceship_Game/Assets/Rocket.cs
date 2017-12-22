using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    AudioSource audioSource;
    Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}

    private void ProcessInput() {
        audioSource.Stop();
        if (Input.GetKey(KeyCode.Space)) {
            rigidBody.AddRelativeForce(Vector3.up);
            audioSource.Play();
        }
        if (Input.GetKey("a") || Input.GetKey("d")) {         
			if (Input.GetKey("a")) {
                transform.Rotate(Vector3.forward);
			}
			else if (Input.GetKey("d")) {
                transform.Rotate(Vector3.back);
			}
        }
    }
}
