using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
	public GameObject plank;

	public void Start()
	{
		GenerateCampfire();
	}

	private void GenerateCampfire()
	{
		float radius = 0.1f;
		int numPlanks = Random.Range(3, 7);

		for (int i = 0; i < numPlanks; i++)
		{
			GameObject _plank = Instantiate(plank, Vector3.zero, Quaternion.identity, transform);

			int ang = (360 / numPlanks) * i;

			Vector3 pos = Vector3.zero;

			pos.x = radius * Mathf.Sin(ang * Mathf.Deg2Rad);
			pos.z = radius * Mathf.Cos(ang * Mathf.Deg2Rad);

			Quaternion rot = Quaternion.FromToRotation(Vector3.forward, Vector3.zero - pos);

			_plank.transform.rotation = rot;
			_plank.transform.localScale = new Vector3(0.2f, 0.2f, 0.5f);
			_plank.transform.localPosition = pos;
		}
	}
}