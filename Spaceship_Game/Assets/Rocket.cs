using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
    
    [SerializeField] float rcsThrust = 250f;
    [SerializeField] float mainThrust = 25f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem successParticles;

    Rigidbody rigidBody;
	AudioSource audioSource;
    ParticleSystem particleSystem;

    enum State { Alive, Transcending, Dead };
    State state = State.Alive;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if (state == State.Alive) {
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
                break;
            case "Finish":
                state = State.Transcending;
                audioSource.Stop();
                audioSource.PlayOneShot(success);
                successParticles.Play();
                Invoke("LoadNextScene", 1f);
                break;
            default:
                state = State.Dead;
                audioSource.Stop();
                audioSource.PlayOneShot(death); 
				deathParticles.Play();
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
            mainEngineParticles.Stop();
        }
    }

    private void Thrusting() {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);

        if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
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
