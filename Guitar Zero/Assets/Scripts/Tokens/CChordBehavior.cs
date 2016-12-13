/*
 * 
 * All tokens have a script which applies their particular note to the sound-playing process in Note.
 * 
 */

using UnityEngine;
using System.Collections;

public class CChordBehavior : Note {

	private const string C_CHORD = "C chord";

	protected override void Start(){
		chordName = C_CHORD;

		base.Start();
	}

	public override void DestroyFeedback(){
		PlaySound();
	}
}
