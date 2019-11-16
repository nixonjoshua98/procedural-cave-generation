using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass : MonoBehaviour
{
	public Quaternion[] tileRotations =
	{
		Quaternion.Euler(0, 0, 0),
		Quaternion.Euler(0, 90, 0),
		Quaternion.Euler(0, 180, 0),
		Quaternion.Euler(0, 270, 0),
	};

	public int worldSize { get => Controller.instance.worldSize; }
	public int tileSize { get => Controller.instance.tileSize; }
	public int numSettlements { get => Controller.instance.numSettlements; }
	public int numMushPatches { get => Controller.instance.numMushPatches; }
	public GameObject emptyTilesParent { get => Controller.instance.emptyTilesParent; }
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
