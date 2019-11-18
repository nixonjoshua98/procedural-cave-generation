using System.Collections;
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

		worldSize		= 128;
		tileSize		= 1;	// CONSTANT
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
		var emptyTileGen = GetComponent<EmptyTileGen>();
		emptyTileGen.Generate();
		yield return new WaitUntil(() => emptyTileGen.isDone);
	}
}
