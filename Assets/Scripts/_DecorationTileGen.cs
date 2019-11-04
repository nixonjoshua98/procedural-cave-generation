using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _DecorationTileGen : MonoBehaviour
{
	public GameObject[] decorationTiles;
	public GameObject[] decorations;

	private Quaternion[] rotations = 
	{
		Quaternion.Euler(0,		0,		0),
		Quaternion.Euler(0,		90,		0),
		Quaternion.Euler(0,		180,	0),
		Quaternion.Euler(0,		270,	0),
	};


	public void Generate(int worldSize, int tileSize, ref GameObject[] tiles)
	{
		int decorationTileIndex = 0;
		List<int> emptyTiles	= GetEmptyTiles(ref tiles);
		int numDecorations		= (int) (emptyTiles.Count * 0.15f);

		for (int i = 0; i < numDecorations; i++)
		{
			int rand		= Random.Range(0, emptyTiles.Count);
			int tilesIndex	= emptyTiles[rand];

			GameObject emptyTile	= tiles[tilesIndex];
			GameObject decoTile		= Instantiate(decorationTiles[decorationTileIndex], emptyTile.transform.position, Quaternion.identity);

			decoTile.transform.parent	= emptyTile.transform.parent;
			decoTile.transform.rotation = rotations[Random.Range(0, rotations.Length)];

			Destroy(emptyTile);

			tiles[tilesIndex] = decoTile;

			emptyTiles.RemoveAt(rand);

			GenerateDecorations(decoTile);

			decorationTileIndex = ++decorationTileIndex % decorationTiles.Length;
		}
	}

	private void GenerateDecorations(GameObject tile)
	{
		int childCount = tile.transform.childCount;

		for (int i = 0; i < childCount; i++)
		{
			GameObject slot = tile.transform.GetChild(i).gameObject;

			GameObject deco;

			deco = Instantiate(decorations[Random.Range(0, decorations.Length)], slot.transform.position, tile.transform.rotation);

			deco.transform.parent = tile.transform;

			Destroy(slot.gameObject);
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
