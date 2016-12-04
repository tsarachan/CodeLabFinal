using UnityEngine;
using System.Collections;

public class Swing : MonoBehaviour {

	/*
	 * Change the pivot point to change the axis of the swinging
	 */

	Transform tf;
	float duration = 0.0f;

	public float swingSpeed = 1.6f;
	public float swingDist = 8.0f;

	void Awake()
	{
		tf = transform;
	}

	void Update ()
	{
		tf.localEulerAngles = new Vector3(tf.localEulerAngles.x, tf.localEulerAngles.y, Mathf.Cos(duration * swingSpeed) * swingDist);
		duration += Time.deltaTime;
	}
}
