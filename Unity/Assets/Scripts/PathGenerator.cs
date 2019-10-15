using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
	[Header("Parent")]
	public GameObject terrainTilesParent;


	[Header("Paths")]
	public GameObject straightPathTile;

	public void v(ref GameObject[] allTiles)
	{

	}


	public void Generate(ref GameObject[] allTiles)
	{
		for (int y = 0; y < DioramaManager.inst.worldSize; y++)
		{
			for (int x = 0; x < DioramaManager.inst.worldSize; x++)
			{
				GameObject tile = allTiles[y * DioramaManager.inst.worldSize + x];

				if (tile.CompareTag("HouseTile"))
				{

				}

				//GameObject left  = GetNeighbour(ref allTiles, new Vector2Int(x, y), new Vector2Int(-1,  0));
				//GameObject right = GetNeighbour(ref allTiles, new Vector2Int(x, y), new Vector2Int( 1,  0));
				//GameObject up	 = GetNeighbour(ref allTiles, new Vector2Int(x, y), new Vector2Int( 0,  1));
				//GameObject down	 = GetNeighbour(ref allTiles, new Vector2Int(x, y), new Vector2Int( 0, -1));
			}
		}
	}

	public GameObject GetNeighbour(ref GameObject[] allTiles, Vector2Int tilePos, Vector2Int posOffset)
	{
		GameObject neighbour = null;

		int nTileX = tilePos.x + posOffset.x;
		int nTileY = tilePos.y + posOffset.y;

		bool validX = nTileX >= 0 && nTileX < DioramaManager.inst.worldSize;
		bool validY = nTileY >= 0 && nTileY < DioramaManager.inst.worldSize;

		if (validY && validX)
		{
			int index = nTileY * DioramaManager.inst.worldSize + nTileX;

			neighbour = allTiles[index];
		}

		return neighbour;
	}
}
