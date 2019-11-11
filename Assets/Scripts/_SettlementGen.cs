using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _SettlementGen : MonoBehaviour
{
	public GameObject settlementTileOBJ;
    public GameObject settlementCenterTileOBJ;
	public GameObject houseTileOBJ;

	public GameObject[] houses;

	public void Generate(int worldSize, int tileSize, ref GameObject[] tiles)
	{
		int NUM_SETTLEMENTS = Random.Range(5, 7);

		Debug.Log("MAX SETTLEMENTS: " + NUM_SETTLEMENTS);

		int houseModelIndex = 0;

		foreach (KeyValuePair<int, List<int>> row in GetSettlementPositions(NUM_SETTLEMENTS, tileSize, 6, ref tiles))
		{
            Vector3 settlementCenter        = tiles[row.Key].transform.position;
            GameObject settlementCenterTile = Instantiate(settlementCenterTileOBJ, settlementCenter, Quaternion.identity, tiles[row.Key].transform.parent);

			Destroy(tiles[row.Key]);

			tiles[row.Key] = settlementCenterTile;

            GenerateSettlementHouses(settlementCenter, houses[houseModelIndex], ref tiles, row.Value);

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

			Vector3 settlementCenter = new Vector3(tiles[index].transform.position.x, 1.0f, tiles[index].transform.position.z);

			settlements.Add(index, new List<int>());

			for (int j = 0; j < availIndexes.Count;)
			{
				int _index = availIndexes[j];
				Vector3 pos = new Vector3(tiles[_index].transform.position.x, 1.0f, tiles[_index].transform.position.z);

				float dist = Vector3.Distance(settlementCenter, pos);

				if (dist <= tileSize * (neighbourRadius * 3))
				{
					availIndexes.RemoveAt(j);

					if (dist <= tileSize * neighbourRadius)
						settlements[index].Add(_index);

					continue;
				}

				j++;
			}

			i++;
		}

		return settlements;

	}

    private void GenerateSettlementHouses(Vector3 centerPos, GameObject houseObj, ref GameObject[] tiles, List<int> neighbourIndexes)
    {
        // Rest of settlement
        foreach (int n in neighbourIndexes)
        {
            GameObject settlementTile, houseObject;

            // 25% to spawn a house
            if (Random.Range(0, 100) <= 25)
            {
                settlementTile  = Instantiate(houseTileOBJ, tiles[n].transform.position, Quaternion.identity, tiles[n].transform.parent);
                houseObject     = Instantiate(houseObj, settlementTile.transform.position, Quaternion.identity);

                Vector3 houseObjectPos  = houseObject.transform.position;
                houseObjectPos.y        = settlementTile.transform.position.y + (settlementTile.transform.localScale.y / 2) + (houseObject.transform.localScale.y / 2);

                houseObject.transform.position  = houseObjectPos;
                houseObject.transform.parent    = settlementTile.transform;

                // Rotation
                Vector3 lookPos                 = centerPos - houseObject.transform.position;
                lookPos.y                       = 0.0f;
                Quaternion rotation             = Quaternion.LookRotation(lookPos);
                float rotatePercent             = Random.Range(0.0f, 1.0f);
                houseObject.transform.rotation  = Quaternion.Slerp(houseObject.transform.rotation, rotation, rotatePercent);

            }
            else
                settlementTile = Instantiate(settlementTileOBJ, tiles[n].transform.position, Quaternion.identity, tiles[n].transform.parent);

            Destroy(tiles[n]);

            tiles[n] = settlementTile;
        }
    }
}
