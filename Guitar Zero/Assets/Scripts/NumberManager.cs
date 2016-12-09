/*
 * 
 * This script handles all the numbers in the game--the player's current score, the player's overall score, and the player's health.
 * 
 * When the game starts, the player has no current or overall score, and a certain amount of health.
 * 
 * With every match, the player's current score doubles. Pushing the "rock" button locks that score in, zeroing the current score and
 * increasing the current score by that amount.
 * 
 * If the player does not make a match within the appointed time, the player's current score zeroes and the player loses that much health.
 * 
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NumberManager : MonoBehaviour {

	//internal values for tracking game-relevant numbers
	private int currentScore = 0;
	private int overallScore = 0;
	public int health = 10000;

	//text to display numbers to player in UI
	private Text currentScoreDisplay;
	private Text overallScoreDisplay;
	private Text healthDisplay;

	//used to locate objects in hierarchy during initialization
	private const string UI_CANVAS = "Canvas";
	private const string CURRENT_SCORE_TEXT = "Score this run";
	private const string OVERALL_SCORE_TEXT = "Locked in points";
	private const string HEALTH_TEXT = "Health";


	//how score changes as player makes matches--used by ScorePoints()
	public int scoreIncrease = 2;

	private void Start () {
		currentScoreDisplay = transform.root.Find(UI_CANVAS).Find(CURRENT_SCORE_TEXT).GetComponent<Text>();	
		overallScoreDisplay = transform.root.Find(UI_CANVAS).Find(OVERALL_SCORE_TEXT).GetComponent<Text>();
		healthDisplay = transform.root.Find(UI_CANVAS).Find(HEALTH_TEXT).GetComponent<Text>();
		ChangeDisplay();
	}


	/// <summary>
	/// Called when the player makes a match to increase the player's score.
	/// </summary>
	/// <returns>The player's new score.</returns>
	public int ScorePoints(){
		if (currentScore == 0){
			currentScore = 1;
		} else {
			currentScore *= scoreIncrease;
		}

		ChangeDisplay();

		return currentScore; //return value not currently used; future-proofing
	}


	/// <summary>
	/// Called when the player fails to make a match within the allotted time to lose health.
	/// </summary>
	/// <returns>The player's new health.</returns>
	public int LosePoints(){
		health -= currentScore;
		currentScore = 0;
		ChangeDisplay();

		return health; //return value not currently used; future-proofing
	}
	

	/// <summary>
	/// The rock button calls this function to lock in the player's score.
	/// </summary>
	public void RockOut(){
		overallScore += currentScore;
		currentScore = 0;
		ChangeDisplay();
		Debug.Log("Button pressed");
	}


	/// <summary>
	/// Whenever any other function changes a number, it calls this function to keep the UI consistent with the game state.
	/// </summary>
	private void ChangeDisplay(){
		currentScoreDisplay.text = currentScore.ToString();
		overallScoreDisplay.text = overallScore.ToString();
		healthDisplay.text = health.ToString();
	}
}
