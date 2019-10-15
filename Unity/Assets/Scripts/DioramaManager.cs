using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DioramaManager : MonoBehaviour
{
	public static DioramaManager inst = null;

	[HideInInspector] public int seed;
	[HideInInspector] public int worldSize;
	[HideInInspector] public int totalHouses;
	[HideInInspector] public int totalTiles;
	[HideInInspector] public int tileSize = 10;

	// Generators
	TileGenerator tileGen;
	PathGenerator pathGen;

	private void Awake()
	{
		if (inst)
			Destroy(this);
		else
			inst = this;

		tileGen = GetComponent<TileGenerator>();
		pathGen = GetComponent<PathGenerator>();
	}

	private void Start()
	{
		seed = Random.Range(0, 999999);

		UnityEngine.Random.InitState(this.seed);

		worldSize = Random.Range(5, 15);
		totalTiles = this.worldSize * worldSize;
		totalHouses = (int)Random.Range(5, Mathf.Min(totalTiles * 0.25f, 20));

		GameObject[] allTiles = new GameObject[totalTiles];

		tileGen.Generate(ref allTiles);
		pathGen.Generate(ref allTiles);
	}
}
