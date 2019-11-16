﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	public static Controller instance = null;

	[HideInInspector] public int worldSize;
	[HideInInspector] public int tileSize;
	[HideInInspector] public int numSettlements;
	[HideInInspector] public int numMushPatches;

	[Header("Gameobjects")]
	public GameObject emptyTilesParent;
	public GameObject roofTilesParent;

	private void Awake()
	{
		instance = this;

		worldSize		= 65;
		tileSize		= 3;	// CONSTANT
		numSettlements	= 5;
		numMushPatches	= 3;
	}

	private void Start()
	{
		Debug.Log("SEED: " + Random.seed);


		StartCoroutine(IGenerate());
	}

	private IEnumerator IGenerate()
	{
		// Empty Tiles
		var emptyTileGen = GetComponent<EmptyTileGen>();
		emptyTileGen.Generate();
		yield return new WaitUntil(() => emptyTileGen.isDone);

		// River
		var riverGen = GetComponent<RiverGen>();
		riverGen.Generate();
		yield return new WaitUntil(() => riverGen.isDone);

		// Roof
		var roofGen = GetComponent<RoofGen>();
		roofGen.Generate();
		yield return new WaitUntil(() => roofGen.isDone);

		// Mushroom
		var mushGen = GetComponent<MushPatchGen>();
		mushGen.Generate();
		yield return new WaitUntil(() => mushGen.isDone);

		// Settlements
		var settlementGen = GetComponent<SettlementGen>();
		settlementGen.Generate();
		yield return new WaitUntil(() => settlementGen.isDone);
	}
}
