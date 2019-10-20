using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DioramaManager : MonoBehaviour
{
	public static DioramaManager inst = null;

	[HideInInspector] public int SEED;
	[HideInInspector] public int WORLD_SIZE;
	[HideInInspector] public int TOTAL_TILES;
	[HideInInspector] public int NUM_SETTLEMENTS;
	[HideInInspector] public int TILE_SIZE = 10;

	[HideInInspector] public GameObject[] allTiles;

	// Generators
	TileGenerator tileGen;

	private void Awake()
	{
		// Set the instance
		inst = this;

		// Grab components
		tileGen = GetComponent<TileGenerator>();

		// Seeding
		SEED = Random.Range(0, 999999);

		UnityEngine.Random.InitState(this.SEED);

		// Constants
		WORLD_SIZE		= Random.Range(4, 8);
		TOTAL_TILES		= this.WORLD_SIZE * WORLD_SIZE;
		NUM_SETTLEMENTS = Random.Range(1, WORLD_SIZE / 2);
	}

	private void Start()
	{
		allTiles = new GameObject[TOTAL_TILES];

		tileGen.Generate();
	}
}
