using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TetrisGameLogic : MonoBehaviour {

	static int gridWidth = 10;
	static int gridHeight = 20;
	GameObject[,] blockGrid;
	List<GameObject> blockList = new List<GameObject>();

	GameObject testBlockObj;
	GameObject testBlockObj2;

	void Start () {
		Debug.Log ("Startat start");
		// Initialize and fill blockGrid with empty blocks
		blockGrid = new GameObject[gridWidth, gridHeight];
		for (int x = 0; x < gridWidth; x++) {
			for (int y = 0; y < gridWidth; y++) {
				// Instantiates a lot of empty (invisible) blocks
				blockGrid[x, y] = GameObject.CreatePrimitive (PrimitiveType.Cube);
				blockGrid[x, y].SetActive(false);
			}
		}

		// Set two test blocks
		testBlockObj = GameObject.CreatePrimitive (PrimitiveType.Cube);
		testBlockObj.AddComponent ("TetrisBlock");
		testBlockObj.transform.position = new Vector3 (1, 6.5f, 0);
		testBlockObj.transform.localScale = new Vector3 (1, 1, 1);
		testBlockObj.GetComponent<TetrisBlock>().startFalling ();

		testBlockObj2 = GameObject.CreatePrimitive (PrimitiveType.Cube);
		testBlockObj2.AddComponent ("TetrisBlock");
		testBlockObj2.transform.position = new Vector3 (1, 8.5f, 0);
		testBlockObj2.transform.localScale = new Vector3 (1, 1, 1);
		testBlockObj2.GetComponent<TetrisBlock>().startFalling ();

		blockList.Add (testBlockObj);
		blockList.Add (testBlockObj2);
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject obj in blockList) {
			if (obj.GetComponent<TetrisBlock>().blockState == TetrisBlock.BlockState.falling) {
				int xpos = (int) obj.transform.position.x;
				float ypos = obj.transform.position.y;

				if ((Mathf.RoundToInt(ypos) == 0 || // Is block at bottom row?
				     blockGrid[xpos, Mathf.RoundToInt(ypos) - 1].GetComponent<TetrisBlock>().blockState == TetrisBlock.BlockState.gridded) && // Is there a block directly underneath?
				    (Mathf.RoundToInt(ypos) % 1) < 0.1) // Is block close to grid midpoint?
				{
					obj.GetComponent<TetrisBlock>().setGridded(new Vector3(0, Mathf.Round(ypos), 0));
					blockGrid[xpos, Mathf.RoundToInt(ypos)] = obj;
				}
			}
		}
	}
}

