using UnityEngine;
using System.Collections;

public class NotePlayerBehavior : MonoBehaviour {

	private SoundManager musicManager;
	private const string SPEAKER_OBJ = "Speaker";
	public float musicVolNormal = 0.0f; //the background music's volume when no chord is playing

	private AudioSource audioSource;

	private void Start(){
		musicManager = GameObject.Find(SPEAKER_OBJ).GetComponent<SoundManager>();
		audioSource = GetComponent<AudioSource>();
	}

	/// <summary>
	/// The if-statement here detects when the chord this object is playing is over, and then
	/// notifies the background music to return to normal volume.
	/// </summary>
	protected void Update(){
		if (audioSource.isPlaying == false){
			musicManager.SetMusicVolume(musicVolNormal);
			Destroy(gameObject);
		}
	}
}
