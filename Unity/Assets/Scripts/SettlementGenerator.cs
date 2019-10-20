using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SettlementGenerator : MonoBehaviour
{

	public GameObject houseTileObj;

	public void Start()
	{
		Generate();
	}

	private void Generate()
	{
		List<int> neighbours = GetNeighbours();

		GameObject[] allTiles = DioramaManager.inst.allTiles;

		foreach (int n in neighbours)
		{
			GameObject tile = allTiles[n];

			if (!tile.CompareTag("EmptyTile")) continue;

			GameObject houseTile = Instantiate(houseTileObj, tile.transform.position, tile.transform.rotation);

			Destroy(tile);

			allTiles[n] = houseTile;

			houseTile.GetComponent<HouseGenerator>().Generate(transform.position);
		}
	}

	private List<int> GetNeighbours()
	{
		GameObject[] allTiles	= DioramaManager.inst.allTiles;
		int TILE_SIZE			= DioramaManager.inst.TILE_SIZE;

		List <int> neighbours = new List<int>();

		Vector3 pos = transform.position;

		for (float x = pos.x - TILE_SIZE; x <= pos.x + TILE_SIZE; x += TILE_SIZE)
		{
			for (float z = pos.z - TILE_SIZE; z <= pos.z + TILE_SIZE; z += TILE_SIZE)
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
			if (allTiles[i].transform.position == pos)
				return i;
		}

		return -1;
	}
}
