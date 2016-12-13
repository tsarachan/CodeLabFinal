/*
 * 
 * This is a base class for the tokens. It sets expectations for the tokens--they'll all have a sound they play, and feedback they give when
 * removed from the board.
 * 
 */

using UnityEngine;
using System.Collections;

public abstract class Note : MonoBehaviour {

	//variables for playing the chord associated with this token
	protected AudioClip chord;
	protected string chordName = "";
	protected const string AUDIO_FOLDER = "Audio/";
	protected GameObject myNotePlayer;
	protected const string NOTE_PLAYER_OBJ = "Note player";


	//variables relating to background music
	protected SoundManager musicManager;
	protected const string SPEAKER_OBJ = "Speaker";
	public float musicVolDuringChord = -20.0f; //the volume of background music in the mixer when this chord is playing
	protected bool chordIsPlaying = false;


	/// <summary>
	/// IMPORTANT: each chord must override this Start function to define its own chordName. After that, run base.Start().
	/// </summary>
	protected virtual void Start(){
		chord = Resources.Load(AUDIO_FOLDER + chordName) as AudioClip;
		myNotePlayer = Resources.Load(AUDIO_FOLDER + NOTE_PLAYER_OBJ) as GameObject;
		myNotePlayer.GetComponent<AudioSource>().clip = chord;
		musicManager = GameObject.Find(SPEAKER_OBJ).GetComponent<SoundManager>();
	}
		

	/// <summary>
	/// Call this to play this token's associated chord.
	/// 
	/// In addition to playing its own chord, this quiets the background music so that the chord is heard clearly.
	/// </summary>
	protected virtual void PlaySound(){
		musicManager.SetMusicVolume(musicVolDuringChord);
		Instantiate(myNotePlayer);
		chordIsPlaying = true;
	}


	/// <summary>
	/// Override to define what happens when this token is removed.
	/// </summary>
	public abstract void DestroyFeedback();
}
