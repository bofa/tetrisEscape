using UnityEngine;
using System.Collections;

public class ballInputController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.UpArrow) && touching) {
			//Debug.Log("up");
			rigidbody.AddForce (0, 400, 0);
			touching = false;
		}

		if (Input.GetKey (KeyCode.Space)) {
			//Debug.Log("space");
			rigidbody.AddForce (0, 1, 0);
		}

		if (Input.GetKey ("left")) {
			//Debug.Log("left");
			rigidbody.AddForce (-10, 0, 0);
		}

		if (Input.GetKey ("right")) {
			//Debug.Log("right");
			rigidbody.AddForce (10, 0, 0);
		}

		// Handel time between contacts
		if (Time.time - contactTime > deltaTime)
			touching = false;


	}

	bool touching = false;
	float contactTime = 0;
	float deltaTime = 3.2f;

	/*
	void FixedUpdate(){
		touching = false;
	}
	*/

	bool gameOver = false;
	
	void OnCollisionEnter(Collision col) {

		//bool touching = true;

		foreach (ContactPoint contact in col.contacts) {

			if( Vector3.Dot(contact.normal, new Vector3(0,1,0)) > 0.1 ){
				Debug.Log("Hopp");
				touching = true;
				contactTime = Time.time;
			}

			if( Vector3.Dot(contact.normal, new Vector3(0,1,0)) < -0.75 ){ 
				GUI.Label( new Rect(0,0, 100, 20), "Game Over!");
				Debug.Log("Game Over!");
				gameOver = true;
			}

		}

	}

	void OnGUI() {

		if (gameOver) {
			GUI.Label (new Rect (200, 400, 400, 200), "Game Over!");
			Application.LoadLevel("GameOver");
		}

	}

}
