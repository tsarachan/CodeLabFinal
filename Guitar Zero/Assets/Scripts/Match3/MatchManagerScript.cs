using UnityEngine;
using System.Collections;

public class MatchManagerScript : MonoBehaviour {

	protected GameManagerScript gameManager;

	protected GameObject colorBurst;
	const int CHORD_SIZE = 3;
	protected int crowdMultiplier = 1;
	public int CrowdMultiplier{
		get { return crowdMultiplier; }
		set{
			crowdMultiplier = value;
		}
	}
	private int successesThisMultiplier = 0;
	public int SuccessesThisMultiplier{
		get { return successesThisMultiplier; }
		set{
			successesThisMultiplier = value;
		}
	}
	protected const int MIN_CROWD_MULTIPLIER = 1;
	public int Min_Crowd_Multiplier{
		get { return MIN_CROWD_MULTIPLIER; }
	}
	protected const int A_CHORD_MULTIPLIER = 1;
	protected const int C_CHORD_MULTIPLIER = 3;
	protected const int G_CHORD_MULTIPLIER = 2;
	protected const int D_CHORD_MULTIPLIER = 4;
	protected const int F_CHORD_MULTIPLIER = 6;
	protected const string SCORE_CANVAS = "Score canvas";
	protected const string SCORE_TEXT = "Score";
	protected const string CROWD_METER = "Crowd anger meter";
	protected const string COLOR_BURST = "Color burst";

	protected const string A_CHORD_NAME = "A";
	protected const string C_CHORD_NAME = "C";
	protected const string G_CHORD_NAME = "G";
	protected const string D_CHORD_NAME = "D";
	protected const string F_CHORD_NAME = "F";


	public virtual void Awake (){
		gameManager = GetComponent<GameManagerScript>();
		colorBurst = Resources.Load(COLOR_BURST) as GameObject;
	}

	/// <summary>
	/// Checks the entire grid for matches.
	/// </summary>
	/// 
	/// <returns><c>true</c> if there are any matches, <c>false</c> otherwise.</returns>
	public virtual bool GridHasMatch(){
		bool match = false; //assume there is no match

		//check each square in the grid
		for(int x = 0; x < gameManager.gridWidth; x++){
			for(int y = 0; y < gameManager.gridHeight ; y++){
				if (x < gameManager.gridWidth - 2){	//GridHasHorizontalMatch checks 2 to the right
					//gameManager.gridWidth - 2 ensures you're never extending into
					//a space that doesn't exist
					match = match || GridHasAChord(x, y); //if match was ever set to true, it stays true forever
				}

				if (x < gameManager.gridWidth - 3 && y < gameManager.gridHeight - 2){
					match = match || GridHasCChord(x, y);
				}

				if (x < gameManager.gridWidth - 5 && y < gameManager.gridHeight - 1){
					match = match || GridHasGChord(x, y);
				}

				if (x < gameManager.gridWidth - 2 && y < gameManager.gridHeight - 1 && y > 0) {
					match = match || GridHasDChord(x, y);
				}

				//				if (x < gameManager.gridWidth - 5 &&  y > 1) {
				//					match = match || GridHasFChord(x, y);
				//				}
			}
		}

		return match;
	}


	#region chord determination

	/*
	 * 
	 * These check whether there's a match, according to the layout of the tokens.
	 * These don't check where the layout appears; that's handled by GameManagerScript, which doesn't
	 * allow tokens to appear in the wrong columns.
	 * 
	 * These also check to make sure that the sprite has the correct name. Otherwise, the layouts would be
	 * chord-agnostic; one would be able to score by making the "A" patterns out of Cs, for example.
	 * For this to work, the sprites must have the chord name as a capital letter, and no sprite can
	 * have another chord's letter as a capital!
	 * 
	 */

	public bool GridHasAChord(int x, int y){
		GameObject token1 = gameManager.gridArray[x + 0, y + 0];
		GameObject token2 = gameManager.gridArray[x + 1, y + 0];
		GameObject token3 = gameManager.gridArray[x + 2, y + 0];

		if(token1 != null && token2 != null && token3 != null){ //ensure all of the tokens exist
			SpriteRenderer sr1 = token1.GetComponent<SpriteRenderer>();
			SpriteRenderer sr2 = token2.GetComponent<SpriteRenderer>();
			SpriteRenderer sr3 = token3.GetComponent<SpriteRenderer>();

			return (sr1.sprite == sr2.sprite && sr2.sprite == sr3.sprite
				&& sr1.sprite.name.Contains(A_CHORD_NAME));  //compare their sprites
		} else {
			return false;
		}
	}

