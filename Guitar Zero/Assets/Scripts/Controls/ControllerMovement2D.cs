using UnityEngine;
using System.Collections;

public class ControllerMovement2D : MonoBehaviour {

	public float speed = 0.1f;

	const string VERT_AXIS = "PS4_LStick_Vert_1";
	const string HORIZ_AXIS = "PS4_LStick_Horiz_1";

	void Update()
	{
		float timeAdjustedSpeed = speed * Time.deltaTime;

		if (Input.GetAxis(VERT_AXIS) <= -0.3)  { Move(new Vector3(0.0f, -timeAdjustedSpeed, 0.0f)); }
		if (Input.GetAxis(VERT_AXIS) >= 0.3)  { Move(new Vector3(0.0f, timeAdjustedSpeed, 0.0f)); }
		if (Input.GetAxis(HORIZ_AXIS) <= -0.3)  { Move(new Vector3(-timeAdjustedSpeed, 0.0f, 0.0f)); }
		if (Input.GetAxis(HORIZ_AXIS) >= 0.3)  { Move(new Vector3(timeAdjustedSpeed, 0.0f, 0.0f)); }
	}

	void Move(Vector3 loc)
	{
		transform.position += loc;
	}
}
