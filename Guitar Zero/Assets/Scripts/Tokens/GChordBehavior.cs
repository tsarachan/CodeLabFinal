/*
 * 
 * All tokens have a script which applies their particular note to the sound-playing process in Note.
 * 
 */

using UnityEngine;
using System.Collections;

public class GChordBehavior : Note {

	private const string G_CHORD = "G chord";

	protected override void Start(){
		chordName = G_CHORD;

		base.Start();
	}

	protected override void DestroyFeedback(){
		PlaySound();
	}
}
