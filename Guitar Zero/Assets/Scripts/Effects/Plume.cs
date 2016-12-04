using UnityEngine;
using System.Collections;

public class Plume : MonoBehaviour {

	public string obj;
	public float forceMax = 2.0f;
	public float lifeDuration = 1.0f;
	float lifeTimer = 0.0f;

	public float objRate = 0.1f;
	float objTimer = 0.0f;

	void Update()
	{
		lifeTimer += Time.deltaTime;

		if (lifeTimer <= lifeDuration)
		{
			objTimer += Time.deltaTime;

			if (objTimer >= objRate)
			{
				MakeObj();
				objTimer = 0.0f;
			}
		}
		else { Destroy(gameObject); }
	}

	void MakeObj()
	{
		GameObject coin = Instantiate(Resources.Load(obj)) as GameObject;
		coin.transform.position = transform.position;
		coin.GetComponent<Rigidbody>().AddForce(Random.Range(-forceMax, forceMax),
												Random.Range(-forceMax, forceMax),
												forceMax,
												ForceMode.Impulse);
		coin.GetComponent<Rigidbody>().AddTorque(Random.Range(-forceMax, forceMax),
												 Random.Range(-forceMax, forceMax),
												 Random.Range(-forceMax, forceMax),
												 ForceMode.Impulse);
	}
}
