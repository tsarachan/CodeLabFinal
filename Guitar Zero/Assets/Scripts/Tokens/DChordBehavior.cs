/*
 * 
 * All tokens have a script which applies their particular note to the sound-playing process in Note.
 * 
 */

using UnityEngine;
using System.Collections;

public class DChordBehavior : Note {

	private const string D_CHORD = "D chord";

	protected override void Start(){
		chordName = D_CHORD;

		base.Start();
	}

	public override void DestroyFeedback(){
		PlaySound();
	}
}
