using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyController : MonoBehaviour
{
	public GameObject firefly;

	private void Start()
	{
		int numFlies = 256;

		for (int i = 0; i < numFlies; i++)
		{
			Instantiate(firefly, transform);
		}
	}
}
