using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LibNoise;
using LibNoise.Generator;
using LibNoise.Operator;


public class EmptyTileGen : BaseClass
{
	[Header("Gameobjects")]
	public GameObject emptyTile;

	private Perlin perlin;
	private Noise2D heightMap;

	[Header("Generation")]
	public int updatesPerFrame;
	public bool isDone = false;

	public void Generate()
	{
		SetupHeightMap();
		StartCoroutine(GenerateEmptyTerrain());
	}

	private IEnumerator GenerateEmptyTerrain()
	{
		int updatesThisFrame	= 0;
		float randomFloat		= Random.Range(4.562f, 15.945f);

		// Centers the world
		float negativeCoord = -((worldSize / 2) * tileSize) + (worldSize % 2 == 0 ? tileSize / 2 : 0.0f);

		for (int y = 0; y < worldSize; y++)
		{
			for (int x = 0; x < worldSize; x++)
			{
				Vector3 pos = new Vector3(negativeCoord + (x * tileSize), 0, -(negativeCoord + (y * tileSize)));

				// Get height from the perlin noise
				double height = perlin.GetValue(pos.x * randomFloat, pos.y, pos.z * randomFloat);

				pos.y = (float)height;

				GameObject spawnedTile = Instantiate(emptyTile, pos, Quaternion.identity, emptyTilesParent.transform);

				// - - - - -

				// Wait to show generation
				if (updatesThisFrame == updatesPerFrame)
				{
					updatesThisFrame = 0;

					yield return new WaitForEndOfFrame();
				}

				++updatesThisFrame;
			}
		}

		isDone = true;
	}

	private void SetupHeightMap()
	{
		perlin		= new Perlin();
		heightMap	= new Noise2D(worldSize, worldSize, perlin);
	}
}
