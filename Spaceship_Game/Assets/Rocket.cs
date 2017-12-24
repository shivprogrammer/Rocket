using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
    
    [SerializeField] float rcsThrust = 250f;
    [SerializeField] float mainThrust = 25f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip death;

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
			RespondToThrustInput();
			RespondToRotateInput();
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
                audioSource.PlayOneShot(death); 
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

    private void RespondToThrustInput() {
        if (Input.GetKey(KeyCode.Space)) {
            Thrusting();
        }
        else {
            audioSource.Stop();
        }
    }

    private void Thrusting() {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);

        if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(mainEngine);
        }
    }

	private void RespondToRotateInput() {
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
