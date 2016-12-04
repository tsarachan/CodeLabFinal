using UnityEngine;
using System.Collections;

public class InputManagerScript : MonoBehaviour {

	//this script tracks player inputs and makes changes to the game

	protected GameManagerScript gameManager;
	protected MoveTokensScript moveManager;

	//selected is a variable which tracks which token (if any) we've currently selected
	protected GameObject selected = null;
	protected GameObject selectionIcon;
	public Vector3 offScreen = new Vector3(100.0f, 100.0f, 100.0f);

	public virtual void Start () {
		moveManager = GetComponent<MoveTokensScript>();
		gameManager = GetComponent<GameManagerScript>();
		selectionIcon = Instantiate(Resources.Load("Selection marker"), offScreen, Quaternion.identity) as GameObject;
	}
		
	public virtual void SelectToken(){

		//handle mouse input
		if(Input.GetMouseButtonDown(0)){
			Debug.Log("Attempt to select detected; Input.mousePosition == " + Input.mousePosition);
			//when you click, check where on the screen you're clicking
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Debug.Log("mousePos == " + mousePos);
			//use a 2D raycast to see what you're clicking on
			Collider2D collider = Physics2D.OverlapPoint(mousePos);

			if(collider != null){
				Debug.Log("Detected collider.name == " + collider.name);
				//if you click on something...
				if(selected == null){
					//if we haven't yet selected a token, select this token and remember it
					selected = collider.gameObject;
					selectionIcon.transform.position = collider.transform.position;
				} else {
					//if we HAVE already selected a token, calculate the distance between this token (which we're currently clicking on)
					//and that one (which we clicked on last time)
					Vector2 pos1 = gameManager.GetPositionOfTokenInGrid(selected);
					Vector2 pos2 = gameManager.GetPositionOfTokenInGrid(collider.gameObject);

					//if they're next to each other, swap them
					if(Mathf.Abs(pos1.x - pos2.x) + Mathf.Abs(pos1.y - pos2.y) == 1){
						Debug.Log("trying to swap");
						moveManager.SetupTokenExchange(selected, pos1, collider.gameObject, pos2, true);
					}
					//then deselect our current token (because we're about to destroy or forget it)
					selected = null;
					selectionIcon.transform.position = offScreen;
				}
			}
		}

		//handle touch input
		foreach (Touch touch in Input.touches){
			if (touch.phase == TouchPhase.Began){
				Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

				Collider2D collider = Physics2D.OverlapPoint(touchPos);

				if(collider != null){
					//if you click on something...
					if(selected == null){
						//if we haven't yet selected a token, select this token and remember it
						selected = collider.gameObject;
						selectionIcon.transform.position = collider.transform.position;
					} else {
						//if we HAVE already selected a token, calculate the distance between this token (which we're currently clicking on)
						//and that one (which we clicked on last time)
						Vector2 pos1 = gameManager.GetPositionOfTokenInGrid(selected);
						Vector2 pos2 = gameManager.GetPositionOfTokenInGrid(collider.gameObject);

						//if they're next to each other, swap them
						if(Mathf.Abs(pos1.x - pos2.x) + Mathf.Abs(pos1.y - pos2.y) == 1){
							Debug.Log("trying to swap");
							moveManager.SetupTokenExchange(selected, pos1, collider.gameObject, pos2, true);
						}
						//then deselect our current token (because we're about to destroy or forget it)
						selected = null;
						selectionIcon.transform.position = offScreen;
					}
				}
			}
		}
	}

	/// <summary>
	/// This seems like a pretty dumb function. 
	/// Maybe Matt just put it here to show some cool commenting tricks?
	/// Maybe these comments aren't even right?
	/// </summary>
	/// 
	/// <param name="x">A float x that will be divided</param>
	/// <param name="y">A float x that will be divided</param>
	/// 
	/// <returns>The value of param x plus y</returns>
	///
	private int CommentFunc(int x, int y){
		return x - y;
	}
}
