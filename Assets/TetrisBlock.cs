using UnityEngine;
using System.Collections;

public class TetrisBlock : MonoBehaviour {
	
	public enum BlockState {empty, falling, gridded};
	public BlockState blockState = BlockState.empty;
	//GameObject blockObj;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		// Check state and update accordingly
		switch (blockState) {
		case BlockState.empty:
			break;
		case BlockState.falling:
			transform.position = transform.position + new Vector3 (0, -0.05f, 0);
			break;
		case BlockState.gridded:
			break;
		}
	}

	public void setPosition (Vector3 position) {
		transform.position = position;
	}

	// Sets all necessary things to start falling
	public void startFalling () {
		Debug.Log ("Anropat starFalling");
		blockState = BlockState.falling;
		gameObject.SetActive (true);
	}

	public void setGridded (Vector3 position) {
		blockState = BlockState.gridded;
		// Only lock the y position, x & z are fine
		transform.position = new Vector3(transform.position.x, position.y, transform.position.z);
	}
}
