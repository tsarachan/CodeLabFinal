﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleSceneManager : MonoBehaviour {

	private const string CREDITS_SCENE = "Credits scene";
	private const string GAME_SCENE = "Game";
	private const string INSTRUCTION_SCENE = "Instructions scene";

	public void LoadCredits(){
		SceneManager.LoadScene(CREDITS_SCENE);
	}

	public void LoadGame(){
		SceneManager.LoadScene(GAME_SCENE);
	}

	public void LoadInstructions(){
		SceneManager.LoadScene(INSTRUCTION_SCENE);
	}
}
