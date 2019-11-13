using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Bonfire : _DynamicObject
{
	[Header("Gameobjects")]
	public GameObject plankOBJ;

	// Coords
	private int numLayers = 0;
	private float yValue;

	private void Start()
	{
		yValue = transform.position.y + (transform.localScale.y / 2) + (plankOBJ.transform.localScale.y / 2);

		for (int i = 0; i < Random.Range(9, 20); i++)
			AddLayer();
	}

	private void AddLayer()
	{
		float maxX = (transform.localScale.z / 2.0f) - (plankOBJ.transform.localScale.z / transform.localScale.z);
		float minX = maxX / 1.5f;

		float distaneFromCenter = Random.Range(minX, maxX);
		float yScaleMultiplier	= Random.Range(1.5f, 2.5f);

		for (int i = -1; i <= 1; i += 2)
		{
			GameObject plank = Instantiate(plankOBJ, transform.position, Quaternion.identity);

			Vector3 plankPos	= plank.transform.position;
			plankPos.y			= yValue;

			// Add plank wiggle
			plank.transform.rotation = Quaternion.Euler(0, Random.Range(-15, 15), 0);

			// Rotate the plank so they can stack
			if (numLayers % 2 == 1)
			{
				plank.transform.rotation	= Quaternion.Euler(0, plank.transform.rotation.y + 90.0f, 0);
				plankPos.z					= distaneFromCenter * i;
			}
			else
				plankPos.x = distaneFromCenter * i;

			Vector3 scale = plankOBJ.transform.localScale;

			scale.y *= yScaleMultiplier;

			plank.transform.position	= plankPos;
			plank.transform.localScale	= scale;
			plank.transform.parent		= transform;
		}

		yValue += plankOBJ.transform.localScale.y;

		numLayers++;
	}

	public override void Generate(int worldSize, int tileSize, ref GameObject[] tiles)
	{
		yValue = transform.position.y + (transform.localScale.y / 2) + (plankOBJ.transform.localScale.y / 2);

		for (int i = 0; i < Random.Range(9, 20); i++)
			AddLayer();
	}
}
