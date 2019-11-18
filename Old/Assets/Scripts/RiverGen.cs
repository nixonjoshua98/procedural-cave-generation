using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverGen : BaseClass
{
	[Header("Gameobjects")]
	public GameObject waterTile;
	public GameObject riverTilesParent;

	[Header("Generation")]
	public int updatesPerFrame;
	public bool isDone = false;

	public void Generate()
	{
		StartCoroutine(IGenerateRiver());
	}

	public IEnumerator IGenerateRiver()
	{
		int updatesThisFrame	= 0;
		GameObject[] tiles		= emptyTiles;
		GameObject currentTile	= tiles[Random.Range(0, tiles.Length)];
		List<int> riverTiles	= new List<int>();

		for (int i = 0; i < worldSize * 7;)
		{
			List<int> neighbourIndexes = GetNeighbours(currentTile, tileSize, ref tiles);

			if (neighbourIndexes.Count == 0)
			{
				currentTile = tiles[riverTiles[Random.Range(0, riverTiles.Count)]];

				continue;
			}

			foreach (int n in neighbourIndexes)
			{
				GameObject spawnedRiverTile			= Instantiate(waterTile, tiles[n].transform.position, Quaternion.identity);
				spawnedRiverTile.transform.parent	= riverTilesParent.transform;

				Destroy(tiles[n]);

				tiles[n] = spawnedRiverTile;

				riverTiles.Add(n);

				// - - - - -

				if (++updatesThisFrame % updatesPerFrame == 0)
					yield return new WaitForEndOfFrame();

				++i;
			}

			currentTile = tiles[neighbourIndexes[Random.Range(0, neighbourIndexes.Count)]];
		}

		isDone = true;
	}

	public List<int> GetNeighbours(GameObject centerTile, int tileSize, ref GameObject[] tiles)
	{
		List<int> neighbours = new List<int>();

		for (int j = 0; j < tiles.Length; j++)
		{
			if (!tiles[j].CompareTag("EmptyTile"))
				continue;

			Vector3 pos		= new Vector3(tiles[j].transform.position.x, 1.0f, tiles[j].transform.position.z);
			Vector3 pos2	= new Vector3(centerTile.transform.position.x, 1.0f, centerTile.transform.position.z);

			float dist = Vector3.Distance(pos2, pos);

			if (dist <= tileSize)
				neighbours.Add(j);
		}

		return neighbours;
	}
}
