using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _SettlementTile : MonoBehaviour
{
	public List<int> GetNeighbours()
	{
		GameObject[] allTiles = _Manager.inst.allTiles;
		int TILE_SIZE = _Manager.inst.TILE_SIZE;

		List<int> neighbours = new List<int>();

		Vector3 pos = transform.position;

		for (float x = pos.x - (TILE_SIZE * 1); x <= pos.x + (TILE_SIZE * 1); x += TILE_SIZE)
		{
			for (float z = pos.z - (TILE_SIZE * 1); z <= pos.z + (TILE_SIZE * 1); z += TILE_SIZE)
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
