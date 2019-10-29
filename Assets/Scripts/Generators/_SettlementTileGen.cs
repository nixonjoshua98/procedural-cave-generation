using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _SettlementTileGen : MonoBehaviour
{
	public GameObject settlementTile;
	public GameObject houseTile;

	public GameObject[] houses;

	public void Generate(int worldSize, int tileSize, ref GameObject[] tiles)
	{
		//int NUM_SETTLEMENTS = Random.Range(5, (worldSize % 128) * 5);
		int NUM_SETTLEMENTS = Random.Range(5, 13);

		Debug.Log("SETTLEMENTS: " + NUM_SETTLEMENTS);

		int houseModelIndex = 0;

		foreach (KeyValuePair<int, List<int>> row in GetSettlementPositions(NUM_SETTLEMENTS, tileSize, 7, ref tiles))
		{
			var tile = Instantiate(settlementTile, tiles[row.Key].transform.position, Quaternion.identity, tiles[row.Key].transform.parent);

			Destroy(tiles[row.Key]);

			tiles[row.Key] = tile;

			foreach (int n in row.Value)
			{
				GameObject settlement, house;

				if (Random.Range(0, 100) <= 25)
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
		List<int> uncheckedIndexes = new List<int>();
		for (int i = 0; i < tiles.Length; i++)
			uncheckedIndexes.Add(i);

		var settlements = new Dictionary<int, List<int>>();

		for (int i = 0; i < numSettlements; i++)
		{
			if (uncheckedIndexes.Count == 0) break;

			int rand = Random.Range(0, uncheckedIndexes.Count);
			int index = uncheckedIndexes[rand];

			uncheckedIndexes.RemoveAt(rand);

			Vector3 settlementPos = new Vector3(tiles[index].transform.position.x, 1.0f, tiles[index].transform.position.z);

			settlements.Add(index, new List<int>());

			for (int j = 0; j < uncheckedIndexes.Count;)
			{
				int _index = uncheckedIndexes[j];
				Vector3 pos = new Vector3(tiles[_index].transform.position.x, 1.0f, tiles[_index].transform.position.z);

				float dist = Vector3.Distance(settlementPos, pos);

				if (dist <= tileSize * (neighbourRadius * 2))
				{
					uncheckedIndexes.RemoveAt(j);

					if (dist <= tileSize * neighbourRadius)
					{
						settlements[index].Add(_index);
					}

					continue;
				}

				j++;
			}
		}

		return settlements;
	}
}
