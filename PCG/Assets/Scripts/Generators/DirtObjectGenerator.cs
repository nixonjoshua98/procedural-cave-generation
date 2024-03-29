﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtObjectGenerator : MonoBehaviour
{
	private Color[] colors = new Color[] { Color.red, Color.green, Color.yellow, Color.blue };

	public GameObject[] objects;
	public void Generate(int worldWidth, int worldHeight, TerrainType[] regionArray, Vector3[] vertices, int borderSize, GameObject parent)
	{
		int _ = 0;

		for (int y = 0; y < worldHeight; y++)
		{
			for (int x = 0; x < worldWidth; x++)
			{
				bool isBorder		= x < borderSize || x > worldWidth - borderSize || y < borderSize || y > worldHeight - borderSize;
				int index			= x + (y * worldWidth);
				TerrainType region	= regionArray[index];
				Vector3 v			= vertices[index];

				float rand = Random.value;

				v.x += 0.5f;
				v.z -= 0.5f;

				Quaternion rot = Quaternion.Euler(0, Random.Range(0, 360.0f), 0.0f);

				if (isBorder)
					continue;

				if (region.name == "Dirt 0")
				{
					if (rand <= 0.1f)
					{
						GameObject _object = Instantiate(objects[Random.Range(0, objects.Length)], v, rot, parent.transform);

						if (_object.CompareTag("Mushroom"))
						{
							GameObject head = _object.transform.GetChild(1).gameObject;

							head.GetComponent<MeshRenderer>().material.color = colors[Random.Range(0, colors.Length)];

							_object.GetComponent<Mushroom>().Create(index);
						}
					}
				}
			}
		}
	}
}
