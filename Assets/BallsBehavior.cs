using UnityEngine;
using System.Collections;

public class BallsBehavior : MonoBehaviour {


	float e = 0.1f;
	float fricDown = 0.3f;
	float fricWall = 0.2f;
	Vector3 touchVector;
	Vector3 vel;

	// Use this for initialization
	void Start () {

		touchVector.Set (0, -0.056f, 0);

		vel.Set(0.01f, 0, 0);
		rigidbody.velocity = new Vector3 (0.5f, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {

		// rigidbody.position = rigidbody.position + vel;
		Vector3 velTouch = rigidbody.velocity + Vector3.Cross(rigidbody.angularVelocity,touchVector);

		rigidbody.AddForceAtPosition (-rigidbody.mass * fricDown * velTouch, touchVector + rigidbody.position);

		/*
		foreach (Touch touch in Input.touches) {

			if(touch.phase == TouchPhase.Began) {

				Vector3 velAdd = new Vector3(touch.position.x/Screen.width - 0.5f,0f,touch.position.y/Screen.height  - 0.5f);
				rigidbody.velocity = rigidbody.velocity + 0.2f*velAdd;
			}

		}
		*/
	}

	void OnCollisionEnter(Collision collision) {

		Debug.Log ("Col!");

		foreach (ContactPoint contact in collision.contacts) {

			if(contact.otherCollider.tag == "ball"){
				//ballCollision(contact);
			}
			else {
				wallCollision(contact);
			}

		}

	}

	public void wallCollision(ContactPoint contact) {
		
		Vector3 n = contact.normal;
		rigidbody.velocity = 0.9f*rigidbody.velocity.magnitude*n;
		return;
	
	}

	public void ballCollision(ContactPoint contact) {

		Vector3 n = contact.normal;
		
		float mass1 = rigidbody.mass;
		float mass2 = contact.otherCollider.attachedRigidbody.mass;
		
		Vector3 v1 = rigidbody.velocity;
		Vector3 v2 = contact.otherCollider.attachedRigidbody.velocity;
		
		Vector3 w1 = rigidbody.angularVelocity;
		Vector3 w2 = contact.otherCollider.attachedRigidbody.angularVelocity;
		
		Vector3 vRel = v2 - v1;
		float vReln = Vector3.Dot(vRel,n);
	
		//Debugg
		float J = vReln * (e + 1) / (1 / mass1 + 1 / mass2);

		rigidbody.velocity = rigidbody.velocity + J/mass1*n;

		return;
	}

	public void ballCollision(Vector3 ballPos, Vector3 v2, Vector3 w2) {

		Vector3 n = rigidbody.position - ballPos;
		n.Normalize ();

		float m1 = rigidbody.mass;
		float m2 = rigidbody.mass; //contact.otherCollider.attachedRigidbody.mass;
		
		Vector3 v1 = rigidbody.velocity;
		// Vector3 v2 = contact.otherCollider.attachedRigidbody.velocity;
		
		Vector3 w1 = rigidbody.angularVelocity;
		//Vector3 w2 = contact.otherCollider.attachedRigidbody.angularVelocity;

		Vector3 vRel = v2 - v1;
		float vReln = Vector3.Dot(vRel,n);

		//Debugg
		float J = vReln * (e + 1) / (1 / m1 + 1 / m2);

		if (Vector3.Dot (n, v1) > 0 && Vector3.Dot (n, v2) < 0) {
			return;
		}

		rigidbody.velocity = rigidbody.velocity + J/m1*n;

		//rigidbody.velocity = new Vector3 (10, 10, 10);

		//Debug.Log("Collision");

		return;
	}


}
