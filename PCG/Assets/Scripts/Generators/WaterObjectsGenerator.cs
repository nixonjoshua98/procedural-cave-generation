using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterObjectsGenerator : MonoBehaviour
{
	public GameObject[] waterObjects;
	public GameObject[] watersideObjects;

	public void Generate(int worldWidth, int worldHeight, TerrainType[] terrainMap, Vector3[] vertices, int borderSize, GameObject parent)
	{
		for (int y = 0; y < worldHeight; y++)
		{
			for (int x = 0; x < worldWidth; x++)
			{
				bool isBorder			= x < borderSize || x > worldWidth - borderSize || y < borderSize || y > worldHeight - borderSize;
				int index					= x + (y * worldWidth);
				TerrainType region	= terrainMap[index];
				Vector3 v				= vertices[index];

				float rand = Random.value;

				v.x += 0.5f;
				v.z -= 0.5f;

				if (isBorder)
					continue;

				if (region.name == "Water 0")
				{
					if (rand <= 0.03f)
					{
						GameObject _object = Instantiate(waterObjects[Random.Range(0, waterObjects.Length)], parent.transform);

						_object.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360.0f), 0.0f);

						_object.transform.position = v;
					}
				}

				else if (region.name == "Water 1")
				{
					if (rand <= 0.05f)
					{
						GameObject _object = Instantiate(watersideObjects[Random.Range(0, watersideObjects.Length)], parent.transform);

						_object.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360.0f), 0.0f);

						_object.transform.position = v;
					}
				}
			}
		}
	}
}
