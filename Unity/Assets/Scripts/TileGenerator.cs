using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
	// Constants
	const int TILE_SIZE = 10;

	[Header("Tiles")]
	public GameObject terrainTilesParent;

	public GameObject emptyTileObj;
	public GameObject settlementTileObj;

	public void Generate()
	{
		SpawnEmptyTiles();
		SpawnSettlements();
	}

	private void SpawnEmptyTiles()
	{
		int WORLD_SIZE = DioramaManager.inst.WORLD_SIZE;

		GameObject[] allTiles = DioramaManager.inst.allTiles;

		// This centers the tiles in the scene
		float negativeCoord = -((WORLD_SIZE / 2) * TILE_SIZE) + (WORLD_SIZE % 2 == 0 ? TILE_SIZE / 2 : 0.0f);

		for (int y = 0; y < WORLD_SIZE; y++)
		{
			for (int x = 0; x < WORLD_SIZE; x++)
			{
				GameObject tileToSpawn = emptyTileObj;

				Vector3 pos = new Vector3(negativeCoord + (x * TILE_SIZE), 0, -(negativeCoord + (y * TILE_SIZE)));

				GameObject spawnedTile = Instantiate(tileToSpawn, pos, Quaternion.identity, terrainTilesParent.transform);

				allTiles[y * WORLD_SIZE + x] = spawnedTile;
			}
		}
	}

	private void SpawnSettlements()
	{
		GameObject[] allTiles = DioramaManager.inst.allTiles;

		for (int i = 0; i < DioramaManager.inst.NUM_SETTLEMENTS; i++)
		{
			int rand = Random.Range(0, allTiles.Length);

			GameObject emptyTile = allTiles[rand];

			Vector3 spawnPos = emptyTile.transform.position;

			GameObject settlementTile = Instantiate(settlementTileObj, spawnPos, emptyTile.transform.rotation);

			Destroy(emptyTile);

			allTiles[rand] = settlementTile;
		}
	}
}