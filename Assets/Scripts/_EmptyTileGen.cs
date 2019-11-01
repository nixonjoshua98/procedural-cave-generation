using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LibNoise;
using LibNoise.Generator;
using LibNoise.Operator;


public class _EmptyTileGen : MonoBehaviour
{
	public GameObject emptyTile;

	private Perlin perlin;
	private Noise2D heightMap;

	public void Generate(int worldSize, int tileSize, ref GameObject[] tiles)
	{
		SetupHeightMap(worldSize, tileSize);


		GameObject parent = GameObject.FindGameObjectWithTag("TilesParent");

		float randomFloat = Random.Range(4.562f, 15.945f);

		// Centers the world
		float negativeCoord = -((worldSize / 2) * tileSize) + (worldSize % 2 == 0 ? tileSize / 2 : 0.0f);

		for (int y = 0; y < worldSize; y++)
		{
			for (int x = 0; x < worldSize; x++)
			{
				Vector3 pos = new Vector3(negativeCoord + (x * tileSize), 0, -(negativeCoord + (y * tileSize)));

				double height = perlin.GetValue(pos.x / randomFloat, pos.y, pos.z / randomFloat);

				pos.y = (float)height;

				GameObject spawnedTile = Instantiate(emptyTile, pos, Quaternion.identity, parent.transform);

				tiles[y * worldSize + x] = spawnedTile;
			}
		}
	}

	private void SetupHeightMap(int worldSize, int tileSize)
	{
		perlin = new Perlin();

		heightMap = new Noise2D(worldSize, worldSize, perlin);
	}
}
