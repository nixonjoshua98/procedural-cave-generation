﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationGenerator : MonoBehaviour
{
	public GameObject house;

    public void Generate(int worldWidth, int worldHeight, TerrainType[] terrainMap, Vector3[] vertices, int tileSize, int borderSize, GameObject parent)
    {
		for (int y = 0; y < worldHeight; y++)
		{
			for (int x = 0; x < worldWidth; x++)
			{
				// Border
				if (x < borderSize || x > worldWidth - borderSize || y < borderSize || y > worldHeight - borderSize)
					continue;

				int index = x + (y * worldWidth);

				TerrainType terrain = terrainMap[index];

				Vector3 v = vertices[index];

				if (terrain.name == "Grass")
				{
					v.x = (v.x * tileSize) + (tileSize / 2);
					v.z = (v.z * tileSize) - (tileSize / 2);

					float rand = Random.value;

					GameObject obj = null;

					if (rand <= 0.15f)
					{
						obj = Instantiate(house);

						v.y += obj.transform.localScale.y;
					}


					if (obj != null)
					{
						obj.transform.parent = parent.transform;

						obj.transform.position		= v;
						obj.transform.localScale	= new Vector3(1.0f, 32.0f, 1.0f);
					}
				}
			}
		}
	}
}