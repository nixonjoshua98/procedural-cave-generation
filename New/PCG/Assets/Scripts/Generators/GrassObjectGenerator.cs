using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassObjectGenerator : MonoBehaviour
{
	public GameObject[] objects;

	public GameObject lamp;

	public void Generate(int worldWidth, int worldHeight, TerrainType[] terrainMap, Vector3[] vertices, int borderSize, GameObject parent)
	{
		for (int y = 0; y < worldHeight; y++)
		{
			for (int x = 0; x < worldWidth; x++)
			{
				bool isBorder		= x < borderSize || x > worldWidth - borderSize || y < borderSize || y > worldHeight - borderSize;
				int index			= x + (y * worldWidth);
				TerrainType region	= terrainMap[index];
				Vector3 v			= vertices[index];

				float rand = Random.value;

				v.x += 0.5f;
				v.z -= 0.5f;

				if (region.name == "Grass 0")
				{
					GameObject _object = null;

					if (rand <= 0.0025f)
						_object = Instantiate(lamp, parent.transform);

					else if (rand <= 0.25f)
						_object = Instantiate(objects[Random.Range(0, objects.Length)], parent.transform);

					else
						continue;

					_object.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360.0f), 0.0f);
					_object.transform.position = v;
				}
			}
		}
	}
}
