/*
 * 
 * All tokens have a script which applies their particular note to the sound-playing process in Note.
 * 
 */

using UnityEngine;
using System.Collections;

public class AChordBehavior : Note {

	private const string A_CHORD = "A chord";

	protected override void Start(){
		chordName = A_CHORD;

		base.Start();
	}

	public override void DestroyFeedback(){
		PlaySound();
	}
}
