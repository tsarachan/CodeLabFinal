/*
 * 
 * This script provides other objects access to the background music's volume.
 * 
 */

using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioMixer mixer;

	/// <summary>
	/// Other objects can call this to set the background music's volume.
	/// </summary>
	/// <param name="newVol">Intended volume, from -80.0f to +20.0f.</param>
	public void SetMusicVolume(float newVol){
		mixer.SetFloat("music_volume", newVol);
	}
}
