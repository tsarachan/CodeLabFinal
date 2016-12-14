/*
 * 
 * This script switches UI elements on and off. It's used to display or hide the chord diagram during play.
 * 
 */

using UnityEngine;
using System.Collections;

public class UIStateSetter : MonoBehaviour {

	/// <summary>
	/// Switch a UI element (or, for that matter, anything else) on.
	/// </summary>
	public void TurnOnUI(){
		gameObject.SetActive(true);
	}


	/// <summary>
	/// Switch a UI element off.
	/// </summary>
	public void TurnOffUI(){
		gameObject.SetActive(false);
	}
}
