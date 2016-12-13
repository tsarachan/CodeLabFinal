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
 * The player's goal is to get the highest overall score possible before running out of ability to rock!
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


	//UI elements that display the numbers to the player
	private Text currentScoreDisplay;
	private Text overallScoreDisplay;
	private Text healthDisplay;
	private Image crowdApprovalDisplay;

	//used to locate objects in hierarchy during initialization
	private const string UI_CANVAS = "Canvas";
	private const string CURRENT_SCORE_TEXT = "Score this run";
	private const string OVERALL_SCORE_TEXT = "Locked in points";
	private const string HEALTH_TEXT = "Health";
	private const string CROWD_IMAGE = "Crowd";
	private const string ROCK_BUTTON = "Rock button";


	//how score changes as player makes matches--used by ScorePoints()
	public int scoreIncrease = 2;

	//the health the player loses if they're unable to make any matches before time runs out
	public int defaultHealthLoss = 1;

	//used to track the crowd's declining patience
	public float crowdPatience = 3.0f; //the player has this many seconds to make a match
	private float timer;
	private const float MIN_FILL = 0.0f;
	private const float MAX_FILL = 1.0f;

	//used to end the game when health falls below zero.
	private float failTimer = 0.0f;
	public float failGameEndDelay = 1.0f;
	private Color failColor = Color.red;
	private GameManagerScript managerScript;
	private const string FAIL_MARKER = "fail"; //must be the same as FAIL_MARKER in GameManagerScript


	private void Start () {
		currentScoreDisplay = transform.root.Find(UI_CANVAS).Find(CURRENT_SCORE_TEXT).GetComponent<Text>();	
		overallScoreDisplay = transform.root.Find(UI_CANVAS).Find(OVERALL_SCORE_TEXT).GetComponent<Text>();
		healthDisplay = transform.root.Find(UI_CANVAS).Find(HEALTH_TEXT).GetComponent<Text>();
		crowdApprovalDisplay = transform.root.Find(UI_CANVAS).Find(CROWD_IMAGE).GetComponent<Image>();
		managerScript = GetComponent<GameManagerScript>();
		ChangeDisplay();
	}


	/// <summary>
	/// Tracks the game state.
	/// </summary>
	private void Update(){
		//update the crowd's current patience with the player
		crowdApprovalDisplay.fillAmount = CrowdLosesPatience();

		//if the player has run out of health, inform the game manager
		if (health <= 0){
			managerScript.PlayerLost = true;
		}
	}


	private float CrowdLosesPatience(){
		timer += Time.deltaTime;

		if (timer <= crowdPatience){
			return Mathf.Lerp(MAX_FILL, MIN_FILL, timer/crowdPatience);
		} else {
			timer = 0.0f;
			LosePoints();
			return MAX_FILL;
		}
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
		if (currentScore > 0){
			health -= currentScore;
		} else {
			health -= defaultHealthLoss;
		}

		currentScore = 0;
		ChangeDisplay();

		return health; //return value not currently used; future-proofing
	}
	

	/// <summary>
	/// The rock button calls this function to lock in the player's score.
	/// </summary>
	public void RockOut(){
		if (currentScore > 0){ //the player can't rock out just to reset the clock; they have to have progressed.
			overallScore += currentScore;
			currentScore = 0;
			ChangeDisplay();
			crowdApprovalDisplay.fillAmount = ResetCrowdApproval();
		}
	}


	/// <summary>
	/// Used to reset the crowd after rocking out and locking in the current score.
	/// </summary>
	/// <returns>1.0, to max out the crowd's approval.</returns>
	public float ResetCrowdApproval(){
		timer = 0.0f;
		return MAX_FILL;
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
