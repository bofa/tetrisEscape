using UnityEngine;
using System.Collections;

public class cameraScript : MonoBehaviour {

	enum State{Rotate, Cue}

	State state = new State();

	float angle = 0;
	//Vector3 centerOfTable = new Vector3(-5,-4.5f,0.74f);
	Vector3 centerOfTable = new Vector3(0,0,0);
	Vector3 cameraPosition = new Vector3(1.9f,1.0f,1.9f);
	Vector3 outerPosition = new Vector3();


	// Use this for initialization
	void Start () {

		state = State.Rotate;

		Camera.mainCamera.transform.position = new Vector3(cameraPosition.x*Mathf.Sin(angle),cameraPosition.y,cameraPosition.z*Mathf.Cos(angle));
		Camera.mainCamera.transform.LookAt(centerOfTable);
	}
	
	// Update is called once per frame
	void Update () {

		switch(state)
		{
		case State.Rotate:
			{
			stateRotate ();
			break;
			}
		case State.Cue:
			{
			stateCue();
			break;
			}
			break;
		}
	}


	void stateRotate(){

		foreach (Touch touch in Input.touches) {

			Debug.Log(touch.tapCount.ToString());

			// State transition
			if(touch.tapCount>1){
				stateRotateTransition(touch);
				return;
			}

			angle += 0.01f*touch.deltaPosition.x;

			Camera.mainCamera.transform.position = new Vector3(cameraPosition.x*Mathf.Sin(angle),cameraPosition.y,cameraPosition.z*Mathf.Cos(angle));
			outerPosition = Camera.mainCamera.transform.position;

			Camera.mainCamera.transform.LookAt(centerOfTable);
		}

	}

	void stateRotateTransition(Touch touch){

		state = State.Cue;

		Vector3 cPos = Camera.mainCamera.transform.position;
		Vector3 lookAt = Camera.mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x,touch.position.y,camera.nearClipPlane));

		/*
		Ray ray = Camera.mainCamera.ScreenPointToRay (touch.position);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			float distance = hit.distance;
			Debug.Log(distance.ToString());
			Camera.mainCamera.transform.LookAt(ray.GetPoint(distance));
		}
		*/

		Vector3 wbPos = GameObject.Find("White_ball").transform.position;

		Camera.mainCamera.transform.LookAt(wbPos);
		Camera.mainCamera.transform.position = cPos + 0.8f*(wbPos - cPos);
		// Camera.mainCamera.transform.LookAt(lookAt);


	}

	void stateCue(){

		foreach (Touch touch in Input.touches) {

			Vector3 near = Camera.mainCamera.ScreenToWorldPoint (new Vector3(touch.position.x, touch.position.y, Camera.mainCamera.nearClipPlane));
			Vector3 far =  Camera.mainCamera.ScreenToWorldPoint (new Vector3(touch.position.x, touch.position.y, Camera.mainCamera.farClipPlane));

			Transform cue = GameObject.Find("cue").transform;

			cue.position = near + 0.0001f*(far-near); 

			Vector3 wbPos = GameObject.Find("White_ball").transform.position;

			cue.rotation = Quaternion.Euler(new Vector3(0,0,95)) * Quaternion.LookRotation(wbPos - Camera.mainCamera.transform.position);

			//Debugg
			//stateRotateTransition(touch);

			/*
			Debug.Log(touch.tapCount.ToString());
			
			// State transition
			if(touch.tapCount>1){
				stateCueTransition();
				return;
			}
			*/

			//Vector3 lookAt = Camera.mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x,touch.position.y,camera.nearClipPlane));

			//Camera.mainCamera.transform.LookAt(lookAt);
		
			break;

		}

	}

	void stateCueTransition(){


	}

}
