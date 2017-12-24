using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
    
    [SerializeField] float rcsThrust = 250f;
    [SerializeField] float mainThrust = 25f;

    Rigidbody rigidBody;
	AudioSource audioSource;

    enum State { Alive, Transcending, Dead };
    State state = State.Alive;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        // TODO: Stop sound on death
        if (state != State.Dead) {
			Boosting();
			Rotate();
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (state != State.Alive) {
            return;
        }

        switch (collision.gameObject.tag) {
            case "Friendly":
                print("we are chillin");
                break;
            case "Finish":
                print("BALLLLLINNNN");
                state = State.Transcending;
                Invoke("LoadNextScene", 1f);
                break;
            default:
                print("You ded son");
                state = State.Dead;
                Invoke("LoadOnDeath", 1f);
                break;
        }
    }

    private void LoadNextScene() {
		SceneManager.LoadScene(1);
    }

    private void LoadOnDeath() {
        SceneManager.LoadScene(0);
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
