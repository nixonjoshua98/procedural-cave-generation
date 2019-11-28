using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastDestroy : MonoBehaviour
{
	private void Awake()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, 48.0f);

		foreach (Collider c in colliders)
		{
			if (c.gameObject == gameObject)
				continue;

			Destroy(c.gameObject);
		}
	}
}
