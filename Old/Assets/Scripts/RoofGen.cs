using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LibNoise;
using LibNoise.Generator;
using LibNoise.Operator;


public class RoofGen : BaseClass
{
	[Header("Gameobjects")]
	public GameObject roofTile;

	private Perlin perlin;
	private Noise2D heightMap;

	[Header("Generation")]
	public int updatesPerFrame;
	public bool isDone = false;
    public void Generate()
	{
		SetupHeightMap();
		StartCoroutine(IGenerateRoof());
	}

	private IEnumerator IGenerateRoof()
	{
		int updatesThisFrame = 0;
		float randomFloat = Random.Range(4.562f, 15.945f);

		// Centers the world
		float negativeCoord = -((worldSize / 2) * tileSize) + (worldSize % 2 == 0 ? tileSize / 2 : 0.0f);

		for (int y = 0; y < worldSize; y++)
		{
			for (int x = 0; x < worldSize; x++)
			{
				Vector3 pos = new Vector3(negativeCoord + (x * tileSize), 0, -(negativeCoord + (y * tileSize)));

				// Get height from the perlin noise
				double height = perlin.GetValue(pos.x * randomFloat, pos.y, pos.z * randomFloat);

				pos.y = (float)height + 32.0f;

				GameObject spawnedTile = Instantiate(roofTile, pos, Quaternion.identity, roofTilesParent.transform);

				// Wait to show generation
				if (updatesThisFrame++ % updatesPerFrame == 0)
					yield return new WaitForEndOfFrame();
			}
		}

		isDone = true;
	}

	private void SetupHeightMap()
	{
		perlin = new Perlin(0.1, 0, 0, 0, Random.seed, QualityMode.High);

		heightMap = new Noise2D(worldSize, worldSize, perlin);
	}
}
