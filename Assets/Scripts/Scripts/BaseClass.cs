using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass : MonoBehaviour
{
	public int worldSize { get => Controller.instance.worldSize; }
	public int tileSize { get => Controller.instance.tileSize; }
	public int numSettlements { get => Controller.instance.numSettlements; }
	public GameObject emptyTilesParent { get => Controller.instance.emptyTilesParent; }
	public GameObject riverTilesParent { get => Controller.instance.riverTilesParent; }
	public GameObject[] emptyTiles
	{
		get
		{
			GameObject[] children = new GameObject[emptyTilesParent.transform.childCount];

			for (int i = 0; i < emptyTilesParent.transform.childCount; i++)
				children[i] = emptyTilesParent.transform.GetChild(i).gameObject;

			return children;
		}
	}
	public List<GameObject> emptyTilesList { get => emptyTiles.ToList(); }
}
