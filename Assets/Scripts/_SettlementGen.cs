using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _SettlementGen : MonoBehaviour
{
	public GameObject settlementTile;
	public GameObject houseTile;

	public GameObject[] houses;

	public void Generate(int worldSize, int tileSize, ref GameObject[] tiles)
	{
		int NUM_SETTLEMENTS = Random.Range(5, 8);

		Debug.Log("MAX SETTLEMENTS: " + NUM_SETTLEMENTS);

		int houseModelIndex = 0;

		foreach (KeyValuePair<int, List<int>> row in GetSettlementPositions(NUM_SETTLEMENTS, tileSize, 5, ref tiles))
		{
			var tile = Instantiate(settlementTile, tiles[row.Key].transform.position, Quaternion.identity, tiles[row.Key].transform.parent);

			Destroy(tiles[row.Key]);

			tiles[row.Key] = tile;

			foreach (int n in row.Value)
			{
				GameObject settlement, house;

				// 40% to spawn a house
				if (Random.Range(0, 100) <= 40)
				{
					settlement = Instantiate(houseTile, tiles[n].transform.position, Quaternion.identity, tiles[n].transform.parent);

					house = Instantiate(houses[houseModelIndex], settlement.transform.position, Quaternion.identity);

					Vector3 pos = house.transform.position;

					pos.y = settlement.transform.position.y + (settlement.transform.localScale.y / 2) + (house.transform.localScale.y / 2);

					house.transform.position	= pos;
					house.transform.parent		= settlement.transform;

				}
				else
					settlement = Instantiate(settlementTile, tiles[n].transform.position, Quaternion.identity, tiles[n].transform.parent);

				Destroy(tiles[n]);

				tiles[n] = settlement;
			}

			houseModelIndex = ++houseModelIndex % houses.Length;
		}
	}

	/*
	 * Key		|		<int> : Settlement Centre
	 * Value	|	List<int> : Neighbours
	 */
	private Dictionary<int, List<int>> GetSettlementPositions(int numSettlements, int tileSize, int neighbourRadius, ref GameObject[] tiles)
	{
		List<int> availIndexes = new List<int>();
		for (int i = 0; i < tiles.Length; i++)
		{
			if (tiles[i].CompareTag("EmptyTile"))
				availIndexes.Add(i);
		}

		var settlements = new Dictionary<int, List<int>>();

		for (int i = 0; i < numSettlements;)
		{
			if (availIndexes.Count == 0) break;

			int rand = Random.Range(0, availIndexes.Count);
			int index = availIndexes[rand];

			availIndexes.RemoveAt(rand);

			Vector3 settlementPos = new Vector3(tiles[index].transform.position.x, 1.0f, tiles[index].transform.position.z);

			settlements.Add(index, new List<int>());

			for (int j = 0; j < availIndexes.Count;)
			{
				int _index = availIndexes[j];
				Vector3 pos = new Vector3(tiles[_index].transform.position.x, 1.0f, tiles[_index].transform.position.z);

				float dist = Vector3.Distance(settlementPos, pos);

				if (dist <= tileSize * (neighbourRadius * 4))
				{
					availIndexes.RemoveAt(j);

					if (dist <= tileSize * neighbourRadius)
					{
						settlements[index].Add(_index);
					}

					continue;
				}

				j++;
			}

			i++;
		}

		return settlements;
	}
}
