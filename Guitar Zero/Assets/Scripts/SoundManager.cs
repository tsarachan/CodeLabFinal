using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioMixer mixer;
	private AudioSource audioSource;


	private void Start(){
		audioSource = GetComponent<AudioSource>();
	}

	public void SetMusicVolume(float newVol){
		mixer.SetFloat("music_volume", newVol);
	}
}
