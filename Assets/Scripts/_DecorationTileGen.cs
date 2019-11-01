using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _DecorationTileGen : MonoBehaviour
{
	public GameObject decorationTile;

	public void Generate(int worldSize, int tileSize, ref GameObject[] tiles)
	{
		List<int> emptyTiles = GetEmptyTiles(ref tiles);
		int numDecorations = (int) (emptyTiles.Count * 0.25f);

		for (int i = 0; i < numDecorations; i++)
		{
			int rand = Random.Range(0, emptyTiles.Count);

			int tilesIndex = emptyTiles[rand];

			GameObject emptyTile = tiles[tilesIndex];

			GameObject decoTile = Instantiate(decorationTile, emptyTile.transform.position, Quaternion.identity);

			decoTile.transform.parent = emptyTile.transform.parent;

			Destroy(emptyTile);

			tiles[tilesIndex] = decoTile;

			emptyTiles.RemoveAt(rand);
		}
	}

	private List<int> GetEmptyTiles(ref GameObject[] tiles)
	{
		List<int> emptyTiles = new List<int>();

		for (int i = 0; i < tiles.Length; i++)
		{
			if ( tiles[i].CompareTag("EmptyTile") )
				emptyTiles.Add(i);
		}

		return emptyTiles;
	}
}
