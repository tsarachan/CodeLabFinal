using UnityEngine;
using System.Collections;

public class FadeInAndOut : MonoBehaviour {

	public float speed = 4.0f;

	SpriteRenderer sr;
	Color colorVec;

	void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
		colorVec = sr.color;
	}

	void Update ()
	{
		colorVec.a = 0.5f + 0.5f * Mathf.Cos(Time.time * speed); //using Time.time means this will jump if the script
																 //is deactivated and activated; change if necessary!
		sr.color = colorVec;
	}
}
