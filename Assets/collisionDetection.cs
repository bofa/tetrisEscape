using UnityEngine;
using System.Collections;

public class collisionDetection : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("Collision Detection Start");
	}


	float radius = 0.03f;

	// Update is called once per frame
	void Update () {

		foreach (Transform child in transform) {
			if (child.tag == "ball") {

				foreach (Transform child2 in transform) {

					if (!child.Equals(child2) && child.tag == "ball" && Vector3.Distance (child.position, child2.position) < 2 * radius) {

						BallsBehavior balls1 = child.GetComponent<BallsBehavior>();
						BallsBehavior balls2 = child2.GetComponent<BallsBehavior>();

						ballCollision(balls1.rigidbody,balls2.rigidbody);

					}
				}
			}
		}
	}


	static float e = 0.8f;

	public void ballCollision(Rigidbody ball1, Rigidbody ball2) {
		
		Vector3 n = ball1.position - ball2.position;
		n.Normalize ();
		
		float m1 = ball1.mass;
		float m2 = ball2.mass;
		
		Vector3 v1 = ball1.velocity;
		Vector3 v2 = ball2.velocity;
		
		Vector3 w1 = ball1.angularVelocity;
		Vector3 w2 = ball2.angularVelocity;
		
		Vector3 vRel = v1 - v2;
		float vReln = Vector3.Dot(vRel,n);
		
		//Debugg
		float J = vReln * (e + 1) / (1 / m1 + 1 / m2);

		Debug.Log (v1.ToString());
		Debug.Log (v2.ToString());

		if (Vector3.Dot (n, v1) > 0 && Vector3.Dot (n, v2) < 0) {
			return;
		}
		
		ball1.velocity = ball1.velocity - J/m1*n;
		ball2.velocity = ball2.velocity + J/m1*n;

		ball1.position = ball1.position + 0.0015f * n;
		ball2.position = ball2.position - 0.0015f * n;

		return;
	}

}
