using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	public static Controller instance = null;

	[HideInInspector] public int worldSize;
	[HideInInspector] public int tileSize;
	[HideInInspector] public int numSettlements;

	[Header("Parents")]
	public GameObject emptyTilesParent;
	public GameObject riverTilesParent;

	private void Awake()
	{
		instance = this;

		worldSize		= 64;
		tileSize		= 3;
		numSettlements	= 5;
	}

	private void Start()
	{
		StartCoroutine(IGenerate());
	}

	private IEnumerator IGenerate()
	{
		// Empty Tiles
		var emptyTileGen = GetComponent<EmptyTileGen>();
		emptyTileGen.Generate();
		yield return new WaitUntil(() => emptyTileGen.isDone);

		// Settlements
		var settlementGen = GetComponent<SettlementGen>();
		settlementGen.Generate();
		yield return new WaitUntil(() => settlementGen.isDone);

		// River
		var riverGen = GetComponent<RiverGen>();
		riverGen.Generate();
		yield return new WaitUntil(() => riverGen.isDone);
	}
}