	public bool GridHasCChord(int x, int y){
		GameObject token1 = gameManager.gridArray[x + 0, y + 0];
		GameObject token2 = gameManager.gridArray[x + 1, y + 1];
		GameObject token3 = gameManager.gridArray[x + 3, y + 2];

		if(token1 != null && token2 != null && token3 != null){
			SpriteRenderer sr1 = token1.GetComponent<SpriteRenderer>();
			SpriteRenderer sr2 = token2.GetComponent<SpriteRenderer>();
			SpriteRenderer sr3 = token3.GetComponent<SpriteRenderer>();

			return (sr1.sprite == sr2.sprite && sr2.sprite == sr3.sprite
				&& sr1.sprite.name.Contains(C_CHORD_NAME));  //compare their sprites
		} else {
			return false;
		}
	}

	public bool GridHasGChord(int x, int y){
		GameObject token1 = gameManager.gridArray[x + 0, y + 0];
		GameObject token2 = gameManager.gridArray[x + 1, y + 1];
		GameObject token3 = gameManager.gridArray[x + 5, y + 0];

		if(token1 != null && token2 != null && token3 != null){
			SpriteRenderer sr1 = token1.GetComponent<SpriteRenderer>();
			SpriteRenderer sr2 = token2.GetComponent<SpriteRenderer>();
			SpriteRenderer sr3 = token3.GetComponent<SpriteRenderer>();

			return (sr1.sprite == sr2.sprite && sr2.sprite == sr3.sprite
				&& sr1.sprite.name.Contains(G_CHORD_NAME));  //compare their sprites
		} else {
			return false;
		}
	}

	public bool GridHasDChord(int x, int y){
		GameObject token1 = gameManager.gridArray[x + 0, y + 0];
		GameObject token2 = gameManager.gridArray[x + 1, y - 1];
		GameObject token3 = gameManager.gridArray[x + 2, y + 0];

		if(token1 != null && token2 != null && token3 != null){
			SpriteRenderer sr1 = token1.GetComponent<SpriteRenderer>();
			SpriteRenderer sr2 = token2.GetComponent<SpriteRenderer>();
			SpriteRenderer sr3 = token3.GetComponent<SpriteRenderer>();

			return (sr1.sprite == sr2.sprite && sr2.sprite == sr3.sprite
				&& sr1.sprite.name.Contains(D_CHORD_NAME));  //compare their sprites
		} else {
			return false;
		}
	}

	//	public bool GridHasFChord(int x, int y){
	//		GameObject token1 = gameManager.gridArray[x + 0, y + 0];
	//		GameObject token2 = gameManager.gridArray[x + 1, y - 2];
	//		GameObject token3 = gameManager.gridArray[x + 2, y - 2];
	//		GameObject token4 = gameManager.gridArray[x + 3, y - 1];
	//		GameObject token5 = gameManager.gridArray[x + 4, y + 0];
	//		GameObject token6 = gameManager.gridArray[x + 5, y + 0];
	//
	//		if(token1 != null && token2 != null && token3 != null
	//			&& token4 != null && token5 != null && token6 != null){
	//			SpriteRenderer sr1 = token1.GetComponent<SpriteRenderer>();
	//			SpriteRenderer sr2 = token2.GetComponent<SpriteRenderer>();
	//			SpriteRenderer sr3 = token3.GetComponent<SpriteRenderer>();
	//			SpriteRenderer sr4 = token4.GetComponent<SpriteRenderer>();
	//			SpriteRenderer sr5 = token5.GetComponent<SpriteRenderer>();
	//			SpriteRenderer sr6 = token6.GetComponent<SpriteRenderer>();
	//
	//			return (sr1.sprite == sr2.sprite && sr2.sprite == sr3.sprite
	//					&& sr3.sprite == sr4.sprite && sr4.sprite == sr5.sprite
	//					&& sr5.sprite == sr6.sprite
	//					&& sr1.sprite.name.Contains(F_CHORD_NAME));  //compare their sprites to see if they're the same
	//		} else {
	//			return false;
	//		}
	//	}

	#endregion

