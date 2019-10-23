using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _SettlementTile : _TileBaseClass
{
	public GameObject houseTileOBJ;

	public override void Generate()
	{
		List<int> allNeighbours = GetAllNeighbours(2);

		int spawnNumHouses = (int) Random.Range(allNeighbours.Count * 0.25f, allNeighbours.Count * 0.65f);

		List<int> neighbourHouses = new List<int>();

		// Get a certain amount of houses to spawn
		for (int i = 0; i < spawnNumHouses; i++)
		{
			int rand = Random.Range(0, allNeighbours.Count);

			neighbourHouses.Add(allNeighbours[rand]);

			allNeighbours.RemoveAt(rand);
		}

		allNeighbours.Clear();	// Remove them from memory

		foreach (int n in neighbourHouses)
		{

			GameObject tile = _Manager.inst.allTiles[n];

			GameObject houseTile = Instantiate(houseTileOBJ, tile.transform.position, tile.transform.rotation);

			houseTile.transform.parent = tile.transform.parent;

			Destroy(tile);

			_Manager.inst.allTiles[n] = houseTile;

			houseTile.GetComponent<_HouseTile>().Generate(transform.position);

		}
	}

	private List<int> GetAllNeighbours(int radius)
	{
		GameObject[] allTiles = _Manager.inst.allTiles;
		int TILE_SIZE = _Manager.inst.TILE_SIZE;

		List<int> neighbours = new List<int>();

		Vector3 pos = transform.position;

		for (float x = pos.x - (TILE_SIZE * radius); x <= pos.x + (TILE_SIZE * radius); x += TILE_SIZE)
		{
			for (float z = pos.z - (TILE_SIZE * radius); z <= pos.z + (TILE_SIZE * radius); z += TILE_SIZE)
			{
				Vector3 v = new Vector3(x, 0.0f, z);

				if (v == pos) continue;

				int neighbour = FindObjectIndexInArray(allTiles, v);

				if (neighbour != -1)
					neighbours.Add(neighbour);
			}
		}

		return neighbours;
	}

	private int FindObjectIndexInArray(GameObject[] allTiles, Vector3 pos)
	{
		for (int i = 0; i < allTiles.Length; i++)
		{
			if ( allTiles[i].transform.position == pos && allTiles[i].CompareTag("EmptyTile") )
				return i;
		}

		return -1;
	}
}
