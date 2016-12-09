using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour {

	//width and height of the token grid, in tokens
	public int gridWidth = 8;
	public int gridHeight = 8;


	//used to reposition the tokens visually
	public float tokenSize = 1.0f;
	public float xOffset = 0.5f;
	public float yOffset = 0.0f;


	//other scripts fundamental to the game's operation
	protected MatchManagerScript matchManager;
	protected InputManagerScript inputManager;
	protected RepopulateScript repopulateManager;
	protected MoveTokensScript moveTokenManager;
	protected NumberManager numberManager;

	//parent object for all tokens
	public GameObject grid;

	//array of tokens used for game-state management
	public GameObject[,] gridArray;

	protected UnityEngine.Object[] tokenTypes; //used to load tokens into the game
	protected Sprite[] noteSprites; //used to reassign sprites when there would otherwise be a start-of-game match

	//the sprite used for the strings in the background
	protected GameObject stringGraphic;

	//used to keep the game on the beat
	public int bpm = 125; //inspired by Guns 'n' Roses' Sweet Child o' Mine!
	private float timeBetweenBeats = 0.0f; //to be set in seconds
	public float TimeBetweenBeats{ //getter so that MoveTokensScript can get the values it needs to move on the beat
		get { return timeBetweenBeats; }
	}
	private float timer = 0.0f;


	public virtual void Awake () {
		tokenTypes = (UnityEngine.Object[])Resources.LoadAll("Tokens/");
		noteSprites = Resources.LoadAll<Sprite>("Sprites/Note sprites");
		gridArray = new GameObject[gridWidth, gridHeight];
		matchManager = GetComponent<MatchManagerScript>();
		inputManager = GetComponent<InputManagerScript>();
		repopulateManager = GetComponent<RepopulateScript>();
		moveTokenManager = GetComponent<MoveTokensScript>();
		numberManager = GetComponent<NumberManager>();
		stringGraphic = Resources.Load("String") as GameObject;
		timeBetweenBeats = 60.0f/bpm;
		MakeGrid();
		ChangeGridDuplicates();
	}

	public virtual void Update(){
		timer += Time.deltaTime;

		//each beat, check for matches
		if (!GridHasEmpty() && timer >= timeBetweenBeats){
			if (matchManager.GridHasMatch()){
				matchManager.RemoveMatches();
				numberManager.ScorePoints();
			}
		} else if (!GridHasEmpty()){
			inputManager.SelectToken(); //if not on the beat, allow the player to make selections
		} 

		//if there are empty spaces, tokens need to move
		if (GridHasEmpty()){
			if(!moveTokenManager.move){
				//if the icons are currently moving, set them up to move and leave it be
				moveTokenManager.SetupTokenMove();
			}
			if(!moveTokenManager.MoveTokensToFillEmptySpaces()){
				//if the MoveTokenManager hasn't added any tokens to the grid
				//tell Repopulate Script to add new tokens
				repopulateManager.AddNewTokensToRepopulateGrid();
			}
		}
			
		//keep on the beat
		if (timer >= timeBetweenBeats){
			timer = 0.0f;
		}

		//every frame, check whether the grid is full of tokens.

//		if(!GridHasEmpty()){
//			//if the grid is full of tokens and has matches, remove them.
//			if(matchManager.GridHasMatch()){
//				matchManager.RemoveMatches();
//			} else {
//				//if the grid is full and there are no matches, wait for the player to make a move (and look for it in InputManager)
//				inputManager.SelectToken();
//			}
//
//		} else {
//			if(!moveTokenManager.move){
//				//if the icons are currently moving, set them up to move and leave it be
//				moveTokenManager.SetupTokenMove();
//			}
//			if(!moveTokenManager.MoveTokensToFillEmptySpaces()){
//				//if the MoveTokenManager hasn't added any tokens to the grid
//				//tell Repopulate Script to add new tokens
//				repopulateManager.AddNewTokensToRepopulateGrid();
//			}
//		}
	}

	/// <summary>
	/// Create a parent object for tokens, and then create a token at each grid position
	/// </summary>
	void MakeGrid(){
		grid = new GameObject("TokenGrid");

		for(int x = 0; x < gridWidth; x++){

//			GameObject newStringGraphic = Instantiate(stringGraphic,
//				new Vector3(x - gridWidth/2 * tokenSize,
//					0.0f,
//					0.0f),
//				Quaternion.identity) as GameObject;
//			newStringGraphic.transform.parent = transform.root.Find("Strings");

			for(int y = 0; y < gridHeight; y++){
				AddTokenToPosInGrid(x, y, grid);
			}
		}
	}

	/// <summary>
	/// Determine whether there are any empty spaces in the grid.
	/// </summary>
	/// <returns><c>true</c> upon finding an empty space, <c>false</c> if it finds none.</returns>
	public virtual bool GridHasEmpty(){
		//this checks every x and y in the grid
		for(int x = 0; x < gridWidth; x++){
			for(int y = 0; y < gridHeight ; y++){
				if(gridArray[x, y] == null){
					return true;
				}
			}
		}

		return false;
	}

	/// <summary>
	/// Gets the grid position of a specific token. (0, 0) is the lower-left.
	/// 
	/// This is not the world position of the token--that is handled by GetWorldPositionFromGridPosition(), below.
	/// </summary>
	/// 
	/// <param name="token">The token whose grid position is to be found,</param>
	/// <returns>The coordinates of the token in the grid.</returns>
	public Vector2 GetPositionOfTokenInGrid(GameObject token){
		for(int x = 0; x < gridWidth; x++){
			for(int y = 0; y < gridHeight ; y++){
				if(gridArray[x, y] == token){
					return(new Vector2(x, y));
				}
			}
		}
		return new Vector2();
	}


	/// <summary>
	/// Converts a grid position into a world space position.
	/// </summary>
	/// 
	/// <param name="x">The grid position x-coordinate</param>
	/// <param name="y">The grid position y-coordinate</param>
	/// <returns>The grid position's 2D world space position</returns>
	public Vector2 GetWorldPositionFromGridPosition(int x, int y){
		return new Vector2(
			(x - gridWidth/2) * tokenSize + xOffset,
			(y - gridHeight/2) * tokenSize + yOffset);
	}


	/// <summary>
	/// This creates a random token and puts it into the grid at the given coordinate
	/// (It also makes the token a child of the grid, to keep the hierarchy tidy.)
	/// </summary>
	/// 
	/// <param name="x">An int x that is the x coordinate in the grid</param>
	/// <param name="y">An int y that is the y coordinate in the grid</param>
	/// <param name="parent">The parent of this token. It should be TokenGrid</param> 
	public void AddTokenToPosInGrid(int x, int y, GameObject parent){
		Vector3 position = GetWorldPositionFromGridPosition(x, y);
		GameObject token = 
			//we create a random kind of token, at that exact position in the grid, with the same rotation as its parent (TokenGrid)
			Instantiate(GetTokenForColumn(x), 
				position, 
				Quaternion.identity) as GameObject;
		token.transform.parent = parent.transform;
		//then, we put this token into the array of tokens
		gridArray[x, y] = token;
	}

	/// <summary>
	/// This is called at the start of the game to see if the random process for creating tokens has left matches. If so, it changes
	/// the affected sprites to try to eliminate matches, and then is called again recursively until no matches are detected.
	/// </summary>
	protected void ChangeGridDuplicates(){
		bool foundDuplicates = false;

		for (int x = 0; x < gridWidth; x++){
			for (int y = 0; y < gridHeight; y++){
				if (x < gridWidth - 2){
					if (matchManager.GridHasAChord(x, y)){
						gridArray[x, y].GetComponent<SpriteRenderer>().sprite = ChangeSprite(x, y);
						foundDuplicates = true;
					}
				}

				if (x < gridWidth - 3 && y < gridHeight - 2){
					if (matchManager.GridHasCChord(x, y)){
						gridArray[x, y].GetComponent<SpriteRenderer>().sprite = ChangeSprite(x, y);
						foundDuplicates = true;
					}
				}

				if (x < gridWidth - 5 && y < gridHeight - 1){
					if (matchManager.GridHasGChord(x, y)){
						gridArray[x, y].GetComponent<SpriteRenderer>().sprite = ChangeSprite(x, y);
						foundDuplicates = true;
					}
				}
			}
		}

		if (foundDuplicates) { ChangeGridDuplicates(); }
	}

	protected Sprite ChangeSprite(int x, int y){
		int index = Array.IndexOf(noteSprites, gridArray[x, y].GetComponent<SpriteRenderer>().sprite);
		index++;
		if (index > noteSprites.Length - 1) { index = 0; }
		return noteSprites[index];
	}

	/// <summary>
	/// Returns a token appropriate for the column provided.
	/// 
	/// This assumes that tokens are named with the number of the columns they can appear in!
	/// The number must be zero-indexed, so the strings are 0-5, not 1-6.
	/// </summary>
	/// <returns>A token that can exist in the column.</returns>
	/// <param name="column">The column to be filled.</param>
	protected GameObject GetTokenForColumn(int column){
		List<GameObject> tokens = new List<GameObject>();

		foreach (GameObject token in tokenTypes){
			if (token.name.Contains(column.ToString())){
				tokens.Add(token);
			}
		}

		return tokens[UnityEngine.Random.Range(0, tokens.Count)];
	}
}
