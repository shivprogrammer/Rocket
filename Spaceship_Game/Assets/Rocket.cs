using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    
    [SerializeField] float rcsThrust = 250f;
    [SerializeField] float mainThrust = 35f;

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

    void OnCollisionEnter(Collision collision) {
        switch (collision.gameObject.tag) {
            case "Friendly":
                print("we are chillin");
                break;
            case "Finish":
                print("YOU WON");
                break;
            default:
                print("You ded son");
                break;
        }
    }

    private void Boosting() {
        if (Input.GetKey(KeyCode.Space)) {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
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
		float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey("a") || Input.GetKey("d")) {

            if (Input.GetKey("a")) {
                transform.Rotate(Vector3.forward * rotationThisFrame);
			}
			else if (Input.GetKey("d")) {
                transform.Rotate(Vector3.back * rotationThisFrame);
			}
		}
        rigidBody.freezeRotation = false; // the physics engine takesover
	}
}
