using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Manager : MonoBehaviour
{
	public int SEED;
	[HideInInspector] public int WORLD_SIZE;
	[HideInInspector] public int TOTAL_TILES;

	private const int _TILE_SIZE = 3;

	[HideInInspector] public int TILE_SIZE { get => _TILE_SIZE; }
	[HideInInspector] public GameObject[] tiles;

	// Generators
	EmptyTileGen		emptyTileGen;
	_SettlementGen		settlementGen;
	_DecorationTileGen	decoGen;
    _DynamicTileGen     dyanmicTileGen;
    _RiverGen           riverGen;

	private void Awake()
	{
		emptyTileGen	= GetComponent<EmptyTileGen>();
		settlementGen	= GetComponent<_SettlementGen>();
		decoGen			= GetComponent<_DecorationTileGen>();
        dyanmicTileGen  = GetComponent<_DynamicTileGen>();
        riverGen        = GetComponent<_RiverGen>();


        if (SEED == 0) SEED = Random.Range(0, 99999);

		Debug.Log("SEED: " + SEED);

		Random.InitState(this.SEED);

        WORLD_SIZE = Random.Range(64, 96);

		Debug.Log("WORLD SIZE: " + WORLD_SIZE);

		TOTAL_TILES = (int) Mathf.Pow(this.WORLD_SIZE, 2);
	}

	private void Start()
	{
		tiles = new GameObject[TOTAL_TILES];

		//emptyTileGen.Generate(WORLD_SIZE, TILE_SIZE, ref tiles);
		//settlementGen.Generate(WORLD_SIZE, TILE_SIZE, ref tiles);
		//riverGen.Generate(WORLD_SIZE, TILE_SIZE, ref tiles);
		//dyanmicTileGen.Generate(WORLD_SIZE, TILE_SIZE, ref tiles);
		//decoGen.Generate(WORLD_SIZE, TILE_SIZE, ref tiles);
	}
}
