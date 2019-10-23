using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TileGenerator : MonoBehaviour
{
	[Header("Tiles")]
	public GameObject emptyTileOBJ;
	public GameObject settlementTileOBJ;

	public GameObject[] decorationTileOBJs;

	[Header("Parent Gameobjects")]
	public GameObject tilesParent;

	Quaternion[] tileRotations =
	{
		Quaternion.Euler(0, 0, 0),
		Quaternion.Euler(0, 90, 0),
		Quaternion.Euler(0, 180, 0),
		Quaternion.Euler(0, 270, 0),
	};

	public void Generate()
	{
		GenerateEmptyTiles();

		List<int> potentialIndexes;
		int numTiles;

		// Generate Settlement Tiles
		potentialIndexes			= GetAllEmptyTileIndexes();
		numTiles					= (int)Random.Range(2, Mathf.Min(Mathf.Sqrt(_Manager.inst.WORLD_SIZE), 7));
		GameObject[] settlementOBJs = { settlementTileOBJ };

		GenerateTiles(potentialIndexes, numTiles, settlementOBJs);

		// Generate Decoration Tiles
		potentialIndexes	= GetAllEmptyTileIndexes();
		numTiles			= (int)(0.25f * potentialIndexes.Count);

		GenerateTiles(potentialIndexes, numTiles, decorationTileOBJs, hasRotation: true);
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

	private void GenerateTiles(List<int> potentialIndexes, int numTiles, GameObject[] potentialOBJs, bool hasRotation = false)
	{
		GameObject[] allTiles = _Manager.inst.allTiles;

		for (int i = 0; i < numTiles; i++)
		{
			int rand = Random.Range(0, potentialIndexes.Count);

			GameObject tileToDestroy	= allTiles[potentialIndexes[rand]];
			GameObject tileToSpawn		= potentialOBJs[Random.Range(0, potentialOBJs.Length)];

			Vector3 spawnPos = tileToDestroy.transform.position;

			GameObject spawnedTile = Instantiate(tileToSpawn, spawnPos, Quaternion.identity);

			if (hasRotation)
				spawnedTile.transform.rotation = tileRotations[Random.Range(0, tileRotations.Length)];

			spawnedTile.transform.parent = tilesParent.transform;

			Destroy(tileToDestroy);
			allTiles[potentialIndexes[rand]] = spawnedTile;
			potentialIndexes.RemoveAt(rand);

			spawnedTile.GetComponent<_TileBaseClass>().Generate();
		}
	}

	private List<int> GetAllEmptyTileIndexes()
	{
		List<int> tiles = new List<int>();

		for (int i = 0; i < _Manager.inst.allTiles.Length; i++)
		{
			var t = _Manager.inst.allTiles[i];

			if (t.CompareTag("EmptyTile"))
				tiles.Add(i);
		}

		return tiles;
	}
}