	/// <summary>
	/// Destroys all tokens in a match of three or more
	/// </summary>
	/// <returns>The number of tokens destroyed.</returns>
	public virtual int RemoveMatches(){
		int numRemoved = 0;

		//iterate across entire grid, looking for matches
		//wherever a chord is found, respond
		for(int x = 0; x < gameManager.gridWidth; x++){
			for(int y = 0; y < gameManager.gridHeight ; y++){

				if (x < gameManager.gridWidth - 2){
					if (GridHasAChord(x, y)){
						RemoveChord(x, y, 'A');
						numRemoved += 1;
					}
				}

				if (x < gameManager.gridWidth - 3 && y < gameManager.gridHeight - 2){
					if (GridHasCChord(x, y)){
						RemoveChord(x, y, 'C');
						numRemoved += 1;
					}
				}

				if (x < gameManager.gridWidth - 5 && y < gameManager.gridHeight - 1){
					if (GridHasGChord(x, y)){
						RemoveChord(x, y, 'G');
						numRemoved += 1;
					}
				}

				if (x < gameManager.gridWidth - 2 && y < gameManager.gridHeight - 1 && y > 0){
					if (GridHasDChord(x, y)){
						RemoveChord(x, y, 'D');
						numRemoved+= 1;
					}
				}

				//				if (x < gameManager.gridWidth - 5 &&  y > 1){
				//					if (GridHasFChord(x, y)){
				//						RemoveChord(x, y, 'F');
				//						numRemoved += 1;
				//					}
				//				}
			}
		}

		return numRemoved;
	}

	protected void RemoveChord(int x, int y, char chord){
		GameObject token1 = gameManager.gridArray[x, y];
		gameManager.gridArray[x, y] = null;


		GameObject token2 = null;
		GameObject token3 = null;

		switch(chord){
		case 'A':
			token2 = gameManager.gridArray[x + 1, y + 0];
			gameManager.gridArray[x + 1, y + 0] = null;
			token3 = gameManager.gridArray[x + 2, y + 0];
			gameManager.gridArray[x + 2, y + 0] = null;
			break;
		case 'C':
			token2 = gameManager.gridArray[x + 1, y + 1];
			gameManager.gridArray[x + 1, y + 1] = null;
			token3 = gameManager.gridArray[x + 3, y + 2];
			gameManager.gridArray[x + 3, y + 2] = null;
			break;
		case 'G':
			token2 = gameManager.gridArray[x + 1, y + 1];
			gameManager.gridArray[x + 1, y + 1] = null;
			token3 = gameManager.gridArray[x + 5, y + 0];
			gameManager.gridArray[x + 5, y + 0] = null;
			break;
		case 'D':
			token2 = gameManager.gridArray[x + 1, y - 1];
			gameManager.gridArray[x + 1, y - 1] = null;
			token3 = gameManager.gridArray[x + 2, y + 0];
			gameManager.gridArray[x + 2, y + 0] = null;
			break;
			//		case 'F':
			//				token2 = gameManager.gridArray[x + 1, y - 2];
			//				gameManager.gridArray[x + 1, y - 2] = null;
			//				token3 = gameManager.gridArray[x + 2, y - 2];
			//				gameManager.gridArray[x + 2, y - 2] = null;
			//
			//				//the F chord has 6 notes, so handle the first 3 normally, and the remaining 3 here
			//				GameObject token4 = gameManager.gridArray[x + 3, y - 1];
			//				gameManager.gridArray[x + 3, y - 1] = null;
			//				GameObject token5 = gameManager.gridArray[x + 4, y + 0];
			//				gameManager.gridArray[x + 4, y + 0] = null;
			//				GameObject token6 = gameManager.gridArray[x + 5, y + 0];
			//				gameManager.gridArray[x + 5, y + 0] = null;
			//
			//				ChordFeedback(new GameObject[] { token4, token5, token6 }, chord);
			//
			//				Destroy(token4);
			//				Destroy(token5);
			//				Destroy(token6);
			//				
			//				break;
		default:
			Debug.Log("Illegal chord: " + chord);
			break;
		}

		ChordFeedback(new GameObject[] { token1, token2, token3 }, chord);

		Destroy(token1);
		Destroy(token2);
		Destroy(token3);

		SuccessesThisMultiplier++;

		if (SuccessesThisMultiplier >= CrowdMultiplier){
			CrowdMultiplier++;
			SuccessesThisMultiplier = 0;
		}
	}

	protected void ChordFeedback(GameObject[] tokens, char chord){
		int chordMultiplier = 1;

		switch (chord){
		case 'A':
			chordMultiplier = A_CHORD_MULTIPLIER;
			break;
		case 'C':
			chordMultiplier = C_CHORD_MULTIPLIER;
			break;
		case 'G':
			chordMultiplier = G_CHORD_MULTIPLIER;
			break;
		case 'D':
			chordMultiplier = D_CHORD_MULTIPLIER;
			break;
			//			case 'F':
			//				chordMultiplier = F_CHORD_MULTIPLIER;
			//				break;
		default:
			Debug.Log("Illegal chord: " + chord);
			break;
		}

		foreach (GameObject token in tokens){
			//Instantiate(colorBurst, token.transform.position, Quaternion.identity);
		}
	}
}
