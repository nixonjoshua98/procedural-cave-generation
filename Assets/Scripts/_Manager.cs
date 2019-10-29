using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Manager : MonoBehaviour
{
	public static _Manager inst = null;

	[HideInInspector] public int SEED;
	[HideInInspector] public int WORLD_SIZE;
	[HideInInspector] public int TOTAL_TILES;

	private const int _TILE_SIZE = 3;

	[HideInInspector] public int TILE_SIZE { get => _TILE_SIZE; }

	[HideInInspector] public GameObject[] tiles;

	// Generators
	_EmptyTileGen emptyTileGen;
	_SettlementTileGen settleTileGen;


	private void Awake()
	{
		inst = this;

		emptyTileGen = GetComponent<_EmptyTileGen>();
		settleTileGen = GetComponent<_SettlementTileGen>();

		SEED = Random.Range(1, 99999);

		Debug.Log("SEED: " + SEED);

		Random.InitState(this.SEED);

		WORLD_SIZE = Random.Range(64, 128);

		Debug.Log("WORLD SIZE: " + WORLD_SIZE);

		TOTAL_TILES = (int) Mathf.Pow(this.WORLD_SIZE, 2);
	}

	private void Start()
	{
		tiles = new GameObject[TOTAL_TILES];

		emptyTileGen.Generate(WORLD_SIZE, TILE_SIZE, ref tiles);
		settleTileGen.Generate(WORLD_SIZE, TILE_SIZE, ref tiles);
	}
}
