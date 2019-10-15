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

	public GameObject emptyTile;
	public GameObject houseTile;
	public GameObject errorTile;

	public void Generate(ref GameObject[] allTiles)
	{
		int WORLD_SIZE = DioramaManager.inst.worldSize;
		int TOTAL_HOUSES = DioramaManager.inst.totalHouses;
		int TOTAL_TILES = DioramaManager.inst.totalTiles;

		float negativeCoord = -((WORLD_SIZE / 2) * TILE_SIZE) + (WORLD_SIZE % 2 == 0 ? TILE_SIZE / 2 : 0.0f);
		Vector2Int[] houseCoordPairings = GetHouseTilePositions(WORLD_SIZE, TOTAL_HOUSES);

		for (int y = 0; y < WORLD_SIZE; y++)
		{
			for (int x = 0; x < WORLD_SIZE; x++)
			{
				GameObject tileToSpawn;

				Vector3 pos = new Vector3(negativeCoord + (x * TILE_SIZE), 0,  - (negativeCoord + (y * TILE_SIZE)));

				if (houseCoordPairings.Contains(new Vector2Int(x, y)))
					tileToSpawn = houseTile;

				else
					tileToSpawn = emptyTile;

				GameObject spawnedTile = Instantiate(tileToSpawn, pos, Quaternion.identity, terrainTilesParent.transform);

				allTiles[y * WORLD_SIZE + x] = spawnedTile;
			}
		}
	}

	private Vector2Int[] GetHouseTilePositions(int worldSize, int totalHouses)
	{
		// Horrible and slow way of doing this.

		List<Vector2Int> allPermutations = GenerateAllTilePermutations(worldSize);

		Vector2Int[] houseIndexes = new Vector2Int[totalHouses];

		for (int i = 0; i < totalHouses; i++)
		{
			int randNum = Random.Range(0, allPermutations.Count);

			Vector2Int v = allPermutations[randNum];

			allPermutations.RemoveAt(randNum);

			houseIndexes[i] = v;
		}

		return houseIndexes;
	}

	private List<Vector2Int> GenerateAllTilePermutations(int worldSize)
	{
		List<Vector2Int> allTilePermutations = new List<Vector2Int>();

		for (int i = 0; i < worldSize; i++)
		{
			for (int j = 0; j < worldSize; j++)
			{
				allTilePermutations.Add(new Vector2Int(i, j));
			}
		}

		return allTilePermutations;
	}
}
