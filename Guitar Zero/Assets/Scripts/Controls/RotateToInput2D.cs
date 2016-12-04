using UnityEngine;
using System.Collections;

public class RotateToInput2D : MonoBehaviour {

	const string VERT_AXIS = "PS4_LStick_Vert_1";
	const string HORIZ_AXIS = "PS4_LStick_Horiz_1";

	void Update()
	{
		Vector3 pointAt = new Vector3(0.0f, 0.0f, 0.0f);

		if (Input.GetAxis(VERT_AXIS) <= -0.3)  { pointAt.y = -1.0f; }
		if (Input.GetAxis(VERT_AXIS) >= 0.3)  { pointAt.y = 1.0f; }
		if (Input.GetAxis(HORIZ_AXIS) <= -0.3)  { pointAt.x = -1.0f; }
		if (Input.GetAxis(HORIZ_AXIS) >= 0.3)  { pointAt.x = 1.0f; }

		transform.up = pointAt;
	}
}
