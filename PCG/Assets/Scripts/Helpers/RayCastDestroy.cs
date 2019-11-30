using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastDestroy : MonoBehaviour
{
	private void Awake()
	{
		Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(0.5f, 0.5f, 0.5f));

		//Debug.Log(colliders.Length);

		foreach (Collider c in colliders)
		{
			//Destroy(c.gameObject);
		}
	}
}
