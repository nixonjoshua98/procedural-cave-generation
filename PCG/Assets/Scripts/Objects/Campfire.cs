using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
	public GameObject plank;

	private void Start()
	{
		Generate();
	}

	private void Generate()
	{
		Vector3 center = transform.position;

		float radius	= 0.1f;
		int numObjects	= Random.Range(3, 7);

		for (int i = 0; i < numObjects; i++)
		{
			int ang = (360 / numObjects) * i;

			Vector3 pos;

			pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
			pos.y = center.z;
			pos.z = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);

			Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);

			GameObject _plank = Instantiate(plank, pos, rot, transform);

			_plank.transform.localScale = new Vector3(0.1f, 0.1f, 0.25f);
		}
	}
}
