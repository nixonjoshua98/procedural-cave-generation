using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TileGenerator : MonoBehaviour
{
	[Header("Tiles")]
	public GameObject emptyTileOBJ;
	public GameObject settlementTileOBJ;

	[Header("Parent Gameobjects")]
	public GameObject tilesParent;

	public void Generate()
	{
		GenerateEmptyTiles();

		GenerateSettlementTiles();
	}

	private void GenerateEmptyTiles()
	{
		int WORLD_SIZE = _Manager.inst.WORLD_SIZE;
		int TILE_SIZE = _Manager.inst.TILE_SIZE;

		float negativeCoord = -((WORLD_SIZE / 2) * TILE_SIZE) + (WORLD_SIZE % 2 == 0 ? TILE_SIZE / 2 : 0.0f);

		for (int y = 0; y < WORLD_SIZE; y++)
		{
			for (int x = 0; x < WORLD_SIZE; x++)
			{
				GameObject tileToSpawn = emptyTileOBJ;

				Vector3 pos = new Vector3(negativeCoord + (x * TILE_SIZE), 0, -(negativeCoord + (y * TILE_SIZE)));

				GameObject spawnedTile = Instantiate(tileToSpawn, pos, Quaternion.identity, tilesParent.transform);

				_Manager.inst.allTiles[y * WORLD_SIZE + x] = spawnedTile;
			}
		}
	}

	private void GenerateSettlementTiles()
	{
		var allTiles = _Manager.inst.allTiles;

		List<int> potentialIndexes = new List<int>();
		for (int i = 0; i < allTiles.Length; i++) { potentialIndexes.Add(i); }

		int NUM_SETTLEMENTS = (int)Random.Range(2, Mathf.Min(Mathf.Sqrt(_Manager.inst.WORLD_SIZE), 7));

		for (int i = 0; i < NUM_SETTLEMENTS; i++)
		{
			int rand = Random.Range(0, potentialIndexes.Count);

			GameObject emptyTile = allTiles[rand];

			Vector3 spawnPos = emptyTile.transform.position;

			GameObject settlementTile = Instantiate(settlementTileOBJ, spawnPos, emptyTile.transform.rotation);

			settlementTile.transform.parent = tilesParent.transform;

			// Switch tiles and remove the index the settlement will spawn at.
			Destroy(emptyTile);
			allTiles[potentialIndexes[rand]] = settlementTile;
			potentialIndexes.RemoveAt(rand);
		}
	}
}
