using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _MushroomObj : MonoBehaviour
{
	float maxScale;

	private void Start()
	{
		maxScale = Random.Range(0.25f, 0.6f);
	}

	private void FixedUpdate()
	{
		Vector3 scale = transform.localScale;

		if (scale.x >= maxScale) return;

		scale *= 1.0f + (0.1f * Time.fixedDeltaTime);

		transform.localScale = scale;
	}
}
