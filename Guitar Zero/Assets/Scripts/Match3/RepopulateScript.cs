/*
 * 
 * This script puts new tokens into the grid. It is not responsible for what the tokens do thereafter, or for any logic relating to the grid
 * (e.g., what the tokens should be).
 * 
 */

using UnityEngine;
using System.Collections;

public class RepopulateScript : MonoBehaviour {
	
	protected GameManagerScript gameManager;

	public virtual void Start () {
		gameManager = GetComponent<GameManagerScript>();
	}

	/// <summary>
	/// Add tokens at the top of the grid.
	/// </summary>
	public virtual void AddNewTokensToRepopulateGrid(){

		//iterate across the top row of the grid, adding a new token in all empty spaces
		for(int x = 0; x < gameManager.gridWidth; x++){
			GameObject token = gameManager.gridArray[x, gameManager.gridHeight - 1];
			if(token == null){
				gameManager.AddTokenToPosInGrid(x, gameManager.gridHeight - 1, gameManager.grid);
			}
		}
	}
}
