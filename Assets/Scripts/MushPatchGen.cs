using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushPatchGen : BaseClass
{
	[Header("Gameobjects")]
	public GameObject mushroomTile;
	public GameObject muchroomPatchParent;

	[Header("Generation")]
	public int updatesPerFrame;
	public bool isDone = false;

	List<List<GameObject>> areas = new List<List<GameObject>>();

	public void Generate()
	{
		GetPotentialAreas();
		StartCoroutine(IGenerateMushroomAreas());
	}

	public IEnumerator IGenerateMushroomAreas()
	{
		int updatesThisFrame = 0;

		areas.Sort((p, q) => q.Count.CompareTo(p.Count));

		for (int i = 0; i < numSettlements; i++)
		{
			List<GameObject> muchPatch	= areas[i];
			GameObject parent			= Instantiate(muchroomPatchParent);

			parent.name = "Mushroom Patch " + i;

			foreach (GameObject tile in muchPatch)
			{
				GameObject newMushTile = Instantiate(mushroomTile, tile.transform.position, Quaternion.identity);

				newMushTile.transform.parent = parent.transform;

				Destroy(tile);

				if (++updatesThisFrame % updatesPerFrame == 0)
					yield return new WaitForEndOfFrame();
			}
		}

		isDone = true;
	}

	private void GetPotentialAreas()
	{
		List<GameObject> availableTiles = emptyTilesList;

		const int radius		= 1;
		int mushroomAreaIndex	= 0;

		bool isWorking = true;

		do
		{
			isWorking = false;

			int centerMushroomIndex		= Random.Range(0, availableTiles.Count);
			GameObject centerMushroom	= availableTiles[centerMushroomIndex];

			availableTiles.RemoveAt(centerMushroomIndex);

			areas.Add(new List<GameObject>() { centerMushroom });

			for (int i = 0; i < availableTiles.Count;)
			{
				GameObject tile = availableTiles[i];

				// Ignore Y
				Vector3 tilePos		= new Vector3(tile.transform.position.x,			1.0f, tile.transform.position.z);
				Vector3 centerPos	= new Vector3(centerMushroom.transform.position.x,	1.0f, centerMushroom.transform.position.z);

				float dist = Vector3.Distance(tilePos, centerPos);

				// Within range
				if (dist <= radius * tileSize)
				{
					availableTiles.RemoveAt(i);

					areas[mushroomAreaIndex].Add(tile);

					isWorking = true;
				}

				// Outside
				else
					i++;
			}

			++mushroomAreaIndex;

		} while (isWorking);
	}
}
