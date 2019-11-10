using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _RiverGen : MonoBehaviour
{
	public GameObject waterTile;


    public void Generate(int worldSize, int tileSize, ref GameObject[] tiles)
    {
		GameObject currentTile = GetStartObject(worldSize, tileSize, ref tiles);

		List<int> riverTiles = new List<int>();

		for (int i = 0; i < worldSize * 6; i++)
		{
			List<int> neighbourIndexes = GetNeighbours(currentTile, tileSize, ref tiles);

			GameObject spawnedTile = null;

			if (neighbourIndexes.Count == 0)
			{
				currentTile = tiles[riverTiles[Random.Range(0, riverTiles.Count)]];

				continue;
			}

			foreach (int n in neighbourIndexes)
			{
				spawnedTile = Instantiate(waterTile, tiles[n].transform.position, Quaternion.identity);

				spawnedTile.transform.parent = tiles[n].transform.parent;

				Destroy(tiles[n]);

				tiles[n] = spawnedTile;

				riverTiles.Add(n);
			}

			currentTile = tiles[ neighbourIndexes [ Random.Range( 0, neighbourIndexes.Count ) ] ];
		}


	}

	public GameObject GetStartObject(int worldSize, int tileSize, ref GameObject[] tiles)
	{
		List<GameObject> outerTiles = new List<GameObject>();

		for (int i = 0; i < tiles.Length; i++)
		{
			if (tiles[i].CompareTag("EmptyTile"))
				outerTiles.Add(tiles[i]);
		}

		return outerTiles[Random.Range(0, outerTiles.Count)];
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
