using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Extensions {

	//flip a transform around the stated axis
	//the flip will be around the anchor point

	/// <summary>
	/// Reflects a transform around the X axis, based on the anchor.
	/// </summary>
	/// <param name="t">T.</param>
	/// <param name="f">F.</param>
	public static void FlipX(this Transform t, float f)
	{
		Vector3 scale = t.localScale;
		scale.x *= -1.0f;
		t.localScale = scale;
	}

	/// <summary>
	/// Reflects a transform around the Y axis, based on the anchor.
	/// </summary>
	/// <param name="t">T.</param>
	/// <param name="f">F.</param>
	public static void FlipY(this Transform t, float f)
	{
		Vector3 scale = t.localScale;
		scale.y *= -1.0f;
		t.localScale = scale;
	}

	/// <summary>
	/// Reflects a transform around the Z axis, based on the anchor.
	/// </summary>
	/// <param name="t">T.</param>
	/// <param name="f">F.</param>
	public static void FlipZ(this Transform t, float f)
	{
		Vector3 scale = t.localScale;
		scale.z *= -1.0f;
		t.localScale = scale;
	}
		 
	/// <summary>
	/// Get new vector that is [degrees] away from [vector].
	/// </summary>
	/// <returns>The direction vector.</returns>
	/// <param name="vector">Vector.</param>
	/// <param name="degrees">Degrees.</param>
	public static Vector3 UnitDirectionVector(this Vector3 vector, float degrees)
	{
		vector.x = Mathf.Cos(degrees * Mathf.Deg2Rad);
		vector.y = Mathf.Sin(degrees * Mathf.Deg2Rad);
		//vector.z does not change

		return vector;
	}

	/// <summary>
	/// Moves the item at index to front.
	/// </summary>
	/// <param name="list">List to have element moved</param>
	/// <param name="index">current index of item to go to front</param>
	/// <typeparam name="T">Type of list</typeparam>
	public static void MoveItemAtIndexToFront<T>(this List<T> list, int index)
	{
		T item = list[index];
		list.RemoveAt(index);
		list.Insert(0, item);
	}
}
