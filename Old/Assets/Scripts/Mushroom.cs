using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
	private float maxScaleMultiplier	= 2.5f;
	private float scaleMultiplier		= 1.0f;

	private Vector3 initialScale;

	private void Start()
	{
		initialScale = transform.localScale;
	}
	private void FixedUpdate()
	{
		scaleMultiplier = Mathf.Clamp(scaleMultiplier * 1.001f, 1.0f, maxScaleMultiplier);

		transform.localScale = initialScale * scaleMultiplier;
	}
}
