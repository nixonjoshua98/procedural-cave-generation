using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SettlementGen : BaseClass
{
	[Header("Gameobjects")]
	public GameObject settlementTile;
	public GameObject settlementParent;

	[Header("Generation")]
	public int updatesPerFrame;
	public bool isDone = false;

	List<List<GameObject>> settlements = new List<List<GameObject>>();


	public void Generate()
    {
		GetPotentialSettlements();
		StartCoroutine(IGenerateSettlements());
	}

	public IEnumerator IGenerateSettlements()
	{
		int updatesThisFrame = 0;

		settlements.Sort((p, q) => q.Count.CompareTo(p.Count));

		for (int i = 0; i < numSettlements; i++)
		{
			List<GameObject> settlement = settlements[i];
			GameObject parent			= Instantiate(settlementParent);

			parent.name = "Settlement " + i;

			foreach (GameObject tile in settlement)
			{
				GameObject newSettlementTile		= Instantiate(settlementTile, tile.transform.position, Quaternion.identity);
				newSettlementTile.transform.parent	= parent.transform;

				Destroy(tile);

				if (++updatesThisFrame % updatesPerFrame == 0)
					yield return new WaitForEndOfFrame();
			}
		}

		isDone = true;
	}

	private void GetPotentialSettlements()
	{
		List<GameObject> availableTiles = emptyTilesList;

		const int radius	= 5;
		int settlementIndex = 0;

		bool isWorking = true;

		do
		{
			isWorking = false;

			int settlementCenterIndex	= Random.Range(0, availableTiles.Count);
			GameObject settlementCenter = availableTiles[settlementCenterIndex];

			availableTiles.RemoveAt(settlementCenterIndex);

			settlements.Add(new List<GameObject>() { settlementCenter });

			for (int i = 0; i < availableTiles.Count;)
			{
				GameObject tile = availableTiles[i];

				// ignore Y
				Vector3 tilePos = new Vector3(tile.transform.position.x, 1.0f, tile.transform.position.z);
				Vector3 centerPos = new Vector3(settlementCenter.transform.position.x, 1.0f, settlementCenter.transform.position.z);

				float dist = Vector3.Distance(tilePos, centerPos);

				// Within range
				if (dist <= radius * tileSize)
				{
					availableTiles.RemoveAt(i);

					settlements[settlementIndex].Add(tile);

					isWorking = true;
				}
				// Outside
				else
					i++;
			}

			++settlementIndex;

		} while (isWorking);
	}
}
