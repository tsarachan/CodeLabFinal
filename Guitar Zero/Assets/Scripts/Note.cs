/*
 * 
 * This is a base class for the tokens. It sets expectations for the tokens--they'll all have a sound they play, and feedback they give when
 * removed from the board.
 * 
 */

using UnityEngine;
using System.Collections;

public abstract class Note : MonoBehaviour {

	protected AudioClip chord;
	protected string chordName = "";
	protected AudioSource speaker;
	protected const string SPEAKER_OBJ = "Speaker";
	protected const string AUDIO_FOLDER = "Audio/";


	/// <summary>
	/// IMPORTANT: each chord must override this Start function to define its own chordName. After that, run base.Start().
	/// </summary>
	protected virtual void Start(){
		chord = Resources.Load(AUDIO_FOLDER + chordName) as AudioClip;
		speaker = GameObject.Find(SPEAKER_OBJ).GetComponent<AudioSource>();
	}
		

	/// <summary>
	/// Call this to play the chord.
	/// </summary>
	protected virtual void PlaySound(){
		speaker.clip = chord;
		speaker.Play();
	}


	/// <summary>
	/// Override to define what happens when this token is removed.
	/// </summary>
	protected abstract void DestroyFeedback();
}
