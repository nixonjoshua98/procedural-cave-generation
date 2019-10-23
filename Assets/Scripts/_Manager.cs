using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Manager : MonoBehaviour
{
	public static _Manager inst = null;

	[HideInInspector] public int SEED;
	[HideInInspector] public int WORLD_SIZE;
	[HideInInspector] public int TOTAL_TILES;

	private const bool _DEBUG = true;
	private const int _TILE_SIZE = 5;

	[HideInInspector] public int TILE_SIZE { get => _TILE_SIZE; }
	[HideInInspector] public bool DEBUG { get => _DEBUG; }

	[HideInInspector] public GameObject[] allTiles;

	// Generators
	_TileGenerator tileGen;


	private void Awake()
	{
		inst = this;

		tileGen = GetComponent<_TileGenerator>();

		SEED = 13;

		Random.InitState(this.SEED);

		WORLD_SIZE = 16;
		TOTAL_TILES = (int) Mathf.Pow(this.WORLD_SIZE, 2);
	}

	private void Start()
	{
		allTiles = new GameObject[TOTAL_TILES];

		tileGen.Generate();

		if (!DEBUG && allTiles.Length > 0)
			Destroy(allTiles[0].transform.parent.gameObject);
	}
}
