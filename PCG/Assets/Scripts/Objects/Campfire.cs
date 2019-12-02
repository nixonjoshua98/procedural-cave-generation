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
		Vector3 center = this.transform.position;
		Debug.Log(center);


		float radius	= 0.1f;
		int numPlanks	= Random.Range(3, 7);

		for (int i = 0; i < numPlanks; i++)
		{
			//int ang = (360 / numPlanks) * i;

			//Vector3 pos;

			//pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
			//pos.y = center.z;
			//pos.z = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);

			//Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);

			GameObject _plank = Instantiate(plank, Vector3.zero, Quaternion.identity, transform);

			int ang = (360 / numPlanks) * i;

			Vector3 pos;

			pos.x = radius * Mathf.Sin(ang * Mathf.Deg2Rad);
			pos.y = 0;
			pos.z = radius * Mathf.Cos(ang * Mathf.Deg2Rad);

			Quaternion rot = Quaternion.FromToRotation(Vector3.forward, Vector3.zero - pos);

			
			

			_plank.transform.rotation = rot;

			_plank.transform.localScale = new Vector3(0.1f, 0.1f, 0.25f);
			_plank.transform.localPosition = pos;
		}
	}
}
